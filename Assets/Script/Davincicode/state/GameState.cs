using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class GameState : BaseState {

    public UIGrid usersBg;
    public UIPanel userObj;

    public SelectableCard selectableCard;
    public UIPanel attackPanel;

    public UIPanel initLoadingPanel;

    int gameNo;
    int roomNo;

    List<Card> fieldCards = new List<Card>();
    List<UserGameData> userList = new List<UserGameData>();

    bool isUpdate = false;
    bool isInit = false;
    
    public override void initState(ResponseBase res)
    {
        base.initState(res);
    }

    public override void hideState()
    {
        
    }

    public override void Awake()
    {
        base.Awake();
        ////alert.gameObject.SetActive(false);

        //showAlert("alertTest", "email 형식이 올바르지 않습니다. 1", false, false, (AlertData data, bool isOn, string fieldText) =>
        //{
        //    Debug.Log("alert result :  " + isOn);
        //});

        //showAlert("alertTest", "email 형식이 올바르지 않습니다. 2", true, true, (AlertData data, bool isOn, string fieldText) =>
        //{
        //    Debug.Log("alert result :  " + isOn);
        //    Debug.Log("alert result text :  " + fieldText);
        //});

        attackPanel.gameObject.SetActive(false);
        selectableCard.gameObject.SetActive(true);



        //int gameNo = DavinciCommon.gamePlayingData.gameNo;
        //int roomNo = DavinciCommon.gamePlayingData.roomNo;

        //ResponseGameCardInfo res = JsonUtility.FromJson<ResponseGameCardInfo>(DavinciCommon.gamePlayingData.json);
        //selectableCard.init(res.cardInfo.fieldCardList);

        //reloadCard(res.cardInfo);

        int gameNo = 1;
        int roomNo = 1;

        ResponseGameCardInfo res = testData();
        selectableCard.init(res.cardInfo.fieldCardList);
        initLoadingPanel.gameObject.SetActive(false);

    }

    ResponseGameCardInfo testData()
    {
        List<UserGameData> userList = new List<UserGameData>();
        for(int i=0; i<4; i++)
        {
            UserGameData data = new UserGameData();
            data.no = 0;
            if (i == 0)
            {
                data.email = "test@naver.com";
                data.nickName = "안녕";
            }
            else
            {
                data.email = "Computer"+i+"autoAI.boardGame";
                data.nickName = "컴퓨터" + i;
            }
            data.cards = new List<NumberCard>();
            data.isLose = false;
            data.isInit = false;
            userList.Add(data);
        }

        List<Card> fieldList = new List<Card>();
        for(int i = 0; i < 24; i++)
        {
            Card c = new Card();
            c.index = i;
            c.isOpen = false;
            fieldList.Add(c);
        }

        ResponseGameCardInfo info = new ResponseGameCardInfo();
        info.cardInfo = new GameCardInfo();
        info.cardInfo.userList = userList;
        info.cardInfo.fieldCardList = fieldList;

        return info;
    }

    // Use this for initialization
    public override void Start () {
        base.Start();
	}
	
	// Update is called once per frame
	public override void Update () {
        base.Update();

        if (isUpdate)
        {
            isUpdate = false;
            if (!isInit)
            {
                
            }
        }
	}

    void reloadCard(GameCardInfo info)
    {
        fieldCards = info.fieldCardList;
        userList = info.userList;

        isUpdate = true;
    }


    public override void responseString(bool isSuccess, string identifier, string json)
    {
        //throw new System.NotImplementedException();
    }
}
