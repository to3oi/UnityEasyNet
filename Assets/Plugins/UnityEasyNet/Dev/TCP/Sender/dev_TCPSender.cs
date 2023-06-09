using System.Net;
using UnityEasyNet;
using UnityEngine;
using UnityEngine.UI;

namespace dev_UnityEasyNet
{
    public class dev_TCPSender : MonoBehaviour
    {
        private TCPSenderString mTCPSender;
        [SerializeField] private InputField _inputField;
        void Start()
        {
            IPEndPoint ipEndPoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"),13000);
            mTCPSender = new TCPSenderString(ipEndPoint);
        }

        public void Send()
        {
            mTCPSender.Send(_inputField.text);
        }
    }
}