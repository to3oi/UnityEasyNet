using System;
using System.Net;
using System.Text;

namespace UnityEasyNet
{
    public class UDPSenderString : UDPSender
    {
        /// <summary>
        /// 指定したホスト名のポートにメッセージを送る準備を開始します
        /// </summary>
        /// <param name="_hostName">送信先のリモートホストのDNS名</param>
        /// <param name="_port">送信先のポート番号</param>
        public UDPSenderString(string _hostName, int _port) : base(_hostName, _port)
        {
        }

        /// <summary>
        /// 指定したIPEndPointにメッセージを送る準備を開始します
        /// </summary>
        /// <param name="_ipEndPoint">送信先の情報が入ったIPEndPoint</param>
        public UDPSenderString(IPEndPoint _ipEndPoint) : base(_ipEndPoint)
        {
        }

        /// <summary>
        /// 指定したIPアドレスのポートにメッセージを送る準備を開始します
        /// </summary>
        /// <param name="_ipAddress">送信先のIPアドレス</param>
        /// <param name="_port">送信先のポート番号</param>
        public UDPSenderString(IPAddress _ipAddress, int _port) : base(_ipAddress, _port)
        {
        }

        /// <summary>
        /// ローカル内の指定したポートにメッセージを送る準備を開始します
        /// </summary>
        /// <param name="_port">送信先のポート番号</param>
        public UDPSenderString(int _port) : base(_port)
        {
        }
        
        
        /// <summary>
        /// 登録したポートに文字列を送信する
        /// </summary>
        /// <param name="s">送信する文字列</param>
        public void Send(string s)
        {
            try
            {
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