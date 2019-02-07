using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectableCard : MonoBehaviour {

    public GameObject cardObj;
    public UIGrid grid;
    public UIScrollBar scrollBar;

    List<Card> cardInfoList = new List<Card>();
    List<GameObject> cardObjList = new List<GameObject>();

    bool isUpdate = false;

    void Start()
    {
        
    }

    void Update()
    {
        if (isUpdate)
        {
            isUpdate = true;

        }
    }

    public void init(List<Card> list)
    {
        cardInfoList = list;
        
        //black
        foreach (Card c in cardInfoList)
        {
            if(c.index % 2 == 0)
            {
                GameObject itemObj = NGUITools.AddChild(grid.gameObject, cardObj);
                //NumberCard itemSource = itemObj.GetComponent<NumberCard>();

                //itemSource.selectDelegate += onSelect;
                //itemObj.transform.parent = grid.transform;
                //NGUITools.AddChild(grid.gameObject, itemObj);
                NumberCard itemSource = itemObj.GetComponent<NumberCard>();
                itemSource.setData(c.isOpen, c.index);
                itemSource.selectDelegate += onSelect;
                cardObjList.Add(itemObj);
                //itemObj.gameObject.SetActive(true);
            }            
        }

        //white
        foreach (Card c in cardInfoList)
        {
            if (c.index % 2 != 0)
            {
                GameObject itemObj = NGUITools.AddChild(grid.gameObject, cardObj);
                //NumberCard itemSource = itemObj.GetComponent<NumberCard>();

                //itemSource.selectDelegate += onSelect;
                //itemObj.transform.parent = grid.transform;
                
                NumberCard itemSource = itemObj.GetComponent<NumberCard>();
                itemSource.setData(c.isOpen, c.index);
                itemSource.selectDelegate += onSelect;
                cardObjList.Add(itemObj);
                //itemObj.gameObject.SetActive(true);
            }
        }

        cardObjList[0].GetComponent<UISprite>().depth -= 1;
        
    }

    public void onSelect(int index)
    {
        Debug.Log("select index : " + index);
    }

    //   public GameObject content;
    //   public GameObject cardObj;
    //List<GameObject> listCards = new List<GameObject>();
    //   List<Card> listData = new List<Card>();

    //   bool isUpdate = false;
    //   bool isShow = false;

    //   void Awake()
    //   {
    //       cardObj.SetActive(false);
    //   }

    //   // Use this for initialization
    //   void Start () {

    //}

    //// Update is called once per frame
    //void Update () {
    //       if (isUpdate)
    //       {
    //           isUpdate = false;

    //           for(int i=0; i<listCards.Count; i++)
    //           {
    //               if( i < listData.Count)
    //               {
    //                   NumberCard itemSource = listCards[i].GetComponent<NumberCard>();
    //                   itemSource.setData(listData[i].isOpen, listData[i].index);
    //                   listCards[i].SetActive(true);
    //               }
    //               else
    //               {
    //                   listCards[i].SetActive(false);
    //               }
    //           }
    //           gameObject.SetActive(isShow);
    //       }
    //}


    //   public void show(List<Card> list)
    //   {
    //       listData = list;
    //       isShow = true;
    //       isUpdate = true;
    //   }

    //   public void hide()
    //   {
    //       isShow = false;
    //       isUpdate = true;
    //   }


    //   public void init(int count)
    //   {
    //       for(int i=0; i<count; i++)
    //       {
    //           GameObject itemObj = Instantiate(cardObj) as GameObject;
    //           NumberCard itemSource = itemObj.GetComponent<NumberCard>();

    //           itemSource.selectDelegate += selectNumber;

    //           itemObj.transform.parent = content.transform;
    //           listCards.Add(itemObj);
    //           itemObj.SetActive(false);
    //       }
    //   }

    //   public void selectNumber(int number)
    //   {
    //       //RequestSelectNumber req = new RequestSelectNumber(number);
    //       //SocketManager.Instance().sendMessage(req);
    //   }

    //   public void selectResult(bool isSuccess, int number)
    //   {
    //       if (isSuccess)
    //       {
    //           GameObject obj = getCard(number);
    //		NumberCard card = obj.GetComponent<NumberCard> ();
    //       }
    //       else
    //       {

    //       }
    //   }

    //   public void removeCard(int cardNumber)
    //   {
    //       GameObject obj = getCard(cardNumber);
    //       removeCard(obj);       
    //   }

    //   public void removeCard(GameObject obj)
    //   {
    //       if (obj != null)
    //       {
    //           listCards.Remove(obj);
    //           Destroy(obj);
    //       }
    //   }

    //   GameObject getCard(int number)
    //   {
    //       foreach (GameObject obj in listCards)
    //       {
    //           NumberCard card = obj.GetComponent<NumberCard>();
    //		if (number == card.getIndex())
    //           {
    //               return obj;                
    //           }
    //       }

    //       return null;
    //   }

    //public List<E> shuffleList<E>(List<E> inputList)
    //{
    //	List<E> randomList = new List<E>();
    //	Random r = new Random();
    //	int randomIndex = 0;
    //	while (inputList.Count > 0)
    //	{
    //		//randomIndex = r.Next(0, inputList.Count); //Choose a random object in the list
    //		randomIndex = Random.Range(0, inputList.Count);
    //		randomList.Add(inputList[randomIndex]); //add it to the new, random list
    //		inputList.RemoveAt(randomIndex); //remove to avoid duplicates
    //	}
    //	return randomList; //return the new random list
    //}
}
