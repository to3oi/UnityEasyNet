using System.Net;
using UnityEasyNet;
using UnityEngine;

namespace dev_UnityEasyNet
{
    public class dev_TCPReceiver : MonoBehaviour
    {
        private TCPReceiverString tcpReceiver;

        void Start()
        {
            tcpReceiver = new TCPReceiverString(13000, test,test2);
        }

        void test(string s)
        {
            DebugUtility.Log($"{s}");
        }
        void test2(IPEndPoint ep)
        {
            DebugUtility.Log($"ip {ep.Address}\n port {ep.Port}");
        }
    }
}