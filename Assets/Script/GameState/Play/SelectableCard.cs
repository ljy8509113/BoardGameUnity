﻿using System.Collections;
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
        while (cardInfo.mapFieldCards.Count > 0)
        {
            int index = Random.Range(0, cardInfo.mapFieldCards.Count);
            NumberCard card = cardInfo.mapFieldCards[index];
            GameObject item = Instantiate(cardObj) as GameObject;
            NumberCard itemSource = item.GetComponent<NumberCard>();

            itemSource.setData(card.number, card.isOpen, card.index);
            itemSource.selectDelegate += selectNumber;

            cardInfo.mapFieldCards.Remove(index);

            item.transform.parent = content.transform;
        }
    }

    //public void init(Dictionary<int, NumberCard> dic)
    //{
        
        
    //}

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
