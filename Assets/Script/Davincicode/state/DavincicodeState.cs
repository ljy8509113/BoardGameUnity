using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class DavincicodeState : BaseState {
    
    DavinciCommon.PLAY_STATE playState;
    bool isUpdate = false;
    List<UserGameData> userList;
    List<Card> fieldCardList;
    public GameObject loadingPanel;
    int roomNo;

    public SelectableCard selectableCard;
    public UserCardLayout userCardLayout;
    public MyCard myCard;

    public override void initState(ResponseBase res)
    {
        // this.gameObject.SetActive(true);
        base.initState(res);
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
                    //ResponseInit res = JsonUtility.FromJson<ResponseInit>(json);
                    //userList = res.cardInfo.userList;
                    //fieldCardList = res.cardInfo.fieldCardList;
                    //state = PLAY_STATE.INIT;
                    //isUpdate = true;
                }
                break;
                case DavinciCommon.IDENTIFIER_SELECT_FIELD_CARD:
                {
                    ResponseSelectFieldCard res = JsonUtility.FromJson<ResponseSelectFieldCard>(json);

                }
                break;
                case DavinciCommon.IDENTIFIER_SELECT_USER_CARD :
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
        initState(null);

        Screen.orientation = ScreenOrientation.Landscape;

        Debug.Log("gameInitJson : " + Common.gameInitJson);
        ResponseBaseDavincicode res = JsonUtility.FromJson<ResponseBaseDavincicode>(Common.gameInitJson);
        userList = res.cardInfo.userList;
        roomNo = res.roomNo;
        fieldCardList = res.cardInfo.fieldCardList;
        selectableCard.hide();
        userCardLayout.gameObject.SetActive(false);

        if (res.identifier.Equals(DavinciCommon.IDENTIFIER_START))
        {
            selectableCard.init(fieldCardList.Count);
            selectableCard.show(fieldCardList);
        }
        else if (res.identifier.Equals(DavinciCommon.IDENTIFIER_GAME_CARD_INFO))
        {

        }
        else
        {

        }

        Common.gameInitJson = "";
    }

    public override void Start () {
        base.Start();
        
    }
	
	// Update is called once per frame
	public override void Update () {		
        base.Update();
        if(isUpdate){
            isUpdate = false;
            switch(playState)
            {
                case DavinciCommon.PLAY_STATE.INIT :
                //ㅋㅏ드 선택
                break;
                case DavinciCommon.PLAY_STATE.WAITING:
                break;
                case DavinciCommon.PLAY_STATE.SELECT_CARD:
                break;
                case DavinciCommon.PLAY_STATE.GAME_OVER:
                break;
                default:
                break;
            }
            
        }
    }

}
