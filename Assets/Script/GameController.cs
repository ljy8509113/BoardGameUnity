using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameController : MonoBehaviour {

    //public GameObject socketManagerObj;
    //SocketManager socketManager;
    public enum OBJECT_INDEX
    {
        NONE = -1,
        CREATEROOM = 0,
        JOIN = 1,
        LOGIN = 2,
        ROOM_LIST = 3,
        SAVE_ACCOUNT = 4
    }

    class StateChange
    {
        OBJECT_INDEX index = OBJECT_INDEX.NONE;
        Dictionary<OBJECT_INDEX, GameObject> dicObject;

        string email;
        string password;
        bool isSave;
        bool isUpdate = false;
        
        public StateChange(GameObject createRoom, GameObject join, GameObject login, GameObject roomList)
        {
            dicObject = new Dictionary<OBJECT_INDEX, GameObject>();
            dicObject.Add(OBJECT_INDEX.CREATEROOM, createRoom);
            dicObject.Add(OBJECT_INDEX.JOIN, join);
            dicObject.Add(OBJECT_INDEX.LOGIN, login);
            dicObject.Add(OBJECT_INDEX.ROOM_LIST, roomList);
        }

        public void setAccount(string email, string password, bool isSave)
        {
            Debug.Log("account init");
            this.email = email;
            this.password = password;
            this.isSave = isSave;
            changeState(OBJECT_INDEX.SAVE_ACCOUNT);
            Debug.Log("account end");
        }

        public void change()
        {
            Debug.Log("chage : " + index);
            isUpdate = false;
            if(index == OBJECT_INDEX.SAVE_ACCOUNT)
            {
                PlayerPrefs.SetString(Common.KEY_EMAIL, email);
                if (isSave)
                {
                    PlayerPrefs.SetInt(Common.KEY_AUTO_LOGIN, 1);                    
                    PlayerPrefs.SetString(Common.KEY_PASSWORD, Security.Instance().cryption(password, true));
                }
                else
                {
                    PlayerPrefs.SetInt(Common.KEY_AUTO_LOGIN, 0);
                    PlayerPrefs.SetString(Common.KEY_PASSWORD, password);
                }

                RequestGamingUser gaming = new RequestGamingUser();
                SocketManager.Instance().sendMessage(gaming);
            }
            else
            {
                foreach (OBJECT_INDEX ix in dicObject.Keys)
                {
                    if (ix == index)
                        dicObject[ix].SetActive(true);
                    else
                        dicObject[ix].SetActive(false);
                }
            }
            index = OBJECT_INDEX.NONE;
        }

        public void changeState(OBJECT_INDEX index)
        {
            this.index = index;
            this.isUpdate = true;            
        }

        public void finishUpdate()
        {
            this.isUpdate = false;
        }

        public bool IsUpdate()
        {
            return this.isUpdate;
        }
        
    }


    private static GameController instance = null;
    public static GameController Instance()
    {
        if (instance == null)
        {
            instance = GameObject.FindObjectOfType(typeof(GameController)) as GameController;
        }

        return instance;
    }

    
    ListView listViewCode;

    public GameObject listView;
    public GameObject createRoom;
    public GameObject join;
    public GameObject login;

    StateChange stateChage;

    void Awake()
    {
        //socketManager = socketManagerObj.GetComponent<SocketManager>();
        listViewCode = listView.GetComponent<ListView>();
        //socketManager.resDelegate += responseString;
        SocketManager.Instance().resDelegate += responseString;
        //createRoom.SetActive(false);
    }
    // Use this for initialization
    void Start () {
        stateChage = new StateChange(createRoom, join, login, listView);

        bool isAuto = PlayerPrefs.GetInt(Common.KEY_AUTO_LOGIN) == 1 ? true : false;
        if (isAuto)
        {
            string email = PlayerPrefs.GetString(Common.KEY_EMAIL);
            string password = PlayerPrefs.GetString(Common.KEY_PASSWORD);
            string pw = Security.Instance().deCryption(password, true);
            RequestLogin req = new RequestLogin(email, Security.Instance().cryption(pw, false), true);
            SocketManager.Instance().sendMessage(req);
        }
        else
        {
            stateChage.changeState(OBJECT_INDEX.LOGIN);
        }
    }
	
	// Update is called once per frame
	void Update () {
        if (stateChage.IsUpdate() != false)
        {
            stateChage.change();
            //listView.SetActive(isActivity);
            //isChangeObj = false;
        }        
	}

    public void responseString(string identifier, string json)
    {
        switch (identifier)
        {
            case Common.IDENTIFIER_GAME_ROOM_LIST:
                {
                    ResponseRoomList roomList = JsonUtility.FromJson<ResponseRoomList>(json);
                    listViewCode.setData(roomList);
                    //listView.SetActive(true);
                    stateChage.changeState(OBJECT_INDEX.ROOM_LIST);
                }
                break;
            case Common.IDENTIFIER_GAMING_USER:
            {
                ResponseGamingUser res = JsonUtility.FromJson<ResponseGamingUser>(json);
                Debug.Log(res.isGaming + " / " +res.textMsg);
                
                if(res.isGaming == false)
                    {
                        RequestRoomList list = new RequestRoomList(Common.GAME_NO, Common.LIST_COUNT);
                        SocketManager.Instance().sendMessage(list);                        
                    }
                    else
                    {

                    }
            }
            break;

            case Common.IDENTIFIER_CREATE_ROOM:
                {
                    ResponseCreateRoom res = JsonUtility.FromJson<ResponseCreateRoom>(json);
                    
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

                }
                break;
            case Common.IDENTIFIER_JOIN:
                {
                    ResponseJoin res = JsonUtility.FromJson<ResponseJoin>(json);
                    if (res.isSuccess())
                    {
                        stateChage.changeState(OBJECT_INDEX.LOGIN);
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
                        if (res.isAutoLogin)
                        {
                            Debug.Log("login : isAuto");
                            try
                            {
                                string pw = Security.Instance().deCryption(res.password, false);
                                Debug.Log("login pw : " + pw);
                                stateChage.setAccount(res.email, pw, true);
                            }
                            catch(Exception e)
                            {
                                Debug.Log("login error : " + e.Message);
                            }
                            
                            
                        }
                        else
                        {
                            Debug.Log("login : isAuto f");
                            stateChage.setAccount("", "", false);                            
                        }
                        
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
    
    public void onCreateRoom()
    {
        createRoom.SetActive(true);
    }

    public void changeState(OBJECT_INDEX index)
    {
        stateChage.changeState(index);
    }
    
}
