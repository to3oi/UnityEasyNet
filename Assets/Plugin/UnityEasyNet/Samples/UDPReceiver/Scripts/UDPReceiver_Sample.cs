using System;
using System.Net;
using UnityEasyNet;
using UnityEngine;
using UnityEngine.UI;

public class UDPReceiver_Sample : MonoBehaviour
{
    private UDPReceiver mUDPReceiver;

    [SerializeField] private Text mLocalIPAddress;

    [SerializeField] private InputField mReceivePort;
    [SerializeField] private Image mOnlyPortConnectImage;
    [SerializeField] private Button mOnlyPortConnectButton;

    [SerializeField] private Image mAnyPortConnectImage;
    [SerializeField] private Button mAnyPortConnectButton;

    [SerializeField] private Text mReceiveText;

    private int mReciveCount = 0;
    private string mReceiveMessage;
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

        mReceiveText.text = "";

        mOnlyPortConnectButton.onClick.AddListener(OnlyConnect);
        mAnyPortConnectButton.onClick.AddListener(AnyConnect);
    }

    /// <summary>
    /// 指定したポートで接続開始
    /// </summary>
    public void OnlyConnect()
    {
        if (mConected)
        {
            return;
        }

        mConected = true;

        try
        {
            //UDPReceiverの接続処理
            mUDPReceiver = new UDPReceiver(int.Parse(mReceivePort.text), ReceiveDataUpdate);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }

        mOnlyPortConnectImage.color = Color.green;
    }

    /// <summary>
    /// すべてのポートで接続開始
    /// </summary>
    public void AnyConnect()
    {
        //実装中
        return;

        if (mConected)
        {
            return;
        }

        mConected = true;

        try
        {
            //UDPReceiverの接続処理
            mUDPReceiver = new UDPReceiver(int.Parse(mReceivePort.text), ReceiveDataUpdate);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }

        mAnyPortConnectImage.color = Color.green;
    }


    void ReceiveDataUpdate(String s)
    {
        if (!mConected)
        {
            return;
        }

        mReciveCount++;


        if (10 <= mReciveCount)
        {
            string str = mReceiveText.text;
            string delimiter = "\n";
            int index = str.IndexOf(delimiter);
            if (index != -1)
            {
                mReceiveMessage = str.Substring(index + delimiter.Length);
            }
            mReceiveMessage += s;
            mReceiveMessage += "\n";
        }
        else
        {
            mReceiveMessage += s;
            mReceiveMessage += "\n";
        }
    }

    void Update()
    {
        //ReceiveDataUpdateで実装するとUIの更新がされないためUpdateで実装
        mReceiveText.text = mReceiveMessage;
    }
}