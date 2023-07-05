# Welcom to UnityEasyNet!
UnityEasyNetはUnity内でTCPとUDP通信によるByte配列の送受信を簡単にできるPluginです。
現在Byte[]とStringの通信のみサポートしています。

# Introduction
接続にはポート番号とIPアドレスなどが必要になります。
以下は簡単な例です。

受信側
```C#
var mUDPReceiverString = new UDPReceiverString(/*受信するポート番号*/, ReceiveDataUpdate);

void ReceiveDataUpdate(String s){
Debug.Log(s);
}
```

送信側
```C#
var mUDPSenderString = new UDPSenderString(/*接続先のIPアドレス*/, /*接続先のポート番号*/);

mUDPSenderString.Send(/*テキスト*/);
```

# UPM
`https://github.com/to3oi/UnityEasyNet.git?path=Assets/Plugins/UnityEasyNet/Scripts`をUnity Package Manager の`Add package from git url`で追加してください。

# Samples
[サンプル一覧はこちら](https://github.com/tomoi/UnityEasyNet/tree/main/Assets/Plugins/UnityEasyNet/Scripts/Samples~/UnityEasyNet)

# licenses
MIT
