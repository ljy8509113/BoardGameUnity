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
    
    //string ip = "192.168.0.8";
    static string ip = "211.201.206.24";
    static int port = 8895;
    private AsyncCallback m_fnReceiveHandler;
    public byte[] buffer = new byte[1024];
    Socket socket;

    public GameObject gameObj;
    GameController gameController;
    
    void Awake()
    {
        socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        gameController = gameObj.GetComponent<GameController>();
        try
        {
            Debug.Log("awake");

            //socket.Connect(ip, port);
            IPAddress ipAddress = IPAddress.Parse(ip);
            IPEndPoint endPoint = new IPEndPoint(ipAddress, port);
            //socket.BeginConnect(endPoint, new AsyncCallback(ConnectCallback), socket);
            //socket.Blocking = false;
            m_fnReceiveHandler = new AsyncCallback(handleDataReceive);
            try
            {
                socket.Connect(ip, port);
            }
            catch(SocketException e)
            {
                Debug.Log("socket error : " + e);
            }
            
            socket.BeginReceive(buffer, 0, buffer.Length, SocketFlags.None, m_fnReceiveHandler, socket);

        }
        catch (Exception e)
        {
            Debug.Log("socket connection fail : " + e.Message);

        }
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
            Debug.Log("Socket connected to " + socket.RemoteEndPoint.ToString());
            
        }
        catch (Exception e)
        {
            Debug.Log(e.ToString());
        }
    }


    public void handleDataReceive(IAsyncResult ar)
    {
        socket = (Socket)ar.AsyncState;
        int length = 0;

        try
        {
            length = socket.EndReceive(ar);
            Debug.Log("1 receive : " + length);            
        }
        catch (Exception e)
        {
            Debug.Log(e);
        }
        
        string stringTransferred = Encoding.UTF8.GetString(buffer, 0, length);
        BaseResponse result = JsonUtility.FromJson<BaseResponse>(stringTransferred); 

        if (result.resCode.Equals("0"))
        {
            gameController.responseString(result.identifier, stringTransferred);            
        }
        else
        {
            Debug.Log("error : " + result.message);
        }
        
        buffer = new byte[1024];
        socket.BeginReceive(buffer, 0, buffer.Length, SocketFlags.None, m_fnReceiveHandler, socket);
        
    }


    public void onClick()
    {
        RequestRoomList list = new RequestRoomList();
        sendMessage(list);
    }

    public void sendMessage(object obj)
    {
        string msg = JsonUtility.ToJson(obj);

        if (socket.Connected)
        {
            byte[] btyString = Encoding.UTF8.GetBytes(msg);
            //socket.BeginSend(btyString, 0, btyString.Length, SocketFlags.None, new AsyncCallback(handleSend), socket);

            SocketAsyncEventArgs socketAsyncData = new SocketAsyncEventArgs();
            socketAsyncData.SetBuffer(btyString, 0, btyString.Length);

            socket.SendAsync(socketAsyncData);
            //socket.BeginReceive(buffer, 0, buffer.Length, SocketFlags.None, new AsyncCallback(handleDataReceive), socket);

        }
        else
        {
            Debug.Log("not connection");
        }
    }


}
