using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectableCard : MonoBehaviour {

    public GameObject content;
    public GameObject cardObj;
    List<GameObject> listCards = new List<GameObject>();

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void init()
    {
		Dictionary<int, NumberCard> dic = CardController.Instance ().getFieldCards ();

		foreach (int key in dic.Keys) {
			NumberCard card = CardController.Instance ().getFieldCards ()[key];
			int index = Random.Range(0, dic.Count);
			GameObject itemObj = Instantiate(cardObj) as GameObject;
			NumberCard itemSource = itemObj.GetComponent<NumberCard>();

			itemSource.setData(card.number, card.isOpen, card.index);
			itemSource.selectDelegate += selectNumber;

			itemObj.transform.parent = content.transform;
			listCards.Add (itemObj);

			dic.Remove (key);
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
            NumberCard card = obj.GetComponent<NumberCard>();

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
            if (number == card.number)
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
}
