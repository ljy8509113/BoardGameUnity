using System;
using UnityEngine;

[Serializable]
public class NumberCard : MonoBehaviour
{
    public int number;
    public bool isOpen;
    public int index;

    public NumberCard(){
    }

    public NumberCard(int number, bool isOpen, int index)
    {
        setData(number, isOpen, index);
    }

    public void setData(int number, bool isOpen, int index)
    {
        this.number = number;
        this.isOpen = isOpen;
        this.index = index;
    }
}
