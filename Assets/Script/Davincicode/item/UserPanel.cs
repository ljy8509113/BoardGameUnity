using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserPanel : MonoBehaviour {

    public UILabel nickName;

    public UIGrid grid;
    public GameObject previewObj;
    public GameObject guidBg;
    public GameObject nickNameObj;
    public UserAttackInfo attackInfo;

    public UISprite spriteLoseBg;
    public UILabel labelLose;

    public UISprite spriteBg;
    public UISprite spriteTurnOn;
    
    List<GameObject> preViewList = new List<GameObject>(); 

    public UserGameData userData;
    //bool isUpdate = false;
    
    void Awake()
    {   
    }

    // Use this for initialization
    void Start () {
        attackInfo.hide();
        preViewList.Add(previewObj);
        previewObj.GetComponent<UserPreviewCard>().init();
		for(int i=0; i<15; i++)
        {
            GameObject item = NGUITools.AddChild(grid.gameObject, previewObj);
            UserPreviewCard itemSource = item.GetComponent<UserPreviewCard>();
            itemSource.init();
            preViewList.Add(item);
        }
        setTurn(false);
	}
	
	// Update is called once per frame
	void Update () {
        //if (isUpdate)
        //{
        //    isUpdate = false;
        //    nickName.text = userData.nickName;
        //    reloadCard();

        //    if (userData.isLose)
        //    {
               
        //    }
        //}
	}

    public void setData(UserGameData data)
    {
        userData = data;
        nickName.text = userData.nickName;

        for (int i = 0; i < userData.cards.Count; i++)
        {
            UserPreviewCard preV = preViewList[i].GetComponent<UserPreviewCard>();
            preV.setData(userData.cards[i], isMy());            
        }

        if (data.isLose == false)
        {
            spriteLoseBg.gameObject.SetActive(false);
            labelLose.gameObject.SetActive(false);
        }        
    }

    public void reload()
    {
        for(int i=0; i<userData.cards.Count; i++)
        {
            UserPreviewCard preV = preViewList[i].GetComponent<UserPreviewCard>();
            preV.setData(userData.cards[i], isMy());
            preV.reload();
        }
    }

    public void offPanel()
    {
        guidBg.SetActive(false);
        nickNameObj.SetActive(false);
        spriteLoseBg.gameObject.SetActive(true);
        labelLose.gameObject.SetActive(true);
        labelLose.text = "NO User";
        setTurn(false);
    }

    public void selectCard(int index)
    {
        UserPreviewCard c = preViewList[index].GetComponent<UserPreviewCard>();
        c.selectCard();        
    }

    public void attack(int selectIndex, int attackIndex, DavinciController.ResultCallback callback)
    {
        attackInfo.show(attackIndex);
        spriteBg.color = Color.red;
        StartCoroutine(attackEffect(selectIndex, attackIndex, callback));
    }

    IEnumerator attackEffect(int selectIndex, int attackIndex, DavinciController.ResultCallback callback)
    {
        yield return new WaitForSeconds(2);
        UserPreviewCard c = preViewList[selectIndex].GetComponent<UserPreviewCard>();
        c.attackCard(attackIndex, (bool isSuccess) => {
            spriteBg.color = Color.white;
            attackInfo.hide();
            callback(isSuccess);
        });
    }
    
    public void onClick()
    {
        DavinciController.Instance().showAttack(userData);
    }

    bool isMy()
    {
        return UserManager.Instance().email.Equals(userData.email);
    }

    public void setTurn(bool isOn)
    {
        spriteTurnOn.gameObject.SetActive(isOn);
    }

    public void openCard(int index, DavinciController.ActionCallback callback)
    {
        foreach(GameObject obj in preViewList)
        {
            UserPreviewCard card = obj.GetComponent<UserPreviewCard>();
            if(card != null && card.getIndex() == index)
            {
                StartCoroutine(openCardEffect(card, callback));
                break;
            }
        }
    }

    IEnumerator openCardEffect(UserPreviewCard card, DavinciController.ActionCallback callback)
    {
        yield return new WaitForSeconds(1);

        card.openCard((bool isSuccess) => {
            callback();
        });
    }

    public void lose()
    {
        spriteLoseBg.gameObject.SetActive(true);
        labelLose.gameObject.SetActive(true);
        labelLose.text = "LOSE";
    }
}
