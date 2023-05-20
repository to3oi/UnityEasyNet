using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace UnityEasyNet
{
    public class TCPSenderString : TCPSender
    {
        /// <summary>
        /// 指定したIPEndPointにメッセージを送る準備を開始します
        /// </summary>
        /// <param name="_ipEndPoint">送信先の情報が入ったIPEndPoint</param>
        public TCPSenderString(IPEndPoint _ipEndPoint) : base(_ipEndPoint)
        {
        }

        /// <summary>
        /// 指定したAddressFamilyにメッセージを送る準備を開始します
        /// </summary>
        /// <param name="_addressFamily">送信先の情報が入ったAddressFamily</param>>
        public TCPSenderString(AddressFamily _addressFamily) : base(_addressFamily)
        {
        }

        /// <summary>
        /// 指定したAddressFamilyにメッセージを送る準備を開始します
        /// </summary>
        /// <param name="_addressFamily">送信先の情報が入ったAddressFamily</param>>
        public TCPSenderString(string _hostName, int _port) : base(_hostName, _port)
        {
        }

        /// <summary>
        /// 登録したアドレスのポートに文字列を送信する
        /// </summary>
        /// <param name="s">送信する文字列</param>
        public void Send(string s)
        {
            try
            {
                if (!mTcpClient.Connected)
                {
                    DebugUtility.LogError($"接続が確立されていません");
                    return;
                }

                byte[] bytes = Encoding.UTF8.GetBytes(s);

                Send(bytes);
            }
            catch (Exception e)
            {
                DebugUtility.LogError(e.ToString());
            }
        }
    }
}