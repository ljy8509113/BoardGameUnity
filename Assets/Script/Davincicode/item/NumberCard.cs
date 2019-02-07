using System;
using UnityEngine;
using UnityEngine.UI;

public class NumberCard : MonoBehaviour
{
	Card info = new Card();
    public delegate void onSelectDelegate(int index);
    public onSelectDelegate selectDelegate;

    //public Text textNumber;
    //public Image backgroundImg;

    UISprite sprite;

    void Awake()
    {
        sprite = gameObject.GetComponent<UISprite>();
    }

    void Start()
    {
        UIButton btn = gameObject.GetComponent<UIButton>();
        AddOnClickEvent(this, btn, "onClick", info.index, typeof(int));
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

        if(sprite == null)
        {
            sprite = gameObject.GetComponent<UISprite>();
        }

        if(index % 2 == 0)
        {
            sprite.spriteName = "bback";
        }
        else
        {
            sprite.spriteName = "wback";
        }
        
        //backgroundImg.color = index % 2 == 0 ? Color.black : Color.white;
        //textNumber.color = index % 2 == 0 ? Color.white : Color.black;

        //string number = index / 2 + "";

        //if(index / 2 == 12)
        //{
        //    number = "-";
        //}

        //textNumber.text = number;

    }

    // 이벤트 동적할당 함수
    public void AddOnClickEvent(MonoBehaviour target, UIButton btn, string method, object value, Type type)
    {
        EventDelegate onClickEvent = new EventDelegate(target, method);

        EventDelegate.Parameter param = new EventDelegate.Parameter();
        param.value = value;
        param.expectedType = type;
        onClickEvent.parameters[0] = param;

        EventDelegate.Add(btn.onClick, onClickEvent);
    }
    
    public void onClick(int index)
    {
        if (selectDelegate != null)
            selectDelegate(index);
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
