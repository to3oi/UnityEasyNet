using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace UnityEasyNet
{
    /// <summary>
    /// TCPの受信の開始と受け取りをする
    /// </summary>
    public class TCPReceiver : IDisposable
    {
        private TcpListener mTcpListener;

        //受信の状態が実行中か判断するためのbool
        private bool isRunning = false;

        /// <summary>
        /// データを受信した際に受信したデータを通知する
        /// </summary>
        public Action<string> OnDataReceived;

        /// <summary>
        ///データを受信した際に送信元の情報を通知する
        /// </summary>
        public Action<IPEndPoint> OnIPEndPointReceived;

        //受信したデータを保持するための変数
        private byte[] receiveBuffer = new byte[1024];

        #region Constructors

        /// <summary>
        /// 指定したポート番号でTCPの受信を開始します
        /// このコンストラクターは一応残してありますが受信したデータを取り扱うことはできません
        /// </summary>
        /// <param name="_port">受信するポート番号</param>
        public TCPReceiver(int _port)
        {
            try
            {
                IPAddress ipAddress = IPAddress.Any;
                IPEndPoint remoteEP = new IPEndPoint(ipAddress, _port);

                mTcpListener = new TcpListener(remoteEP);
                isRunning = true;

                StartListening();
            }
            catch (Exception e)
            {
                DebugUtility.LogError(e.ToString());
            }
        }

        /// <summary>
        /// 指定したポート番号でTCPの受信を開始します
        /// </summary>
        /// <param name="_port">受信するポート番号</param>
        /// <param name="_OnDataReceived">受信したデータを通知するメソッド</param>
        public TCPReceiver(int _port, Action<string> _OnDataReceived)
        {
            try
            {
                OnDataReceived = _OnDataReceived;

                IPAddress ipAddress = IPAddress.Any;
                IPEndPoint remoteEP = new IPEndPoint(ipAddress, _port);

                mTcpListener = new TcpListener(remoteEP);
                isRunning = true;

                StartListening();
            }
            catch (Exception e)
            {
                DebugUtility.LogError(e.ToString());
            }
        }

        /// <summary>
        /// 指定したポート番号でTCPの受信を開始します
        /// </summary>
        /// <param name="_port">受信するポート番号</param>
        /// <param name="_OnDataReceived">受信したデータを通知するメソッド</param>
        /// <param name="_OnIPEndPointReceived">送信元のIPEndPointを通知するメソッド</param>
        public TCPReceiver(int _port, Action<string> _OnDataReceived, Action<IPEndPoint> _OnIPEndPointReceived)
        {
            try
            {
                OnDataReceived = _OnDataReceived;
                OnIPEndPointReceived = _OnIPEndPointReceived;

                IPAddress ipAddress = IPAddress.Any;
                IPEndPoint remoteEP = new IPEndPoint(ipAddress, _port);

                mTcpListener = new TcpListener(remoteEP);
                isRunning = true;

                StartListening();
            }
            catch (Exception e)
            {
                DebugUtility.LogError(e.ToString());
            }
        }

        /// <summary>
        /// 指定したIPアドレスとポート番号でTCPの受信を開始します
        /// このコンストラクターは一応残してありますが受信したデータを取り扱うことはできません
        /// </summary>
        /// <param name="_ipAddress">受信するIPアドレス</param>
        /// <param name="_port">受信するポート番号</param>
        public TCPReceiver(IPAddress _ipAddress, int _port)
        {
            try
            {
                IPEndPoint remoteEP = new IPEndPoint(_ipAddress, _port);

                mTcpListener = new TcpListener(remoteEP);
                isRunning = true;

                StartListening();
            }
            catch (Exception e)
            {
                DebugUtility.LogError(e.ToString());
            }
        }

        /// <summary>
        /// 指定したIPアドレスとポート番号でTCPの受信を開始します
        /// </summary>
        /// <param name="_ipAddress">受信するIPアドレス</param>
        /// <param name="_port">受信するポート番号</param>
        /// <param name="_OnDataReceived">受信したデータを通知するメソッド</param>
        /// <param name="_OnIPEndPointReceived">送信元のIPEndPointを通知するメソッド</param>
        public TCPReceiver(IPAddress _ipAddress, int _port, Action<string> _OnDataReceived)
        {
            try
            {
                IPEndPoint remoteEP = new IPEndPoint(_ipAddress, _port);

                mTcpListener = new TcpListener(remoteEP);
                isRunning = true;

                StartListening();
            }
            catch (Exception e)
            {
                DebugUtility.LogError(e.ToString());
            }
        }

        /// <summary>
        /// 指定したIPアドレスとポート番号でTCPの受信を開始します
        /// </summary>
        /// <param name="_ipAddress">受信するIPアドレス</param>
        /// <param name="_port">受信するポート番号</param>
        /// <param name="_OnDataReceived">受信したデータを通知するメソッド</param>
        /// <param name="_OnIPEndPointReceived">送信元のIPEndPointを通知するメソッド</param>
        public TCPReceiver(IPAddress _ipAddress, int _port, Action<string> _OnDataReceived,
            Action<IPEndPoint> _OnIPEndPointReceived)
        {
            try
            {
                IPEndPoint remoteEP = new IPEndPoint(_ipAddress, _port);

                mTcpListener = new TcpListener(remoteEP);
                isRunning = true;

                StartListening();
            }
            catch (Exception e)
            {
                DebugUtility.LogError(e.ToString());
            }
        }

        /// <summary>
        /// 指定したIPEndPointでTCPの受信を開始します
        /// このコンストラクターは一応残してありますが受信したデータを取り扱うことはできません
        /// </summary>
        /// <param name="_remoteEP">接続先の情報が入ったIPEndPoint</param>
        /// <param name="_OnDataReceived">受信したデータを通知するメソッド</param>
        /// <param name="_OnIPEndPointReceived">送信元のIPEndPointを通知するメソッド</param>
        public TCPReceiver(IPEndPoint _remoteEP)
        {
            try
            {
                mTcpListener = new TcpListener(_remoteEP);
                isRunning = true;

                StartListening();
            }
            catch (Exception e)
            {
                DebugUtility.LogError(e.ToString());
            }
        }

        /// <summary>
        /// 指定したIPEndPointでTCPの受信を開始します
        /// </summary>
        /// <param name="_remoteEP">接続先の情報が入ったIPEndPoint</param>
        /// <param name="_OnDataReceived">受信したデータを通知するメソッド</param>
        /// <param name="_OnIPEndPointReceived">送信元のIPEndPointを通知するメソッド</param>
        public TCPReceiver(IPEndPoint _remoteEP, Action<string> _OnDataReceived)
        {
            try
            {
                OnDataReceived = _OnDataReceived;

                mTcpListener = new TcpListener(_remoteEP);
                isRunning = true;

                StartListening();
            }
            catch (Exception e)
            {
                DebugUtility.LogError(e.ToString());
            }
        }

        /// <summary>
        /// 指定したIPEndPointでTCPの受信を開始します
        /// </summary>
        /// <param name="_remoteEP">接続先の情報が入ったIPEndPoint</param>
        /// <param name="_OnDataReceived">受信したデータを通知するメソッド</param>
        /// <param name="_OnIPEndPointReceived">送信元のIPEndPointを通知するメソッド</param>
        public TCPReceiver(IPEndPoint _remoteEP, Action<string> _OnDataReceived,
            Action<IPEndPoint> _OnIPEndPointReceived)
        {
            try
            {
                OnDataReceived = _OnDataReceived;
                OnIPEndPointReceived = _OnIPEndPointReceived;

                mTcpListener = new TcpListener(_remoteEP);
                isRunning = true;

                StartListening();
            }
            catch (Exception e)
            {
                DebugUtility.LogError(e.ToString());
            }
        }

        #endregion

        /// <summary>
        /// 接続の待機をし、接続があったらそのデータをReceiveAsyncへ渡します
        /// </summary>
        private async void StartListening()
        {
            try
            {
                mTcpListener.Start();

                while (isRunning)
                {
                    DebugUtility.Log("接続待機");
                    TcpClient tcpClient = await mTcpListener.AcceptTcpClientAsync();

                    ReceiveAsync(tcpClient);
                }
            }
            catch (Exception e)
            {
                DebugUtility.LogError(e.ToString());
            }
        }

        /// <summary>
        /// 受信したデータを文字列に変換しOnDataReceivedとOnIPEndPointReceivedに設定された関数へ渡します
        /// </summary>
        /// <param name="tcpClient"></param>
        private async void ReceiveAsync(TcpClient tcpClient)
        {
            try
            {
                using (NetworkStream stream = tcpClient.GetStream())
                {
                    while (isRunning)
                    {
                        int byteSize = await stream.ReadAsync(receiveBuffer, 0, receiveBuffer.Length);
                        if (byteSize == 0) // クライアントが切断した場合
                        {
                            tcpClient.Close();
                            return;
                        }

                        string result = Encoding.UTF8.GetString(receiveBuffer, 0, byteSize);
                        DebugUtility.Log($"get {result}");
                        OnDataReceived?.Invoke(result);
                        OnIPEndPointReceived?.Invoke((IPEndPoint)tcpClient.Client.RemoteEndPoint);
                    }
                }
            }
            catch (Exception e)
            {
                DebugUtility.LogError(e.ToString());
            }
        }

        /// <summary>
        /// 通信の停止をします
        /// </summary>
        public void Stop()
        {
            Dispose();
        }

        public void Dispose()
        {
            isRunning = false;
            mTcpListener?.Stop();
        }
    }
}