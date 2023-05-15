using System;
using System.Net;
using System.Net.Sockets;

namespace UnityEasyNet
{
    /// <summary>
    /// TCPの送信をする
    /// </summary>
    public class TCPSender : IDisposable
    {
        protected TcpClient mTcpClient;
        private NetworkStream mNetworkStream;


        private bool mIsConnection;

        #region Constructors

        /// <summary>
        /// 指定したIPEndPointにメッセージを送る準備を開始します
        /// </summary>
        /// <param name="_ipEndPoint">送信先の情報が入ったIPEndPoint</param>
        public TCPSender(IPEndPoint _ipEndPoint)
        {
            try
            {
                mTcpClient = new TcpClient();
                mTcpClient.Connect(_ipEndPoint);
                mNetworkStream = mTcpClient.GetStream();
                mIsConnection = true;
                DebugUtility.Log($"接続完了");
            }
            catch (Exception e)
            {
                DebugUtility.LogError(e.ToString());
            }
        }

        /// <summary>
        /// 指定したAddressFamilyにメッセージを送る準備を開始します
        /// </summary>
        /// <param name="_addressFamily">送信先の情報が入ったAddressFamily</param>>
        public TCPSender(AddressFamily _addressFamily)
        {
            try
            {
                mTcpClient = new TcpClient(_addressFamily);
                mNetworkStream = mTcpClient.GetStream();
                mIsConnection = true;
                DebugUtility.Log($"送信準備完了");
            }
            catch (Exception e)
            {
                DebugUtility.LogError(e.ToString());
            }
        }

        /// <summary>
        /// 指定したホスト名のポートにメッセージを送る準備を開始します
        /// </summary>
        /// <param name="_hostName">送信先のリモートホストのDNS名</param>
        /// <param name="_port">送信先のポート番号</param>
        public TCPSender(string _hostName, int _port)
        {
            try
            {
                mTcpClient = new TcpClient();
                mTcpClient.Connect(_hostName, _port);
                mNetworkStream = mTcpClient.GetStream();
                mIsConnection = true;
                DebugUtility.Log($"送信準備完了");
            }
            catch (Exception e)
            {
                DebugUtility.LogError(e.ToString());
            }
        }

        #endregion


        /// <summary>
        /// 登録したアドレスのポートにbyte配列を送信する
        /// </summary>
        /// <param name="bytes">送信するbyte配列</param>
        public void Send(byte[] bytes)
        {
            try
            {
                if (!mTcpClient.Connected)
                {
                    DebugUtility.LogError($"接続が確立されていません");
                    return;
                }
                
                mNetworkStream.BeginWrite(bytes, 0, bytes.Length, null, null);
                DebugUtility.Log($"送信成功");
            }
            catch (Exception e)
            {
                DebugUtility.LogError(e.ToString());
            }
        }

        public void Dispose()
        {
            mTcpClient?.Dispose();
            mNetworkStream?.Dispose();
        }
    }
}