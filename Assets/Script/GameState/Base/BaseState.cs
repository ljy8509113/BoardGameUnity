using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseState : MonoBehaviour {
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
    
    public abstract void responseString(string identifier, string json);

    virtual void Start(){

    }

    virtual void Update(){
        if(isShowAlert){
            isShowAlert = false;
            if(listAlert.Count > 0){
                AlertData data = listAlert[listAlert.LastIndexOf];
                alert.setData(data.message, data.isTwoButton, alertResult, data.isInput);
                alert.showAlert();
            }
            


        }
    }

    void checkAlert(string message, bool isTwoButton){
        // public void setData(string message, bool isTwoButton, ButtonResult result, bool isShowField)
        
    }
    public void showAlert(string message, bool isTwoButton, bool isInput){
        listAlert.Add( new AlertData(message, isTwoButton, isInput) );
        isShowAlert = true;
    }

    public void alertResult(bool isOn, string fieldText){

    }
}
