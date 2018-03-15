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
        GAME_STATE changeState = GAME_STATE.INIT;
        ResponseBase resInfo = null;
        bool isUpdate = false;

        public void setData(GAME_STATE state, ResponseBase res)
        {
            resInfo = res;
            changeState = state;
            isUpdate = true;
        }

        public void changed()
        {
            isUpdate = false;
            currentState = changeState;            
        }

        public bool isChage()
        {
            return isUpdate;
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

    void Awake()
    {
        stateChange(GAME_STATE.INIT, null);
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(info.isChage())
        {
            info.changed();
            for(int i=0; i<objectList.Count; i++)
            {
                if(i == (int)info.getState())
                {

                    objectList[i].showState(info.getData());
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
    }
}
