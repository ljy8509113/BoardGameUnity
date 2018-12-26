using System.Collections.Generic;
using UnityEngine;
using System;

public class StateManager : MonoBehaviour{
    
    private static StateManager instance = null;
    public static StateManager Instance()
    {
        if (instance == null)
        {
            instance = GameObject.FindObjectOfType(typeof(StateManager)) as StateManager;             
        }

        return instance;
    }
    
    BaseResponse res;
    bool isChange = false;
    GAME_STATE currentState = GAME_STATE.INIT;

    [SerializeField]
    public List<BaseState> stateList;
    void Start(){

    }
    void Update () {		
        if(isChange){
            isChange = false;
            for(int i=0; i<stateList.Count; i++)
            {
                if(stateList[i].state == currentState){
                    stateList[i].initState(res);
                }else{
                    stateList[i].hideState();
                }
            }
        }
    }

    public void changeState(GAME_STATE state, BaseResponse res){
        this.res = res;
        currentState = state;
        isChange = true;
    }

	//public void showAlert(string message, bool isTwoButton, Alert.ButtonResult result, bool isShowField)
 //   {
	//	alert.setData(message, isTwoButton, result, isShowField);
 //   }

 //   public void hideAlert()
 //   {
 //       alert.hide();
 //   }

}
