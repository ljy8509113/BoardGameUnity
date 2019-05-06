using System.Collections.Generic;
using UnityEngine;

public class AttackState : BaseDavincicodeState {

    public UILabel labelTitle;
    public UILabel labelUserName;

    List<Card> cardList = new List<Card>();
    List<GameObject> cardObjList = new List<GameObject>();
    GameObject cardObj = null;
    public UIGrid grid;

    [SerializeField]
    public List<UIButton> numberButtonList;
    
    int selectIndex = -1;

    string selectUser;
    int roomNo;

    public override void Awake()
    {
        base.Awake();
    }

    // Use this for initialization
    public override void Start () {
        base.Start();
	}

    // Update is called once per frame
    public override void Update () {
        base.Update();
	}

    public void show(int roomNo, UserGameData selectUserData)
    {
        base.show();
        this.roomNo = roomNo;
        this.selectUser = selectUserData.email;
        this.cardList = selectUserData.cards;
        labelUserName.text = selectUserData.nickName;

        if(cardObj == null){
            cardObj = (GameObject)Instantiate(Resources.Load ("Davincicode/prefab/NumberCardPrefab"));
            cardObj.SetActive(false);
        }
        
        for(int i=0; i<cardList.Count; i++){
            GameObject itemObj = NGUITools.AddChild(grid.gameObject, cardObj);
            NumberCard source = itemObj.GetComponent<NumberCard>();
            cardObjList.Add(itemObj);

            Debug.Log("cardList for : " + itemObj + "  //  index : " + source.info.index);

            source.setData(cardList[i].isOpen, cardList[i].index);
            source.selectCallback((int index) => {
                Debug.Log("selected index : " + i);
                selectIndex = index;
                foreach (GameObject obj in cardObjList)
                {
                    NumberCard src = obj.GetComponent<NumberCard>();

                    if (src.info.index == index)
                        src.setSelect(true);
                    else
                        src.setSelect(false);
                }
                setButton(true);
            });
        }
        grid.Reposition();
        // Canvas.ForceUpdateCanvases();
        
        for(int i = 0; i<cardList.Count; i++){
            Debug.Log("cardList : " + cardList[i].index);
        }
        
        Debug.Log("cardListObj count : " + cardObjList.Count);

        setButton(false);
        DavinciController.Instance().hideBlock();
    }

    public override void hide()
    {
        selectIndex = -1;
        
        int length = cardObjList.Count - 1;
        for(int i = length; i >= 0; i--){
            Debug.Log("remove in");
            Destroy(cardObjList[i]);
        }
        cardObjList.Clear();
        base.hide();
    }
    
    public void numberButton(UILabel numberLabel)
    {
        Debug.Log("label text : " + numberLabel.text);
        setButton(false);
        int numberIndex = 0;
        if (numberLabel.text.Equals("-"))
        {
            numberIndex = 24;
        }
        else
        {
            numberIndex = int.Parse(numberLabel.text) * 2;
        }

        for(int i=0; i<cardObjList.Count; i++)
        {
            NumberCard source = cardObjList[i].GetComponent<NumberCard>();
            if(selectIndex == source.info.index)
            {
                if (source.info.index % 2 != 0)
                    numberIndex += 1;

                RequestAttack req = new RequestAttack(roomNo, selectUser, i, numberIndex);
                SocketManager.Instance().sendMessage(req);
                break;
            }
        }
        
        hide();
    }

    void setButton(bool isEnable)
    {
        foreach (UIButton btn in numberButtonList)
        {
            btn.enabled = isEnable;
        }
    }

    public override void finish()
    {
        base.finish();
    }
}
