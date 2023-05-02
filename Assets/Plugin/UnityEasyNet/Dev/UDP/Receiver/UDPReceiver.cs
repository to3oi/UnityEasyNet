using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace UnityEasyNet
{
    /// <summary>
    /// UDPの受信の開始と受け取りをする
    /// </summary>
    public class UDPReceiver : IDisposable
    {
        //TODO:IDisposableをよくわかってないのでDisposeの処理を実行するタイミングがない？

        private UdpClient mUDP;

        //データを受信した際に受信したデータを通知する
        public Action<string> OnDataReceived;

        //データを受信した際に送信元の情報を通知する
        public Action<IPEndPoint> OnIPEndPointReceived;

        #region Constructors

        /// <summary>
        /// 指定したポート番号でUDPの受信を開始します
        /// </summary>
        /// <param name="_port">受信するポート番号</param>
        public UDPReceiver(int _port)
        {
            try
            {
                mUDP = new UdpClient(_port);
                mUDP.BeginReceive(UDPReceive, mUDP);
                DebugUtility.Log($"受信開始");
            }
            catch (Exception e)
            {
                DebugUtility.LogError(e.ToString());
            }
        }

        /// <summary>
        /// 指定したポート番号でUDPの受信を開始します
        /// </summary>
        /// <param name="_port">受信するポート番号</param>
        /// <param name="_OnDataReceived">受信したデータを通知するメソッド</param>
        public UDPReceiver(int _port, Action<string> _OnDataReceived)
        {
            try
            {
                //アクションの登録
                OnDataReceived = _OnDataReceived;

                mUDP = new UdpClient(_port);
                mUDP.BeginReceive(UDPReceive, mUDP);
                DebugUtility.Log($"受信開始");
            }
            catch (Exception e)
            {
                DebugUtility.LogError(e.ToString());
            }
        }

        /// <summary>
        /// 指定したポート番号でUDPの受信を開始します
        /// </summary>
        /// <param name="_port">受信するポート番号</param>
        /// <param name="_OnDataReceived">受信したデータを通知するメソッド</param>
        /// <param name="_OnIPEndPointReceived">送信元のIPEndPointを通知するメソッド</param>
        public UDPReceiver(int _port, Action<string> _OnDataReceived, Action<IPEndPoint> _OnIPEndPointReceived)
        {
            try
            {
                //アクションの登録
                OnDataReceived = _OnDataReceived;
                OnIPEndPointReceived = _OnIPEndPointReceived;

                mUDP = new UdpClient(_port);
                mUDP.BeginReceive(UDPReceive, mUDP);
                DebugUtility.Log($"受信開始");
            }
            catch (Exception e)
            {
                DebugUtility.LogError(e.ToString());
            }
        }

        /// <summary>
        /// 指定したポート番号でUDPの受信を開始します
        /// </summary>
        /// <param name="_port">受信するポート番号</param>
        /// <param name="_OnIPEndPointReceived">送信元のIPEndPointを通知するメソッド</param>
        public UDPReceiver(int _port, Action<IPEndPoint> _OnIPEndPointReceived)
        {
            try
            {
                //アクションの登録
                OnIPEndPointReceived = _OnIPEndPointReceived;

                mUDP = new UdpClient(_port);
                mUDP.BeginReceive(UDPReceive, mUDP);
                DebugUtility.Log($"受信開始");
            }
            catch (Exception e)
            {
                DebugUtility.LogError(e.ToString());
            }
        }

        #endregion

        void UDPReceive(IAsyncResult res)
        {
            UdpClient getUDP = (UdpClient)res.AsyncState;
            IPEndPoint ipEnd = null;

            try
            {
                //受信データの取得
                byte[] getByte = getUDP.EndReceive(res, ref ipEnd);

                //UTF8でエンコード
                string s = Encoding.UTF8.GetString(getByte);

                //受け取ったデータを通知
                OnDataReceived?.Invoke(s);

                //IPEndPointを通知
                OnIPEndPointReceived?.Invoke(ipEnd);
            }
            catch (SocketException e)
            {
                DebugUtility.LogError(e.ToString());
                return;
            }
            catch (ObjectDisposedException e)
            {
                DebugUtility.LogError(e.ToString());
                return;
            }

            //再度受信
            getUDP.BeginReceive(UDPReceive, getUDP);
        }

        public void Dispose()
        {
            mUDP?.Dispose();
        }
    }
}