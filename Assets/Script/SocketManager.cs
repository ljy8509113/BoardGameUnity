using UnityEngine;
using System;
using System.IO;
using System.Text;
using System.Net;
using System.Net.Sockets;

public class SocketManager : MonoBehaviour {
    
    public string ip = "192.168.0.8";
    public const int port = 8895;

    static Socket socket;
    static IPEndPoint endPoint;
    
    void Awake()
    {
        
    }

    public void onSendMessage()
    {
        string dataToSend = "send test text";
        byte[] data = Encoding.Default.GetBytes(dataToSend);
        
        
    }



}
