using UnityEasyNet;
using UnityEngine;

namespace dev_UnityEasyNet
{
    public class dev_UDPReceiver : MonoBehaviour
    {
        void Start()
        {
            UDPReceiver udpReceiver = new UDPReceiver(10000,test);
        }

        void test(string s)
        {
            DebugUtility.Log($"{s}");
        }
    }
}