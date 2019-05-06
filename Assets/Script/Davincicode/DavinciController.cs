using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.Collections;

public class DavinciController : BaseState {

    public delegate void ActionCallback();
    public delegate void ResultCallback(bool isSuccess);

    private static DavinciController instance = null;
    public static DavinciController Instance()
    {
        if (instance == null)
        {
            instance = GameObject.FindObjectOfType(typeof(DavinciController)) as DavinciController;
        }

        return instance;
    }

    public UserListState userListPanel;
    public SelectableCardState selectableCard;

    public UIPanel attackPanel;
    public UIPanel initLoadingPanel;

    public GameObject turnLabel;
    public GameObject blockObj;

    public Turn turnMessage;

    int gameNo;
    int roomNo;

    //List<Card> fieldCards = new List<Card>();
    //List<UserGameData> userList = new List<UserGameData>();

    bool isUpdate = false;
    public bool isInit = false;

    string resString = "";

    bool isMaster = false;
    System.Random rm = new System.Random();
    int maxIndex;
    
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

        

    }
    
    // Use this for initialization
    public override void Start () {
        base.Start();
        turnMessage.gameObject.SetActive(false);

        ResponseGameCardInfo res = JsonUtility.FromJson<ResponseGameCardInfo>(DavinciCommon.gamePlayingData.json);
        initState(res);

        IndicatorManager.Instance().hide();
        UserManager.Instance().loadData();

        selectableCard.delegateSelected += selectedCard;

        attackPanel.gameObject.SetActive(false);
        selectableCard.gameObject.SetActive(false);

        maxIndex = res.fieldCardList.Count - 1;

        roomNo = res.roomNo;

        foreach (UserGameData user in res.userList)
        {
            if (user.email.Equals(UserManager.Instance().email))
            {
                isMaster = user.userType == (int)Common.USER_TYPE.MASTER ? true : false;
            }
        }
        
        userListPanel.init(res.userList, roomNo);

        selectableCard.init(res.fieldCardList, roomNo);
        int selectCount = res.userList.Count > 3 ? 3 : 4;
        selectableCard.show(selectCount);
        initLoadingPanel.gameObject.SetActive(false);
    }
	
	// Update is called once per frame
	public override void Update () {
        base.Update();

        if (isUpdate)
        {
            isUpdate = false;
            ResponseBaseDavincicode resBase = JsonUtility.FromJson<ResponseSelectFieldCard>(resString);

            List<UserGameData> userList = resBase.userList;
            List<Card> fieldCardList = resBase.fieldCardList;
            userListPanel.setData(resBase.userList, resBase.turnUserIndex);
            selectableCard.setData(fieldCardList, false);

            switch (resBase.identifier)
            {
                case DavinciCommon.IDENTIFIER_GAME_FINISH:
                    {
                        Debug.Log("finish game");
                    }
                    break;
                case DavinciCommon.IDENTIFIER_SELECT_FIELD_CARD:
                    {
                        //ResponseSelectFieldCard res = JsonUtility.FromJson<ResponseSelectFieldCard>(resString);
                        if (resBase.isSuccess() == false)
                        {
                            selectableCard.reload();
                            showAlert(resBase.identifier, resBase.message, false, false, (AlertData data, bool isOn, string fieldText) => {
                            });
                        }
                        else
                        {
                            //selectableCard.updateData(res.fieldCardList, res.isSuccess(), selectableCard.gameObject.activeSelf);
                            userListPanel.reload();
                            if (isInit)
                            {
                                UserGameData player = resBase.userList[resBase.turnUserIndex];
                                
                                if (player.email.Equals(UserManager.Instance().email))
                                {
                                    IndicatorManager.Instance().hide();
                                    selectableCard.setData(fieldCardList, true);
                                }
                                else
                                {
                                    if (player.userType == (int)Common.USER_TYPE.COMPUTER)
                                    {
                                        if (isMaster)
                                        {
                                            //autoSelectUserCard(resBase.userList, resBase.fieldCardList, player);
                                            StartCoroutine(waitAttack(resBase.userList, resBase.fieldCardList, player));
                                        }
                                    }
                                }
                            }
                            else
                            {
                                IndicatorManager.Instance().hide();
                                selectableCard.setData(fieldCardList, true);
                            }
                        }
                    }
                    break;
                case DavinciCommon.IDENTIFIER_SELECT_USER_CARD:
                    {   
                    }
                    break;
                case DavinciCommon.IDENTIFIER_TURN:
                    {
                        if(isInit == false)
                        {
                            isInit = true;
                            userListPanel.reload();
                        }
                        
                        //ResponseTurn res = JsonUtility.FromJson<ResponseTurn>(resString);
                        UserGameData player = resBase.userList[resBase.turnUserIndex];
                        IndicatorManager.Instance().hide();
                        userListPanel.attackGuide.gameObject.SetActive(false);
                        turnMessage.gameObject.SetActive(true);
                        turnMessage.show(()=> {
                            userListPanel.setTrun(resBase.turnUserIndex);
                            if (player.email.Equals(UserManager.Instance().email))
                            {
                                IndicatorManager.Instance().hide();
                                if (resBase.fieldCardList.Count > 0)
                                    selectableCard.show(1);
                                else
                                    userListPanel.attackGuide.gameObject.SetActive(true);
                            }
                            else
                            {
                                IndicatorManager.Instance().show(player.nickName + " 님의 턴입니다.");
                                //showTurnMsg(player.nickName);

                                if (player.userType == (int)Common.USER_TYPE.COMPUTER)
                                {
                                    if (isMaster)
                                    {
                                        //autoSelectFieldCard(resBase.fieldCardList, player);
                                        if (resBase.fieldCardList.Count > 0)
                                            StartCoroutine(waitSelectFieldCard(resBase.fieldCardList, player));
                                        else
                                            StartCoroutine(waitAttack(resBase.userList, resBase.fieldCardList, player));
                                    }
                                }
                            }
                        });
                    }
                    break;
                case DavinciCommon.IDENTIFIER_ATTACK:
                    {
                        ResponseAttack res = JsonUtility.FromJson<ResponseAttack>(resString);
                        UserGameData player = res.userList[res.turnUserIndex];
                        if (res.isSuccess() == false)
                        {
                            if (player.email.Equals(UserManager.Instance().email))
                            {
                                showAlert(resBase.identifier, res.message, false, false, (AlertData data, bool isOn, string fieldText) => {
                                });
                            }
                        }
                        else
                        {
                            userListPanel.attack(res.selectUser, res.selectIndex, res.attackValue, (bool isSuccess) => {
                                Debug.Log("attack result : " + isSuccess);
                                if (isSuccess)
                                {
                                    userListPanel.reload();
                                    userListPanel.checkLose(res.selectUser);

                                    if (player.email.Equals(UserManager.Instance().email))
                                    {
                                        showAlert("select", "공격을 계속 하시겠습니까?", true, false, (AlertData data, bool isOn, string fieldText) => {
                                            if (!isOn)
                                            {
                                                RequestNext req = new RequestNext(roomNo);
                                                SocketManager.Instance().sendMessage(req);
                                            }
                                            else
                                            {
                                                userListPanel.attackGuide.gameObject.SetActive(false);
                                            }                                            
                                        });
                                    }
                                    else
                                    {
                                        if(player.userType == (int)Common.USER_TYPE.COMPUTER)
                                        {
                                            if (isMaster)
                                            {
                                                if(rm.Next(3) > 2)
                                                {
                                                    RequestNext req = new RequestNext(roomNo);
                                                    SocketManager.Instance().sendMessage(req);
                                                }
                                                else
                                                {
                                                    StartCoroutine(waitAttack(resBase.userList, resBase.fieldCardList, player));
                                                }
                                            }
                                        }
                                    }                                        
                                }
                                else
                                {
                                    userListPanel.attackGuide.gameObject.SetActive(false);
                                    userListPanel.openCard(res.turnUserIndex, res.openIndex, () => {
                                        RequestNext req = new RequestNext(roomNo);
                                        SocketManager.Instance().sendMessage(req);
                                    });
                                }
                            });
                        }
                    }
                    break;
            }            
        }

        if (IndicatorManager.Instance().isShow && IndicatorManager.Instance().gameObject.activeSelf == false)
        {
            IndicatorManager.Instance().gameObject.SetActive(true);
        }
	}
    
    public override void responseString(bool isSuccess, string identifier, string json)
    {
        resString = json; 
        isUpdate = true;        
    }

    public void selectedCard(bool isOpen, int index)
    {
        if (isOpen)
        {
            //유저카드선택
        }
        else
        {
            //필드카드선택
            if (!isInit)
            {
                RequestInit req = new RequestInit(roomNo);
                SocketManager.Instance().sendMessage(req);
                IndicatorManager.Instance().show("다른 유져의 선택을 기다립니다.");
            }
            else
            {
                //attack
                userListPanel.attackGuide.gameObject.SetActive(true);
            }
        }        
    }
    
    public void showBlock()
    {
        blockObj.SetActive(true);
    }

    public void hideBlock()
    {
        blockObj.SetActive(false);
    }


    IEnumerator waitSelectFieldCard(List<Card> fieldList, UserGameData user)
    {
        yield return new WaitForSeconds(2);
        autoSelect(fieldList, user);
    }

    void autoSelect(List<Card> fieldList, UserGameData user)
    {
        int index = rm.Next(0,fieldList.Count);

        Card c = fieldList[index];
        RequestSelectFieldCard req = new RequestSelectFieldCard(user.email, c.index, roomNo);
        SocketManager.Instance().sendMessage(req);
    }
    
    //RequestAttack autoAttack(List<UserGameData> userList, List<Card> filedCards, UserGameData player, UserGameData selectUser, int index)
    //{
    //    RequestAttack req = new RequestAttack(roomNo, selectUser.email, index, autoIndex);
    //    return req;
    //    //SocketManager.Instance().sendMessage(req);
    //}   

    IEnumerator waitAttack(List<UserGameData> userList, List<Card> filedCards, UserGameData player)
    {
        yield return new WaitForSeconds(1);
        autoAttack(userList, filedCards, player);
    }

    void autoAttack(List<UserGameData> userList, List<Card> filedCards, UserGameData player)
    {
        //선택가능한 렌덤 유저 선택
        List<UserGameData> list = new List<UserGameData>();
        foreach (UserGameData userData in userList)
        {
            if (!player.email.Equals(userData.email) && !userData.isLose)
            {
                list.Add(userData);                
            }
        }

        int selectUserIndex = rm.Next(list.Count);
        UserGameData selectUser = list[selectUserIndex];

        //해당 유저의 오픈되지 않은 카드 선택
        Card card = null;
        int selectindex = 0;
        while (card == null)
        {
            selectindex = rm.Next(selectUser.cards.Count);
            if (!selectUser.cards[selectindex].isOpen)
            {
                card = new Card();
                card.index = selectUser.cards[selectindex].index;
                card.isOpen = selectUser.cards[selectindex].isOpen;
            } 
        }
        
        //선택가능한 카드
        List<Card> totalSelectList = new List<Card>();
        bool isHolsoo = card.index % 2 != 0;
        foreach (Card c in filedCards)
        {
            if ( isHolsoo == (c.index % 2 != 0) )
            {
                totalSelectList.Add(c);
            }            
        }

        foreach (UserGameData userData in userList)
        {
            if (!player.email.Equals(userData.email) && !userData.isLose)
            {
                foreach (Card c in userData.cards)
                {
                    if (!c.isOpen)
                    {
                        if (isHolsoo == (c.index % 2 != 0))
                        {
                            totalSelectList.Add(c);
                        }                        
                    }
                }
            }
        }

        var result = from c in totalSelectList orderby c.index descending select c;
        int min = -1;
        int max = 100;

        for (int i = 0; i < selectUser.cards.Count; i++)
        {
            Card c = selectUser.cards[i];

            if (c.isOpen)
            {
                if (i < selectindex)
                {
                    min = c.index;
                }
                else
                {
                    max = c.index;
                    break;
                }
            }
        }

        List<Card> selectList = new List<Card>();
        foreach (Card c in result)
        {
            if (c.index > min && c.index < max)
            {
                selectList.Add(c);
            }
        }
        
        //조커 추가
        // if(max < DavinciCommon.JOCKER_START_INDEX)
        // {
        //     int lastIndex = totalSelectList.Count - 1;
        //     Card lastCard = totalSelectList[lastIndex];

        //     if (lastCard.index >= DavinciCommon.JOCKER_START_INDEX)
        //     {
        //         selectList.Add(lastCard);
        //     }
        // }

        int rmIndex = rm.Next(selectList.Count);
        int attackIndex = selectList[rmIndex].index;        

        Debug.Log("autoAttack selectList count : " + selectList.Count + " // rmIndex : " + rmIndex + " // attackIndex : " + attackIndex);

        RequestAttack req = new RequestAttack(roomNo, selectUser.email, selectindex, attackIndex); //autoAttack(userList, filedCards, player, selectUser, index);
        SocketManager.Instance().sendMessage(req);
    }
    
    public UserGameData getUser(string email, List<UserGameData> userList)
    {
        foreach(UserGameData user in userList)
        {
            if (user.email.Equals(email))
                return user;
        }

        return null;
    }

    public void showAttack(UserGameData user)
    {
        if(!userListPanel.getTurnUser().email.Equals(user.email)) {
            AttackState state = attackPanel.GetComponent<AttackState>();
            state.show(roomNo, user);
        }
    }    
}
