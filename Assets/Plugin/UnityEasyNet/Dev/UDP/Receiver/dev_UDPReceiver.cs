using UnityEasyNet;
using UnityEngine;

namespace dev_UnityEasyNet
{
    public class dev_UDPReceiver : MonoBehaviour
    {
        void Start()
        {
            UDPReceiverString udpReceiver = new UDPReceiverString(10000,test);
        }

        void test(string s)
        {
            DebugUtility.Log($"{s}");
        }
    }
}