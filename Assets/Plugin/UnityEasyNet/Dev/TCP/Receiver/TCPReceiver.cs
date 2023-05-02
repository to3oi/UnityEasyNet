using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace UnityEasyNet
{
    /// <summary>
    /// TCPの受信をする
    /// </summary>
    public class TCPReceiver : IDisposable
    {
        public static Dictionary<TcpClient, int> clientDict = new Dictionary<TcpClient, int>();
        public string m_ipAddress = "127.0.0.1";

        private TcpListener mTcpListener;
        private TcpClient mTcpClient;
        private NetworkStream mNetworkStream;


        //データを受信した際に受信したデータを通知する
        public Action<string> OnDataReceived;

        //データを受信した際に送信元の情報を通知する
        public Action<IPEndPoint> OnIPEndPointReceived;

        private string m_message = string.Empty; // クライアントから受信した文字列

        private byte[] receiveBuffer = new byte[1024];
        private bool isRunning = false;

        #region Constructors

        public TCPReceiver(int _port, Action<string> _OnDataReceived)
        {
            Main();
            /*
            try
            {
                OnDataReceived = _OnDataReceived;

                var ipAddress = IPAddress.Parse(m_ipAddress);
                mTcpListener = new TcpListener(ipAddress, _port);
                mTcpListener.Start();

                Debug.Log("待機中");

                // クライアントからの接続を待機します
                mTcpClient = mTcpListener.AcceptTcpClient();
                isRunning = true;
                Debug.Log("接続完了");

                DebugUtility.Log($"受信開始");
            }
            catch (Exception e)
            {
                DebugUtility.LogError(e.ToString());
            }*/
        }

        #endregion

        public async void Main()
        {
            DebugUtility.Log("START");
            // 非同期でチャットルームを立ち上げる
            Task.Run(() => ChatRoom("room name"));

            // TCPサーバを立ち上げる
            //string ipString = "127.0.0.1";
            //System.Net.IPAddress ipAdd = System.Net.IPAddress.Parse(ipString);
            System.Net.IPAddress ipAdd = IPAddress.Any;
            //Listenするポート番号
            int port = 10021;

            //TcpListenerオブジェクトを作成する
            TcpListener server = new TcpListener(ipAdd, port);
            //Listenを開始する
            server.Start();
            DebugUtility.Log(
                $"Listenを開始しました({((System.Net.IPEndPoint)server.LocalEndpoint).Address}:{((System.Net.IPEndPoint)server.LocalEndpoint).Port})。");

            // test
            //Task.Run(() => TestChat());


            while (true)
            {
                await Task.Yield();
                try
                {
                    //接続要求があったら受け入れる
                    TcpClient client = server.AcceptTcpClient();

                    //クライアントからのTCP接続は別スレッドに投げる
                    Task.Run(() => ChatStream(client));
                }
                catch (Exception e)
                {
                    DebugUtility.LogError($"{e.ToString()}");
                }
            }

            DebugUtility.Log("FINISH");
        }

        static void ChatRoom(string tag)
        {
            DebugUtility.Log("Start Chat");
            DebugUtility.Log("Finish Chat");
        }

        static async Task ChatStream(TcpClient client)
        {
            DebugUtility.Log(
                $"クライアント({((IPEndPoint)client.Client.RemoteEndPoint).Address}:{((IPEndPoint)client.Client.RemoteEndPoint).Port})と接続しました。");

            clientDict.Add(client, 0);

            //NetworkStreamを取得
            NetworkStream stream = client.GetStream();
            StreamReader reader = new StreamReader(stream);


            //接続されている限り読み続ける
            while (client.Connected)
            {
                await Task.Yield();

                string line = await reader.ReadLineAsync() + '\n';
                DebugUtility.Log("Message:" + line);

                // bloadcastで接続しているclient全員に通知
                Task.Run(() => Broadcast(line));
            }

            clientDict.Remove(client);
        }

        static async Task Broadcast(string message)
        {
            if (System.String.IsNullOrEmpty(message))
            {
                return;
            }

            foreach (KeyValuePair<TcpClient, int> pair in clientDict)
            {
                if (pair.Key.Connected)
                {
                    NetworkStream stream = pair.Key.GetStream();
                    await stream.WriteAsync(Encoding.ASCII.GetBytes(message), 0, message.Length);
                    DebugUtility.Log("Send Done:" + message);
                }
            }
        }


        static async Task TestChat()
        {
            Task.Delay(1000);
            DebugUtility.Log("-Start TestChat");

            // 接続試験
            string ipOrHost = "127.0.0.1";
            int port = 10021;
            TcpClient client = new TcpClient(ipOrHost, port);
            var stream = client.GetStream();

            // 送信
            Thread.Sleep(1000);
            Encoding enc = Encoding.UTF8;
            byte[] sendBytes = enc.GetBytes("test message" + '\n');
            //データを送信する
            stream.Write(sendBytes, 0, sendBytes.Length);

            // 受信
            DebugUtility.Log("--Start Read");
            StreamReader reader = new StreamReader(stream);
            string line = await reader.ReadLineAsync();
            DebugUtility.Log("-TestChat Message:" + line);
            DebugUtility.Log("-Finish TestChat");

            // 定期送信試験
            int count = 0;
            while (true)
            {
                sendBytes = enc.GetBytes("[Test]: message test" + count.ToString() + '\n');
                //データを送信する
                stream.Write(sendBytes, 0, sendBytes.Length);
                Thread.Sleep(5000);
                count++;
            }
        }

        public void Dispose()
        {
            mNetworkStream?.Dispose();
            mTcpClient?.Dispose();
            mTcpListener?.Stop();
        }
    }
}