using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace UnityEasyNet
{
    public class TCPReceiver : IDisposable
    {
        private TcpListener mTcpListener;
        private bool isRunning = false;

        public Action<string> OnDataReceived;
        public Action<IPEndPoint> OnIPEndPointReceived;

        private byte[] receiveBuffer = new byte[1024];

        public TCPReceiver(int _port, Action<string> _OnDataReceived)
        {
            try
            {
                OnDataReceived = _OnDataReceived;

                IPAddress ipAddress = IPAddress.Any;
                IPEndPoint remoteEP = new IPEndPoint(ipAddress, _port);

                mTcpListener = new TcpListener(remoteEP);
                isRunning = true;

                StartListening();
            }
            catch (Exception e)
            {
                DebugUtility.LogError(e.ToString());
            }
        }
        public TCPReceiver(int _port, Action<string> _OnDataReceived,Action<IPEndPoint> _OnIPEndPointReceived)
        {
            try
            {
                OnDataReceived = _OnDataReceived;
                OnIPEndPointReceived = _OnIPEndPointReceived;
                
                IPAddress ipAddress = IPAddress.Any;
                IPEndPoint remoteEP = new IPEndPoint(ipAddress, _port);

                mTcpListener = new TcpListener(remoteEP);
                isRunning = true;

                StartListening();
            }
            catch (Exception e)
            {
                DebugUtility.LogError(e.ToString());
            }
        }

        private async void StartListening()
        {
            try
            {
                mTcpListener.Start();

                while (isRunning)
                {
                    DebugUtility.Log("接続待機");
                    var tcpClient = await mTcpListener.AcceptTcpClientAsync();

                    ReceiveAsync(tcpClient);
                }
            }
            catch (Exception e)
            {
                DebugUtility.LogError(e.ToString());
            }
        }

        public async void ReceiveAsync(TcpClient tcpClient)
        {
            try
            {
                using (NetworkStream stream = tcpClient.GetStream())
                {
                    while (isRunning)
                    {
                        int byteSize = await stream.ReadAsync(receiveBuffer, 0, receiveBuffer.Length);
                        if (byteSize == 0) // クライアントが切断した場合
                        {
                            tcpClient.Close();
                            return;
                        }

                        string result = Encoding.UTF8.GetString(receiveBuffer, 0, byteSize);
                        DebugUtility.Log($"get {result}");
                        OnDataReceived?.Invoke(result);
                        OnIPEndPointReceived?.Invoke((IPEndPoint)tcpClient.Client.RemoteEndPoint);
                    }
                }
            }
            catch (Exception e)
            {
                DebugUtility.LogError(e.ToString());
            }
        }

        public void Stop()
        {
            Dispose();
        }
        public void Dispose()
        {
            isRunning = false;
            mTcpListener?.Stop();
        }
    }
}
