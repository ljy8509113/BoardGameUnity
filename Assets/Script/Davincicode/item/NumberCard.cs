using System;
using UnityEngine;
using UnityEngine.UI;

public class NumberCard : MonoBehaviour
{
	public Card info = new Card();
    public delegate void onSelectDelegate(int index);
    public onSelectDelegate callback;
    
    public UISprite spriteSelectBg;
    public UISprite spriteNumber;

    void Awake()
    {
        spriteSelectBg.gameObject.SetActive(false);    
    }



    public void onClick()
    {
        Debug.Log("onClick : " + info.index);
        if (callback != null && info.isOpen == false)
        {
            callback(info.index);
        }            
    }

    public void setSelect(bool isSelect)
    {
        spriteSelectBg.gameObject.SetActive(isSelect);
    }
    
    public bool IsOpen()
    {
        return info.isOpen;
    }

    public int getIndex()
    {
        return info.index;
    }

    public void setData(bool isOpen, int index)
    {
        info.isOpen = isOpen;
        info.index = index;
        setSelect(false);

        string startName = "";

        if (index % 2 == 0)
            startName = "b";
        else
            startName = "w";
        
        if (isOpen)
        {
            spriteNumber.spriteName = startName + (index/2);
        }
        else
        {
            spriteNumber.spriteName = startName + "back";
        }
    }

    public void selectCallback(onSelectDelegate callback)
    {
        this.callback = callback;
    }


    //  void Awake()    {

    //      button = gameObject.GetComponent<UIButton>();
    //  }

    //  void Start()
    //  {   
    //      AddOnClickEvent(this, button, "onClick", info.index, typeof(int));
    //  }

    //  void Update()
    //  {

    //  }

    //  public void setData(bool isOpen, int index)
    //  {
    //      info.isOpen = isOpen;
    //info.index = index;

    //      if(sprite == null)
    //      {
    //          sprite = gameObject.GetComponent<UISprite>();
    //      }

    //      if(info.index % 2 == 0)
    //      {
    //          sprite.spriteName = "bback";
    //          button.normalSprite = "bback";
    //      }
    //      else
    //      {
    //          sprite.spriteName = "wback";
    //          button.normalSprite = "wback";
    //      }

    //  }

    //  // 이벤트 동적할당 함수
    //  public void AddOnClickEvent(MonoBehaviour target, UIButton btn, string method, object value, Type type)
    //  {
    //      EventDelegate onClickEvent = new EventDelegate(target, method);

    //      EventDelegate.Parameter param = new EventDelegate.Parameter();
    //      param.value = value;
    //      param.expectedType = type;
    //      onClickEvent.parameters[0] = param;

    //      EventDelegate.Add(btn.onClick, onClickEvent);
    //  }

    //  public void selectEffect(bool isSelect)
    //  {
    //      if (isSelect)
    //      {
    //          twColor.enabled = true;
    //      }
    //      else
    //      {
    //          twColor.enabled = false;
    //          sprite.color = Color.white;
    //          if (info.index % 2 == 0)
    //              sprite.spriteName = "bback";
    //          else
    //              sprite.spriteName = "wback";
    //      }        
    //  }
}
