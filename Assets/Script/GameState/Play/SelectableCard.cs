using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectableCard : MonoBehaviour {

    public GameObject content;
    public GameObject cardObj;
	List<GameObject> listCards;
	List<Card> listData;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void init()
    {
		listCards = new List<GameObject> ();
		listData = shuffleList (CardController.Instance ().getFieldCards ());
		Debug.Log ("list size : " + listData.Count);
		foreach(Card card in listData){
			Debug.Log ("card : " + card.number);
			GameObject itemObj = Instantiate(cardObj) as GameObject;
			NumberCard itemSource = itemObj.GetComponent<NumberCard>();

			itemSource.setData(card.number, card.isOpen, card.index);
			itemSource.selectDelegate += selectNumber;

			itemObj.transform.parent = content.transform;
			listCards.Add (itemObj);
			itemObj.SetActive (true);
		}
    }

    public void selectNumber(int number)
    {
        RequestSelectNumber req = new RequestSelectNumber(number);
        SocketManager.Instance().sendMessage(req);
    }

    public void selectResult(bool isSuccess, int number)
    {
        if (isSuccess)
        {
            GameObject obj = getCard(number);
			NumberCard card = obj.GetComponent<NumberCard> ();
        }
        else
        {

        }
    }

    public void removeCard(int cardNumber)
    {
        GameObject obj = getCard(cardNumber);
        removeCard(obj);       
    }

    public void removeCard(GameObject obj)
    {
        if (obj != null)
        {
            listCards.Remove(obj);
            Destroy(obj);
        }
    }

    GameObject getCard(int number)
    {
        foreach (GameObject obj in listCards)
        {
            NumberCard card = obj.GetComponent<NumberCard>();
			if (number == card.getNumber())
            {
                return obj;                
            }
        }

        return null;
    }

    public void show()
    {
        gameObject.SetActive(true);
    }

    public void hide()
    {
        gameObject.SetActive(false);
    }

	public List<E> shuffleList<E>(List<E> inputList)
	{
		List<E> randomList = new List<E>();
		Random r = new Random();
		int randomIndex = 0;
		while (inputList.Count > 0)
		{
			//randomIndex = r.Next(0, inputList.Count); //Choose a random object in the list
			randomIndex = Random.Range(0, inputList.Count);
			randomList.Add(inputList[randomIndex]); //add it to the new, random list
			inputList.RemoveAt(randomIndex); //remove to avoid duplicates
		}
		return randomList; //return the new random list
	}
}
