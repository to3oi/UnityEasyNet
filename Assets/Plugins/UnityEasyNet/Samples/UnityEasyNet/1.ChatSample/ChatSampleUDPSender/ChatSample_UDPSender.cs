using System;
using System.Net;
using UnityEasyNet;
using UnityEngine;
using UnityEngine.UI;

public class ChatSample_UDPSender : MonoBehaviour
{
    private UDPSenderString mUDPSenderString;

    [SerializeField] private Text mLocalIPAddress;
    [SerializeField] private InputField mSendIPAddress;
    [SerializeField] private InputField mSendPort;

    [SerializeField] private Image mConnectImage;
    [SerializeField] private Button mConnectButton;


    [SerializeField] private InputField mSendText;
    [SerializeField] private Image mSendImage;
    [SerializeField] private Button mSendButton;

    private bool mConected = false;

    void Start()
    {
        //IPv4のアドレスを取得して表示
        IPHostEntry ipHostEntry = Dns.GetHostEntry(Dns.GetHostName());

        foreach (IPAddress ip in ipHostEntry.AddressList)
        {
            if (ip.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
            {
                mLocalIPAddress.text = ip.ToString();
                break;
            }
        }

        mConnectButton.onClick.AddListener(Connect);
        mSendButton.onClick.AddListener(SendMessage);
    }

    /// <summary>
    /// 接続開始
    /// </summary>
    public void Connect()
    {
        if (mConected)
        {
            return;
        }

        mConected = true;

        try
        {
            //UDPSenderの接続処理
            mUDPSenderString = new UDPSenderString(mSendIPAddress.text, int.Parse(mSendPort.text));
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }

        mConnectImage.color = Color.green;
    }

    /// <summary>
    /// メッセージの送信
    /// </summary>
    public void SendMessage()
    {
        if (!mConected)
        {
            return;
        }

        try
        {
            //メッセージの送信
            mUDPSenderString.Send(mSendText.text);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }

        mSendImage.color = Color.green;
    }
}