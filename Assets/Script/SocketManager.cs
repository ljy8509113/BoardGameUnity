﻿using UnityEngine;
using System.Net.Sockets;
using System;
using System.Text;
using System.Net;

public class SocketManager : MonoBehaviour
{
    public enum IP_KINDS
    {
        KOITT = 0,
        HOME = 1
    }

    public IP_KINDS ipKinds;
    public string getIp()
    {
        switch (ipKinds)
        {
            case IP_KINDS.KOITT:
                return "192.168.0.8";
            case IP_KINDS.HOME:
                return "211.201.206.24";

        }
        return "211.201.206.24";
    }

    private static SocketManager instance = null;
    public static SocketManager Instance()
    {
        if (instance == null)
        {
            instance = GameObject.FindObjectOfType(typeof(SocketManager)) as SocketManager;
        }

        return instance;
    }
    
    static int port = 8895;
    private AsyncCallback m_fnReceiveHandler;
    const int BUFFER_SIZE = 2048; 
    byte[] buffer = new byte[BUFFER_SIZE];
    Socket socket;
    
    public delegate void ResponseResultDelegate(string identifier, string result);
    public ResponseResultDelegate resDelegate = null;
    
    void Awake()
    {
        socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        
        try
        {
            Debug.Log("awake");

            //socket.Connect(ip, port);
            IPAddress ipAddress = IPAddress.Parse(getIp());
            IPEndPoint endPoint = new IPEndPoint(ipAddress, port);
            
            //socket.Blocking = false;
            m_fnReceiveHandler = new AsyncCallback(handleDataReceive);
            try
            {
                socket.BeginConnect(endPoint, new AsyncCallback(ConnectCallback), socket);
                //socket.Connect(ip, port);
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
        Debug.Log("response : " + stringTransferred);
        ResponseBase result = JsonUtility.FromJson<ResponseBase>(stringTransferred);
        

        if (result.resCode == 0)
        {
            resDelegate(result.identifier, stringTransferred);
        }
        else
        {
            Debug.Log("error : " + result.message);
        }
        
        buffer = new byte[BUFFER_SIZE];
        socket.BeginReceive(buffer, 0, buffer.Length, SocketFlags.None, m_fnReceiveHandler, socket);
        
    }
    
    public void sendMessage(object obj)
    {
        string msg = JsonUtility.ToJson(obj);
        Debug.Log("sendMessage : " + msg);
        
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
            Debug.Log("not connection");
        }
    }


}
