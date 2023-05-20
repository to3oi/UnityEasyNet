using UnityEasyNet;
using UnityEngine;
using UnityEngine.UI;

namespace dev_UnityEasyNet
{
    public class dev_UDPSender : MonoBehaviour
    {
        private UDPSenderString mUDPSender;
        [SerializeField] private InputField _inputField;
        void Start()
        {
            mUDPSender = new UDPSenderString("localhost",10000);
        }

        public void Send()
        {
            mUDPSender.Send(_inputField.text);
        }
    }
}