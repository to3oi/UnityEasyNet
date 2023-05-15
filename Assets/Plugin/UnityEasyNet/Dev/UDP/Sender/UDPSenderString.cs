using System;
using System.Net;
using System.Text;

namespace UnityEasyNet
{
    public class UDPSenderString : UDPSender
    {
        public UDPSenderString(string _hostName, int _port) : base(_hostName, _port)
        {
        }

        public UDPSenderString(IPEndPoint _ipEndPoint) : base(_ipEndPoint)
        {
        }

        public UDPSenderString(IPAddress _ipAddress, int _port) : base(_ipAddress, _port)
        {
        }

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