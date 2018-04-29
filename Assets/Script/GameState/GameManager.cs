using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public enum GAME_STATE
    {
        INIT = 0,
        LOGIN,
        JOIN,
        ROOM_LIST,
        CREATE_ROOM,
        WAITING_ROOM
    }

    public Alert alert;

    class StateInfo
    {
        GAME_STATE currentState = GAME_STATE.INIT;
        ResponseBase resInfo = null;
        public bool isStateChange = false;

        public void setData(GAME_STATE state, ResponseBase res)
        {
            resInfo = res;
            if (currentState == state)
            {
                isStateChange = false;
            }   
            else
            {
                currentState = state;
                isStateChange = true;
            }            
        }
        
        public GAME_STATE getState()
        {
            return currentState;
        }

        public ResponseBase getData()
        {
            return resInfo;
        }
    }

    public Common.IP_KINDS ipKinds = Common.IP_KINDS.HOME;
    public string getIp()
    {
        switch (ipKinds)
        {
            case Common.IP_KINDS.KOITT:
                return "192.168.0.8";
            case Common.IP_KINDS.HOME:
                return "211.44.213.112";
            case Common.IP_KINDS.LAMU:
                return "192.168.0.3";

        }
        return "211.44.213.112";
    }

    private static GameManager instance = null;
    public static GameManager Instance()
    {
        if (instance == null)
        {
            instance = GameObject.FindObjectOfType(typeof(GameManager)) as GameManager;
        }

        return instance;
    }
    
    [SerializeField]
    public List<BaseState> objectList;

    StateInfo info = new StateInfo();
    bool isUpdate = false;
    string changeSceneName = null;

    void Awake()
    {
        stateChange(GAME_STATE.INIT, null);
        SocketManager.Instance();
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(isUpdate)
        {
            isUpdate = false;
            for(int i=0; i<objectList.Count; i++)
            {
                if(i == (int)info.getState())
                {
                    if(info.isStateChange)
                        objectList[i].initState(info.getData());
                    else
                        objectList[i].updateState(info.getData());                    
                }
                else
                {
                    objectList[i].hideState();
                }
            }
        }

        if (alert != null && alert.getState())
        {
            //alert.gameObject.SetActive(true);
            Debug.Log("alert update");
            alert.showAlert();
        }

        if(changeSceneName != null)
        {
            string sceneName = changeSceneName;
            changeSceneName = null;
            SceneChanger.Instance().changeScene(sceneName);            
        }
    }

    public void stateChange(GAME_STATE state, ResponseBase res)
    {
        info.setData(state, res);
        isUpdate = true;
    }

    public void showAlert(string message, bool isTwoButton, Alert.ButtonResult result, bool isShowField)
    {
        Debug.Log("showAlert");
        alert.setData(message, isTwoButton, result, isShowField);
    }

    public void hideAlert()
    {
        alert.hide();
    }

    public void changeScene(string sceneName)
    {
        changeSceneName = sceneName;
    }
}
