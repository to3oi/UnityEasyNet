using System.Collections;
using System.Collections.Generic;
using UnityEasyNet;
using UnityEngine;

namespace dev_UnityEasyNet
{
    public class dev_TCPReceiver : MonoBehaviour
    {
        private TCPReceiver tcpReceiver;

        void Start()
        {
            tcpReceiver = new TCPReceiver(10021, test);
        }

        void test(string s)
        {
            DebugUtility.Log($"{s}");
        }
    }
}