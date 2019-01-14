using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class PlayState : BaseState {

    public enum PLAY_STATE
    {
        INIT = 0,
        WAITING = 1,
        SELECT_CARD = 2,
        GAME_OVER = 3
    }

    PLAY_STATE state;
    bool isUpdate = false;
    List<UserGameData> userList;
    List<Card> fieldCardList;
    public override void initState(ResponseBase res)
    {
        // this.gameObject.SetActive(true);
    }
    
    public override void hideState()
    {
        // this.gameObject.SetActive(false);
    }

    public override void responseString(bool isSuccess, string identifier, string json)
    {
        if(isSuccess){
            switch(identifier){
                case Common.IDENTIFIER_INIT_GAME :
                {
                    ResponseInit res = JsonUtility.FromJson<ResponseInit>(json);
                    userList = res.cardInfo.userList;
                    fieldCardList = res.cardInfo.fieldCardList;
                    state = PLAY_STATE.INIT;
                    isUpdate = true;
                }
                break;
                case Common.IDENTIFIER_SELECT_FIELD_CARD:
                {
                    ResponseSelectFieldCard res = JsonUtility.FromJson<ResponseSelectFieldCard>(json);

                }
                break;
                case Common.IDENTIFIER_SELECT_USER_CARD :
                {
                    ResponseSelectUserCard res = JsonUtility.FromJson<ResponseSelectUserCard>(json);
                    
                }
                break;
            }
        }else{
            ResponseBase res = JsonUtility.FromJson<ResponseBase>(json);
            showAlert("errorCreate", res.message, false, false, (AlertData data, bool isOn, string fieldText) => {
            } );
        }
    }

    public override void Awake(){
        base.Awake();
        //RequestInit req = new RequestInit(UserManager.Instance().roomNo);
        //SocketManager.Instance().sendMessage(req);
    }

    public override void Start () {
        base.Start();
	}
	
	// Update is called once per frame
	public override void Update () {		
        base.Update();
        if(isUpdate){
            isUpdate = false;
            switch(state){
                case PLAY_STATE.INIT :
                //ㅋㅏ드 선택
                break;
                case PLAY_STATE.WAITING:
                break;
                case PLAY_STATE.SELECT_CARD:
                break;
                case PLAY_STATE.GAME_OVER:
                break;
                default:
                break;
            }
            
        }
    }

}
