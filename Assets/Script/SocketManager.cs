using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net.Sockets;
using System.IO;
using System;
using System.Text;
using System.Net;
using System.Threading;


public class SocketManager : MonoBehaviour {

    class StateObject
    {
        // Client socket.  
        public Socket workSocket = null;
        // Size of receive buffer.  
        public const int BufferSize = 1024;
        // Receive buffer.  
        public byte[] buffer = new byte[BufferSize];
        // Received data string.  
        public StringBuilder sb = new StringBuilder();
    }

    string ip = "192.168.0.8";
    int port = 8895;
    private AsyncCallback m_fnReceiveHandler;
    public byte[] buffer = new byte[1024];
    Socket socket;

    void Awake()
    {
        socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        try
        {
            Debug.Log("awake");

            //socket.Connect(ip, port);
            IPAddress ipAddress = IPAddress.Parse(ip);
            IPEndPoint endPoint = new IPEndPoint(ipAddress, port);
            socket.BeginConnect(endPoint, new AsyncCallback(ConnectCallback), socket);
            socket.Blocking = false;
            m_fnReceiveHandler = new AsyncCallback(handleDataReceive);
            socket.Connect(ip, port);
            //socket.BeginReceive(buffer, 0, buffer.Length, SocketFlags.None, m_fnReceiveHandler, SocketFlags.None);

        }
        catch (Exception e)
        {
            Debug.Log("socket connection fail : " + e.Message);
        }


    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ConnectCallback(IAsyncResult ar)
    {
        try
        {
            Debug.Log("connection callback");

            // Retrieve the socket from the state object.  
            socket = (Socket)ar.AsyncState;

            // Complete the connection.  
            socket.EndConnect(ar);
            Debug.Log("Socket connected to "+ socket.RemoteEndPoint.ToString());

            // Signal that the connection has been made.  
            //connectDone.Set();
        }
        catch (Exception e)
        {
            Console.WriteLine(e.ToString());
        }
    }


    public void handleDataReceive(IAsyncResult ar)
    {
        Debug.Log("receive");
        // 넘겨진 추가 정보를 가져옵니다.
        // AsyncState 속성의 자료형은 Object 형식이기 때문에 형 변환이 필요합니다~!
        //AsyncObject ao = (AsyncObject)ar.AsyncState;
        
        //socket = (Socket)ar.AsyncState;
        StateObject obj = (StateObject)ar.AsyncState;

        // 자료를 수신하고, 수신받은 바이트를 가져옵니다.
        Int32 recvBytes = socket.EndReceive(ar);

        // 수신받은 자료의 크기가 1 이상일 때에만 자료 처리
        if (recvBytes > 0)
        {
            obj.sb.Append(Encoding.ASCII.GetString(obj.buffer, 0, recvBytes));
            Debug.Log("res : " +obj.sb.ToString());
            socket.BeginReceive(buffer, 0, buffer.Length, SocketFlags.None, m_fnReceiveHandler, socket);
        }

        // 자료 처리가 끝났으면~
        // 이제 다시 데이터를 수신받기 위해서 수신 대기를 해야 합니다.
        // Begin~~ 메서드를 이용해 비동기적으로 작업을 대기했다면
        // 반드시 대리자 함수에서 End~~ 메서드를 이용해 비동기 작업이 끝났다고 알려줘야 합니다!
        

    }

    public void onClick()
    {
        if (socket.Connected)
        {
            byte[] btyString = Encoding.UTF8.GetBytes("test ~ ");

            socket.BeginSend(btyString, 0, btyString.Length, SocketFlags.None, m_fnReceiveHandler, socket);
            //socket.Send(btyString, SocketFlags.None);
            
        }
        else
        {
            Debug.Log("not connection");
        }
    }

    string makeJson(string str)
    {
        return "";
    }
    

 }
