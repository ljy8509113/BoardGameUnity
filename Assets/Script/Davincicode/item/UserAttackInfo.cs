using UnityEngine;

public class UserAttackInfo : BaseDavincicodeState {
    public UISprite spriteColor;
    public UILabel labelNumber;
    
    public override void Awake()
    {
        base.Awake();        
    }

    // Use this for initialization
    public override void Start () {
        base.Start();
	}

    // Update is called once per frame
    public override void Update () {
        base.Update();
	}

    public void show(int index)
    {
        base.show();
        //scale.enabled = true;
        if (index > 23)
            labelNumber.text = "-";
        else
            labelNumber.text = index / 2 + "";
        if (index % 2 != 0)
        {
            //흰색
            labelNumber.color = spriteColor.color = Color.white;            
        }
        else
        {
            //검정
            labelNumber.color = spriteColor.color = Color.black;             
        }        
    }

    public override void hide()
    {
        base.hide();
    }
}
