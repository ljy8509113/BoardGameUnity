using System.Net.Sockets;
using System;
using System.Text;
using System.Net;
using System.Diagnostics;
using UnityEngine;

public class SocketManager
{
    private static SocketManager instance = null;
    public static SocketManager Instance()
    {
        if (instance == null)
        {
            instance = new SocketManager(); //GameObject.FindObjectOfType(typeof(SocketManager)) as SocketManager;
        }

        return instance;
    }

    static int port = 8895;
    private AsyncCallback m_fnReceiveHandler;
    const int BUFFER_SIZE = 2048;
    byte[] buffer = new byte[BUFFER_SIZE];
    Socket socket;

    public delegate void ResponseResultDelegate(bool isSuccess, string identifier, string result);
    public ResponseResultDelegate resDelegate = null;
    public delegate void isConnection(bool isConnection);
    public isConnection socketDelegate = null;

    public SocketManager()
    {

    }

    public void connection(isConnection con)
    {
        socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        socket.SendTimeout = 120000;
        socket.ReceiveTimeout = 120000;

        try
        {
            socketDelegate += con;
            //socket.Connect(ip, port);
            IPAddress ipAddress = IPAddress.Parse("39.115.194.21");//IPAddress.Parse(GameManager.Instance().getIp());
            IPEndPoint endPoint = new IPEndPoint(ipAddress, port);

            //socket.Blocking = false;
            m_fnReceiveHandler = new AsyncCallback(handleDataReceive);
            try
            {
                socket.BeginConnect(endPoint, new AsyncCallback(ConnectCallback), socket);
                Console.WriteLine("beginConnect");
                //socket.Connect(ip, port);
            }
            catch (SocketException e)
            {
                Console.WriteLine("socket error : " + e);
            }

        }
        catch (Exception e)
        {
            Console.WriteLine("socket connection fail : " + e.Message);

        }
    }

    public void ConnectCallback(IAsyncResult ar)
    {
        try
        {
            Console.WriteLine("connection callback");
            // Retrieve the socket from the state object.  
            socket = (Socket)ar.AsyncState;
            Console.WriteLine("1 " + socket);

            // Complete the connection.  
            socket.EndConnect(ar);
            UnityEngine.Debug.Log("Socket connected to " + socket.RemoteEndPoint.ToString());
            socket.BeginReceive(buffer, 0, buffer.Length, SocketFlags.None, m_fnReceiveHandler, socket);
            socketDelegate(true);
        }
        catch (Exception e)
        {
            UnityEngine.Debug.Log("connectionCallback : " + e.Message);
            socketDelegate(false);
        }
    }

    private object lockObject = new object();
    int current = 0;
    string resStr = "";
    public void handleDataReceive(IAsyncResult ar)
    {
        socket = (Socket)ar.AsyncState;
        int length = 0;

        try
        {
            length = socket.EndReceive(ar);
            UnityEngine.Debug.Log("1 receive : " + length);

            lock (lockObject)
            {
                string stringTransferred = Encoding.UTF8.GetString(buffer, 0, length);
                UnityEngine.Debug.Log("response : " + stringTransferred);
                ResponseBase result = JsonUtility.FromJson<ResponseBase>(stringTransferred);
                resDelegate(result.isSuccess(), result.identifier, stringTransferred);
                //resDelegate(result.identifier, stringTransferred);

                buffer = new byte[BUFFER_SIZE];
                socket.BeginReceive(buffer, 0, buffer.Length, SocketFlags.None, m_fnReceiveHandler, socket);
            }

            /*
            int current = 0;

            while (current < length)
            {
                lock (lockObject)
                {
                    byte[] sizeByte = new byte[2];
                    // socket.Receive(sizeByte, 0, 2, SocketFlags.None);
                    Array.Copy(buffer, sizeByte, 2);
                    int size = BitConverter.ToUInt16(sizeByte, 0);
                    byte[] dataBytes = new byte[size];
                    // socket.Receive(dataBytes, 0, size, SocketFlags.None);    
                    Array.Copy(buffer, 2, dataBytes, 0, size);
                    string stringTransferred = Encoding.UTF8.GetString(dataBytes);
                    //Console.WriteLine("response : " + stringTransferred);

                    UnityEngine.Debug.Log("response : " + stringTransferred);

                    ResponseBase result = JsonUtility.FromJson<ResponseBase>(stringTransferred);
                    resDelegate(result.isSuccess(), result.identifier, stringTransferred);

                    current += (sizeByte.Length + dataBytes.Length);

                    if (current == length)
                    {
                        socket.BeginReceive(buffer, 0, buffer.Length, SocketFlags.None, m_fnReceiveHandler, socket);
                    }
                }
            }*/
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }


        // string stringTransferred = Encoding.UTF8.GetString(buffer, 0, length);
        // Console.WriteLine("response : " + stringTransferred);
        // ResponseBase result =  JsonConvert.DeserializeObject<ResponseBase>(stringTransferred);//JsonUtility.FromJson<ResponseBase>(stringTransferred);

        // resDelegate(result.identifier, stringTransferred);

        // buffer = new byte[BUFFER_SIZE];
        // socket.BeginReceive(buffer, 0, buffer.Length, SocketFlags.None, m_fnReceiveHandler, socket);

    }

    public void sendMessage(object obj)
    {
        string msg = JsonUtility.ToJson(obj);
        UnityEngine.Debug.Log("sendMessage : " + msg);

        if (socket.Connected)
        {
            byte[] btyString = Encoding.UTF8.GetBytes(msg);
            string encodingBase64 = Convert.ToBase64String(btyString);

            byte[] resultEncoding = Encoding.UTF8.GetBytes(encodingBase64);

            //socket.BeginSend(btyString, 0, btyString.Length, SocketFlags.None, new AsyncCallback(handleSend), socket);

            SocketAsyncEventArgs socketAsyncData = new SocketAsyncEventArgs();
            socketAsyncData.SetBuffer(resultEncoding, 0, resultEncoding.Length);

            socket.SendAsync(socketAsyncData);
            //socket.BeginReceive(buffer, 0, buffer.Length, SocketFlags.None, new AsyncCallback(handleDataReceive), socket);

        }
        else
        {
            UnityEngine.Debug.Log("not connection");
        }
    }


}
