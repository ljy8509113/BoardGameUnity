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

    public void init(Dictionary<int, NumberCard> dic)
    {
        while (dic.Count > 0)
        {
            int index = Random.Range(0, dic.Count);
            NumberCard card = dic[index];
            GameObject item = Instantiate(cardObj) as GameObject;
            NumberCard itemSource = item.GetComponent<NumberCard>();

            itemSource.setData(card.number, card.isOpen, card.index);

            //listCards.Add(new NumberCard(card.number, card.isOpen, card.index));
            dic.Remove(index);

            item.transform.parent = content.transform;            
        }

       
    }
}
