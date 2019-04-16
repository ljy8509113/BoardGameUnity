using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserListState : BaseDavincicodeState {

    public UIGrid grid;
    public GameObject userObj;
    List<GameObject> userObjList = new List<GameObject>();

    //bool isUpdate = false;

    public UILabel attackGuide;

    List<GameObject> playUserList = new List<GameObject>();
    List<UserGameData> userList = new List<UserGameData>();

    int roomNo;
    int turnUserIndex;

    bool isMyData(UserGameData data)
    {
        return data.email.Equals(UserManager.Instance().email);
    }

    public void init(List<UserGameData> list, int roomNo)
    {
        this.roomNo = roomNo;
        attackGuide.gameObject.SetActive(false);
        UserGameData myData = new UserGameData();
        foreach(UserGameData data in list)
        {
            if (isMyData(data))
            {
                myData.setData(data);
                list.Remove(data);
                break;
            }
        }

        for (int i = 0; i < 4; i++)
        {
            GameObject itemObj = NGUITools.AddChild(grid.gameObject, userObj);
            userObjList.Add(itemObj);
            UserPanel data = itemObj.GetComponent<UserPanel>();

            if (i < list.Count)
            {
                data.setData(list[i]);
                data.reload();
                playUserList.Add(userObjList[i]);
            }
            else
            {
                if(i == 3)
                {
                    data.setData(myData);
                    data.reload();
                    playUserList.Add(userObjList[i]);
                }
                else
                {
                    //userObjList[i].SetActive(false);
                    data.offPanel();
                }
            }
        }        
    }

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

    //public void updateData(List<UserGameData> userList, int turnUserIndex)
    //{
    //    this.userList = userList;
    //    isUpdate = true;
    //    this.turnUserIndex = turnUserIndex;
    //}

    public void setData(List<UserGameData> userList, int turnUserIndex)
    {
        this.userList = userList;
        this.turnUserIndex = turnUserIndex;

        foreach (UserGameData data in userList)
        {
            foreach (GameObject obj in playUserList)
            {
                UserPanel panelData = obj.GetComponent<UserPanel>();
                if (panelData.userData.email.Equals(data.email))
                {
                    panelData.setData(data);                    
                }
            }
        }
    }

    public void reload()
    {
        foreach (UserGameData data in userList)
        {
            foreach (GameObject obj in playUserList)
            {
                UserPanel panelData = obj.GetComponent<UserPanel>();
                if (panelData.userData.email.Equals(data.email))
                {
                    panelData.setData(data);
                    panelData.reload();
                }
            }
        }
    }

    public void selectCard(string email, int index)
    {
        UserPanel p = getUserPanel(email);
        p.selectCard(index);
    }
    
    UserPanel getUserPanel(string email)
    {
        foreach(GameObject obj in playUserList)
        {
            UserPanel p = obj.GetComponent<UserPanel>();
            if (p.userData.email.Equals(email))
            {
                return p;
            }
        }
        return null;
    }

    public void attack(string email, int selectIndex, int attackIndex, DavinciController.ResultCallback callback)
    {
        UserPanel p = getUserPanel(email);
        p.attack(selectIndex, attackIndex, callback);
    }

    public void setEnableClick(bool isEnable, string playerEmail)
    {
        foreach (GameObject obj in playUserList) 
        {
            UserPanel p = obj.GetComponent<UserPanel>();
            UIButton btn = p.GetComponent<UIButton>();
            if (!playerEmail.Equals(p.userData.email) && !p.userData.isLose)
            {
                btn.enabled = isEnable;
            }
            else
            {
                btn.enabled = false;
            }
        }
    }

    public override void finish()
    {
        base.finish();
    }

    public void setTrun(int turnIndex)
    {
        UserGameData user = userList[turnIndex];

        foreach(GameObject obj in playUserList)
        {
            UserPanel userPanel = obj.GetComponent<UserPanel>();
            userPanel.setTurn(userPanel.userData.email.Equals(user.email));
        }
    }

    public void openCard(int turnIndex, int cardIndex, DavinciController.ActionCallback callback)
    {
        UserGameData user = userList[turnIndex];

        foreach (GameObject obj in playUserList)
        {
            UserPanel userPanel = obj.GetComponent<UserPanel>();
            if (userPanel.userData.email.Equals(user.email))
            {
                userPanel.openCard(cardIndex, callback);
                break;
            }
        }
    }

    public void checkLose(string email)
    {
        UserPanel user = getUserPanel(email);
        if (user.userData.isLose)
        {
            user.lose();
        }
    }

    public UserGameData getTurnUser()
    {
        return userList[turnUserIndex];
    }
}
