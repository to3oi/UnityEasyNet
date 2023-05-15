using System;
using System.Net;
using System.Text;
using UnityEngine;

namespace UnityEasyNet
{
    public class TCPReceiverString : MonoBehaviour
    {
        private TCPReceiver mTCPReceiver;

        /// <summary>
        /// データを受信した際に受信したデータを通知する
        /// </summary>
        public Action<string> OnDataReceived;


        /// <summary>
        /// 指定したポート番号でTCPの受信を開始します
        /// </summary>
        /// <param name="_port">受信するポート番号</param>
        /// <param name="_OnDataReceived">受信したデータを通知するメソッド</param>
        public TCPReceiverString(int _port, Action<string> _OnDataReceived)
        {
            mTCPReceiver = new TCPReceiver(_port, EncodeBytesToString);
            OnDataReceived = _OnDataReceived;
        }

        /// <summary>
        /// 指定したポート番号でTCPの受信を開始します
        /// </summary>
        /// <param name="_port">受信するポート番号</param>
        /// <param name="_OnDataReceived">受信したデータを通知するメソッド</param>
        /// <param name="_OnIPEndPointReceived">送信元のIPEndPointを通知するメソッド</param>
        public TCPReceiverString(int _port, Action<string> _OnDataReceived, Action<IPEndPoint> _OnIPEndPointReceived)
        {
            mTCPReceiver = new TCPReceiver(_port, EncodeBytesToString, _OnIPEndPointReceived);
            OnDataReceived = _OnDataReceived;
        }


        /// <summary>
        /// 指定したIPアドレスとポート番号でTCPの受信を開始します
        /// </summary>
        /// <param name="_ipAddress">受信するIPアドレス</param>
        /// <param name="_port">受信するポート番号</param>
        /// <param name="_OnDataReceived">受信したデータを通知するメソッド</param>
        public TCPReceiverString(IPAddress _ipAddress, int _port, Action<string> _OnDataReceived)
        {
            mTCPReceiver = new TCPReceiver(_ipAddress, _port, EncodeBytesToString);
            OnDataReceived = _OnDataReceived;
        }

        /// <summary>
        /// 指定したIPアドレスとポート番号でTCPの受信を開始します
        /// </summary>
        /// <param name="_ipAddress">受信するIPアドレス</param>
        /// <param name="_port">受信するポート番号</param>
        /// <param name="_OnDataReceived">受信したデータを通知するメソッド</param>
        /// <param name="_OnIPEndPointReceived">送信元のIPEndPointを通知するメソッド</param>
        public TCPReceiverString(IPAddress _ipAddress, int _port, Action<string> _OnDataReceived,
            Action<IPEndPoint> _OnIPEndPointReceived)
        {
            mTCPReceiver = new TCPReceiver(_ipAddress, _port, EncodeBytesToString, _OnIPEndPointReceived);
            OnDataReceived = _OnDataReceived;
        }

        /// <summary>
        /// 指定したIPEndPointでTCPの受信を開始します
        /// </summary>
        /// <param name="_remoteEP">接続先の情報が入ったIPEndPoint</param>
        /// <param name="_OnDataReceived">受信したデータを通知するメソッド</param>
        public TCPReceiverString(IPEndPoint _remoteEP, Action<string> _OnDataReceived)
        {
            mTCPReceiver = new TCPReceiver(_remoteEP, EncodeBytesToString);
            OnDataReceived = _OnDataReceived;
        }

        /// <summary>
        /// 指定したIPEndPointでTCPの受信を開始します
        /// </summary>
        /// <param name="_remoteEP">接続先の情報が入ったIPEndPoint</param>
        /// <param name="_OnDataReceived">受信したデータを通知するメソッド</param>
        /// <param name="_OnIPEndPointReceived">送信元のIPEndPointを通知するメソッド</param>
        public TCPReceiverString(IPEndPoint _remoteEP, Action<string> _OnDataReceived,
            Action<IPEndPoint> _OnIPEndPointReceived)
        {
            mTCPReceiver = new TCPReceiver(_remoteEP, EncodeBytesToString, _OnIPEndPointReceived);
            OnDataReceived = _OnDataReceived;
        }

        
        
        /// <summary>
        /// Byte配列をStringに変換する
        /// </summary>
        /// <param name="buffer"></param>
        private void EncodeBytesToString((byte[] buffer,int readCount) buffer)
        {
            //UTF8でエンコード
            string s = Encoding.UTF8.GetString(buffer.buffer,0,buffer.readCount);

            //受け取ったデータを通知
            OnDataReceived?.Invoke(s);
        }
    }
}