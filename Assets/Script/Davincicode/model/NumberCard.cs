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

    public NumberCard(bool isJocker, bool isOpen, int index)
    {
        setData(isJocker, isOpen, index);
    }

    public void setData(bool isJocker, bool isOpen, int index)
    {
        info.isJoker = isJocker;
		info.isOpen = isOpen;
		info.index = index;
    }

    public void onSelect()
    {
        if (selectDelegate != null)
			selectDelegate(info.index);
    }

	public bool isJocker(){
		return info.isJoker;
	}

	public bool IsOpen(){
		return info.isOpen;
	}

	public int getIndex(){
		return info.index;
	}
}
