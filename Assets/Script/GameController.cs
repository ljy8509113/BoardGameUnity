using System.Collections.Generic;
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
//                        DialogManager.Instance.ShowSubmitDialog(res.message, (bool result) => {
//                        });
						showAlert(res.message, false, (bool result, string fieldText)=>{
							
						},false);
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
						showAlert(res.textMsg, true, (bool result, string fieldText) => {
                            if (result)
                            {
                                Debug.Log("isGaming YES");
                                RequestConnectionRoom req = new RequestConnectionRoom(res.roomNo, UserManager.Instance().nickName);
                            }
                            else
                            {
                                RequestRoomList list = new RequestRoomList(Common.GAME_NO, Common.LIST_COUNT);
                                SocketManager.Instance().sendMessage(list);
                            }
						},false);                        
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
						showAlert(res.message, false, (bool result, string fieldText)=>{

						},false);
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
						showAlert(res.message, false, (bool result, string fieldText)=>{

						},false);
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
						showAlert(res.message, false, (bool result, string fieldText)=>{

						},false);
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
						showAlert(res.message, false, (bool result, string fieldText)=>{

						},false);
                    }
                }
                break;
            case Common.IDENTIFIER_ROOM_USERS:
                {
                    ResponseRoomUsers res = JsonUtility.FromJson<ResponseRoomUsers>(json);
                    if (res.isSuccess())
                    {
                        GameManager.Instance().stateChange(GameManager.GAME_STATE.WAITING_ROOM, res);
                    }
                    else
                    {
                        
                    }
                }
                break;
            case Common.IDENTIFIER_OUT_ROOM:
                {
                    ResponseOutRoom res = JsonUtility.FromJson<ResponseOutRoom>(json);
                    if (res.isSuccess())
                    {
                        GameManager.Instance().stateChange(GameManager.GAME_STATE.WAITING_ROOM, res);
                    }
                    else
                    {

                    }
                }
                break;
            case Common.IDENTIFIER_READY:
                {
                    ResponseReady res = JsonUtility.FromJson<ResponseReady>(json);
                    if (res.isSuccess())
                    {
                        GameManager.Instance().stateChange(GameManager.GAME_STATE.WAITING_ROOM, res);
                    }
                    else
                    {

                    }
                }
                break;
			case Common.IDENTIFIER_ROOM_PASSWORD:
			{
				ResponseRoomPassword res = JsonUtility.FromJson<ResponseRoomPassword> (json);
				if (res.isSuccess ()) {
					RequestConnectionRoom req = new RequestConnectionRoom (res.roomNo, UserManager.Instance().nickName);
					SocketManager.Instance ().sendMessage (req);
				} else {
					showAlert(res.message, false, (bool result, string fieldText)=>{

					},false);
				}

			}
			break;
            case Common.IDENTIFIER_START:
                {
                    ResponseStart res = JsonUtility.FromJson<ResponseStart>(json);
                    if (res.isSuccess())
                    {
                        LoadingManager.LoadScene("game");
                    }
                    else
                    {

                    }
                }
                break;
            case Common.IDENTIFIER_SELECT_NUMBER:
            case Common.IDENTIFIER_TURN:
            case Common.IDENTIFIER_GAME_CARD_INFO:
            case Common.IDENTIFIER_OPEN_CARD:
            case Common.IDENTIFIER_GAME_FINISH:
                {
                    //GamePlayManager.Instance().resData(json);
                }
                break;
            
        }

    }

	public void showAlert(string message, bool isTwoButton, Alert.ButtonResult result, bool isShowField)
    {
		alert.setData(message, isTwoButton, result, isShowField);
    }

    public void hideAlert()
    {
        alert.hide();
    }

}
