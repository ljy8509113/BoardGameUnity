using System;
using UnityEngine;

public class NumberCard : MonoBehaviour
{
	Card info;
    public delegate void onSelectDelegate(int number);
    public onSelectDelegate selectDelegate;

    public NumberCard(){
		info = new Card ();
    }

    public NumberCard(int number, bool isOpen, int index)
    {
        setData(number, isOpen, index);
    }

    public void setData(int number, bool isOpen, int index)
    {
        info.number = number;
		info.isOpen = isOpen;
		info.index = index;
    }

    public void onSelect()
    {
        if (selectDelegate != null)
			selectDelegate(info.number);
    }

	public int getNumber(){
		return info.number;
	}

	public bool IsOpen(){
		return info.isOpen;
	}

	public int getIndex(){
		return info.index;
	}
}
