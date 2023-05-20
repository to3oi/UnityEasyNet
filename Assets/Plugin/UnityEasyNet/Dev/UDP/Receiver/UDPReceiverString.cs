using System;
using System.Net;
using System.Text;

namespace UnityEasyNet
{
    /// <summary>
    /// UDPの受信の開始と受け取りをし、stringに変換して通知する
    /// </summary>
    public class UDPReceiverString
    {
        private UDPReceiver mUDPReceiver;

        /// <summary>
        /// データを受信した際に受信したデータを通知する
        /// </summary>
        public Action<string> OnDataReceived;

        /// <summary>
        /// 指定したポート番号でUDPの受信を開始します
        /// </summary>
        /// <param name="_port">受信するポート番号</param>
        /// <param name="_OnDataReceived">受信したデータを通知するメソッド</param>
        public UDPReceiverString(int _port, Action<string> _OnDataReceived)
        {
            mUDPReceiver = new UDPReceiver(_port, EncodeBytesToString);
            OnDataReceived = _OnDataReceived;
        }

        /// <summary>
        /// 指定したポート番号でUDPの受信を開始します
        /// </summary>
        /// <param name="_port">受信するポート番号</param>
        /// <param name="_OnDataReceived">受信したデータを通知するメソッド</param>
        /// <param name="_OnIPEndPointReceived">送信元のIPEndPointを通知するメソッド</param>
        public UDPReceiverString(int _port, Action<string> _OnDataReceived, Action<IPEndPoint> _OnIPEndPointReceived)
        {
            mUDPReceiver = new UDPReceiver(_port, EncodeBytesToString, _OnIPEndPointReceived);
            OnDataReceived = _OnDataReceived;
        }

        /// <summary>
        /// Byte配列をStringに変換する
        /// </summary>
        /// <param name="bytes"></param>
        private void EncodeBytesToString(byte[] bytes)
        {
            //UTF8でエンコード
            string s = Encoding.UTF8.GetString(bytes);

            //受け取ったデータを通知
            OnDataReceived?.Invoke(s);
        }
    }
}