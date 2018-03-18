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

    class StateInfo
    {
        GAME_STATE currentState = GAME_STATE.INIT;
        ResponseBase resInfo = null;
        public bool isUpdate = false;
        public bool isStateChange = false;

        public void setData(GAME_STATE state, ResponseBase res)
        {
            resInfo = res;
            if (currentState == state)
            {
                isUpdate = true;
            }   
            else
            {
                currentState = state;
                isStateChange = true;
            }            
        }

        public void changed()
        {
            isUpdate = false;
            isStateChange = false;
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

    void Awake()
    {
        stateChange(GAME_STATE.INIT, null);
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
                    info.changed();
                }
                else
                {
                    objectList[i].hideState();
                }

            }
        }
	}

    public void stateChange(GAME_STATE state, ResponseBase res)
    {
        info.setData(state, res);
        isUpdate = true;
    }
}
