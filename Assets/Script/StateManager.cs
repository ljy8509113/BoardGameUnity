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
    
    ResponseBase res;
    bool isChange = false;
    BaseState.GAME_STATE currentState = BaseState.GAME_STATE.INIT;

    [SerializeField]
    public List<BaseState> stateList;

    void Start(){
        changeState(BaseState.GAME_STATE.INIT, null);
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

    public void changeState(BaseState.GAME_STATE state, ResponseBase res){
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
