using System.Collections.Generic;
using UnityEngine;

public class SelectableCardState : BaseDavincicodeState {

    public GameObject cardObj;
    public UIGrid grid;
    public UIScrollBar scrollBar;

    List<Card> fieldCardList = new List<Card>();
    List<GameObject> cardObjList = new List<GameObject>();

    //bool isUpdate = false;
    
    public int selectCount = 0;
    int selectIndex = -1;

    int roomNo;

    public delegate void onSelectedCard(bool isOpen, int index);
    public onSelectedCard delegateSelected;

    public override void Awake()
    {
        base.Awake();
    }

    // Use this for initialization
    public override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();
    }

    public void init(List<Card> list, int roomNo)
    {
        fieldCardList = list;
        this.roomNo = roomNo;
        
        //black
        foreach (Card c in fieldCardList)
        {
            if(c.index % 2 == 0)
            {
                setListData(c);
            }            
        }

        //white
        foreach (Card c in fieldCardList)
        {
            if (c.index % 2 != 0)
            {
                setListData(c);
            }
        }
        cardObjList[0].GetComponent<UIPanel>().depth -= 1;        
    }

    void setListData(Card card)
    {
        GameObject itemObj = NGUITools.AddChild(grid.gameObject, cardObj);
        NumberCard itemSource = itemObj.GetComponent<NumberCard>();
        itemSource.setData(card.isOpen, card.index);
        itemSource.selectCallback((int index) => {
            Debug.Log("select index : " + index);
            IndicatorManager.Instance().show("로딩중입니다.");

            selectIndex = index;
            RequestSelectFieldCard req = new RequestSelectFieldCard(UserManager.Instance().email, index, roomNo);
            SocketManager.Instance().sendMessage(req);
        });
        cardObjList.Add(itemObj);
    }

    public void show(int selectCount)
    {
        base.show();
        this.selectCount = selectCount;
        reload();        
    }

    public override void hide()
    {
        base.hide();
        selectCount = 0;
        selectIndex = -1;        
    }
    
    public void setData(List<Card> fieldCardList, bool isSelected)
    {
        this.fieldCardList = fieldCardList;
        if (isSelected)
        {
            selectCount -= 1;
            reload();

            if (selectCount == 0)
            {
                delegateSelected(false, selectIndex);
                hide();
            }
        }
    }

    //public void updateData(List<Card> fieldCardList, bool isSuccess, bool isReload)
    //{
    //    this.fieldCardList = fieldCardList;
    //    if (isReload)
    //    {
    //        if (isSuccess)
    //        {
    //            selectCount -= 1;                
    //        }
    //        isUpdate = true;
    //    }
    //}

    public void reload()
    {
        Debug.Log("obj : " + cardObjList.Count + " / data : " + fieldCardList.Count );
        if(cardObjList.Count != fieldCardList.Count)
        {
            int deleteCount = Mathf.Abs(cardObjList.Count - fieldCardList.Count);
            Debug.Log("delCount : " + deleteCount);

            int last = cardObjList.Count - 1;
            int dCount = 0;

            for(int i = last; i >= 0; i--)
            {
                if(dCount < deleteCount)
                {
                    GameObject obj = cardObjList[i];
                    Destroy(obj);
                    cardObjList.Remove(obj);
                    dCount++;
                }
                else
                {
                    NumberCard card = cardObjList[i].GetComponent<NumberCard>();
                    card.setData(fieldCardList[i].isOpen, fieldCardList[i].index);                    
                }
            }

            string print = "";
            for(int i=0; i<cardObjList.Count; i++)
            {
                NumberCard card = cardObjList[i].GetComponent<NumberCard>();
                print += card.getIndex() + ", ";
            }

            Debug.Log("card : " + print);
        }        
    }

    public override void finish()
    {
        base.finish();
    }

    //void OnEnable()
    //{
    //    isUpdate = true;    
    //}

}
