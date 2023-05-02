using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEasyNet;
using UnityEngine;
using UnityEngine.UI;

namespace dev_UnityEasyNet
{
    public class dev_TCPSender : MonoBehaviour
    {
        private TCPSender mTCPSender;
        [SerializeField] private InputField _inputField;
        void Start()
        {
            IPEndPoint ipEndPoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"),10021);
            //mTCPSender = new TCPSender("127.0.0.1",11000);
            mTCPSender = new TCPSender(ipEndPoint);
        }

        public void Send()
        {
            mTCPSender.Send(_inputField.text);
        }
    }
}