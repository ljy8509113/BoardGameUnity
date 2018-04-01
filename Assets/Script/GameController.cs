﻿using System.Collections.Generic;
using UnityEngine;
using System;

public class GameController : MonoBehaviour {
    
    private static GameController instance = null;
    public static GameController Instance()
    {
        if (instance == null)
        {
            instance = GameObject.FindObjectOfType(typeof(GameController)) as GameController;            
        }

        return instance;
    }

    public Alert alert;
    
    void Awake()
    {
        SocketManager.Instance().resDelegate += responseString;        
    }

    void Update()
    {
        if(alert != null && alert.getState())
        {
            alert.gameObject.SetActive(true);
        }
    }

    public void responseString(string identifier, string json)
    {
        switch (identifier)
        {
            case Common.IDENTIFIER_GAME_ROOM_LIST:
                {
                    ResponseRoomList res = JsonUtility.FromJson<ResponseRoomList>(json);
                    if (res.isSuccess()) {
                        GameManager.Instance().stateChange(GameManager.GAME_STATE.ROOM_LIST, res);
                    }
                    else
                    {
                        DialogManager.Instance.ShowSubmitDialog(res.message, (bool result) => {
                        });
                    }
                    
                }
                break;
            case Common.IDENTIFIER_GAMING_USER:
                {
                    ResponseGamingUser res = JsonUtility.FromJson<ResponseGamingUser>(json);
                    Debug.Log(res.isGaming + " / " + res.textMsg);

                    if (res.isGaming == false)
                    {
                        RequestRoomList list = new RequestRoomList(Common.GAME_NO, Common.LIST_COUNT);
                        SocketManager.Instance().sendMessage(list);
                    }
                    else
                    {
                        showAlert(res.textMsg, true, (bool result) => {
                            if (result)
                            {
                                Debug.Log("isGaming YES");
                            }
                            else
                            {
                                RequestRoomList list = new RequestRoomList(Common.GAME_NO, Common.LIST_COUNT);
                                SocketManager.Instance().sendMessage(list);
                            }
                        });
                        
                    }
                }
                break;

            case Common.IDENTIFIER_CREATE_ROOM:
                {
                    ResponseCreateRoom res = JsonUtility.FromJson<ResponseCreateRoom>(json);
                    if (res.isSuccess())
                    {
                        GameManager.Instance().stateChange(GameManager.GAME_STATE.WAITING_ROOM, res);
                    }
                    else
                    {
                        DialogManager.Instance.ShowSubmitDialog(res.message, (bool result) => {
                        });
                    }
                        
                }
                break;

            case Common.IDENTIFIER_TEST:
                {
                    ResponseTest res = JsonUtility.FromJson<ResponseTest>(json);

                }

                break;
            case Common.IDENTIFIER_CONNECT_ROOM:
                {
                    ResponseConnectionRoom res = JsonUtility.FromJson<ResponseConnectionRoom>(json);
                    if (res.isSuccess())
                    {
                        GameManager.Instance().stateChange(GameManager.GAME_STATE.WAITING_ROOM, res);
                    }else
                    {
                        DialogManager.Instance.ShowSubmitDialog(res.message, (bool result) => {
                        });
                    }

                }
                break;
            case Common.IDENTIFIER_JOIN:
                {
                    ResponseJoin res = JsonUtility.FromJson<ResponseJoin>(json);
                    if (res.isSuccess())
                    {
                        GameManager.Instance().stateChange(GameManager.GAME_STATE.JOIN, res);
                    }
                    else
                    {
                        DialogManager.Instance.ShowSubmitDialog(res.message, (bool result) => {
                        });
                    }
                }
                break;
            case Common.IDENTIFIER_LOGIN:
                {
                    ResponseLogin res = JsonUtility.FromJson<ResponseLogin>(json);
                    if (res.isSuccess())
                    {
                        GameManager.Instance().stateChange(GameManager.GAME_STATE.LOGIN, res);
                    }
                    else
                    {
                        DialogManager.Instance.ShowSubmitDialog(res.message, (bool result) => {
                            Debug.Log("login error : " + res.message);
                        });
                    }
                }
                break;

        }

    }

    public void showAlert(string message, bool isTwoButton, Alert.ButtonResult result)
    {
        alert.setData(message, isTwoButton, result);
    }

    public void hideAlert()
    {

    }

}
