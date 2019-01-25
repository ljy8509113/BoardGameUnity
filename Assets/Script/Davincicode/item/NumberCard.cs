using System;
using UnityEngine;
using UnityEngine.UI;

public class NumberCard : MonoBehaviour
{
	Card info = new Card();
    public delegate void onSelectDelegate(int number);
    public onSelectDelegate selectDelegate;

    public Text textNumber;
    public Image backgroundImg;


    void Awake()
    {
        
    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    //public NumberCard(bool isJocker, bool isOpen, int index)
    //{
    //    setData(isJocker, isOpen, index);
    //}

    public void setData(bool isOpen, int index)
    {
        info.isOpen = isOpen;
		info.index = index;

        backgroundImg.color = index % 2 == 0 ? Color.black : Color.white;
        textNumber.color = index % 2 == 0 ? Color.white : Color.black;

        string number = index / 2 + "";

        if(index / 2 == 12)
        {
            number = "-";
        }

        textNumber.text = number;

    }

    public void onSelect()
    {
        if (selectDelegate != null)
			selectDelegate(info.index);
    }
    
	public bool IsOpen(){
		return info.isOpen;
	}

	public int getIndex(){
		return info.index;
	}
}
