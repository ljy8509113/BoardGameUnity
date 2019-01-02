using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseState : MonoBehaviour {
    public enum GAME_STATE
    {
        INIT = 0,
        LOGIN,
        JOIN,
        ROOM_LIST,
        CREATE_ROOM,
        WAITING_ROOM,
        PLAYING
    }

    [SerializeField]
    public GAME_STATE state;
    public Alert alert;
    public List<AlertData> listAlert = new List<AlertData>();
    bool isShowAlert = false;
    AlertData alertData = new AlertData();
    protected BaseState(){
        SocketManager.Instance().resDelegate = responseString;
    }
    public abstract void initState(ResponseBase res);
    public abstract void hideState();
    
    public abstract void responseString(bool isSuccess, string identifier, string json);

    virtual void Awake(){
    }
    virtual void Start(){
    }

    virtual void Update(){
        if(isShowAlert && alert.isShowing == false){
            isShowAlert = false;
            if(listAlert.Count > 0){
                AlertData data = listAlert[listAlert.LastIndexOf];
                alert.setData(data, alertResult);
                alert.showAlert();
            }
        }
    }
    
    public void showAlert(string identifier, string message, bool isTwoButton, bool isInput, ButtonResult callback){
        listAlert.Add( new AlertData(identifier, message, isTwoButton, isInput, callback) );
        isShowAlert = true;
    }

    public void alertResult(AlertData data, bool isOn, string fieldText){
        
        if(data.callback != null){
            data.callback(data, isOn, fieldText);
        }

        listAlert.Remove(data);
        if(listAlert.Count > 0){
            isShowAlert = true;
        }
    }
}
