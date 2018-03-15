using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class JoinState : BaseState {

    public InputField email;
    public InputField password;
    public InputField nickName;
    public InputField year;
    public InputField month;
    public InputField day;

    override public void showState(ResponseBase res)
    {
        this.gameObject.SetActive(true);
    }

    override public void hideState()
    {
        this.gameObject.SetActive(false);
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    
    public void onJoin()
    {
        //email.text = "test1@gmail.com";
        //password.text = "1234";
        //nickName.text = "테스터1";
        //year.text = "1985";
        //month.text = "10";
        //day.text = "21";

        if (Common.isMailCheck(email.text) == false)
        {
            DialogManager.Instance.ShowSubmitDialog("이메일 형식이 잘못되었습니다.", (bool result) => {
            });
            Debug.Log("이메일 실패");
            return;
        }
            
        if(password.text.Length < 4)
        {
            DialogManager.Instance.ShowSubmitDialog("비밀번호는 최소 4글자 이상 입력해야합니다.", (bool result) => {
            });
            Debug.Log("비밀번호 실패");
            return;
        }

        if(nickName.text.Length < 2)
        {
            DialogManager.Instance.ShowSubmitDialog("닉네임은 최소 2글자 이상 입력하셔야합니다.", (bool result) => {
            });
            Debug.Log("닉네임 실패");
            return;
        }


        DateTime birthDay = new DateTime();
        string dayStr;
        try
        {
            dayStr = year.text + "-" + String.Format("{0,2}", int.Parse(month.text).ToString("D2")) + "-" + String.Format("{0,2}", int.Parse(day.text).ToString("D2"));
            birthDay = DateTime.ParseExact(dayStr, "yyyy-MM-dd", null);
            Debug.Log("birthDay : " + birthDay);

            RequestJoin req = new RequestJoin(email.text, Security.Instance().cryption(password.text, false), nickName.text, birthDay);
            SocketManager.Instance().sendMessage(req);
        }
        catch(Exception e)
        {
            Debug.Log("날짜 파싱 실패 : " + e.Message );
            DialogManager.Instance.ShowSubmitDialog("날짜 입력 형식이 잘못되었습니다.", (bool result) => {
            });
            return;
        }
        
        
    }

    public void cancel()
    {
        //GameController.Instance().changeState(GameController.OBJECT_INDEX.LOGIN);
        GameManager.Instance().stateChange(GameManager.GAME_STATE.LOGIN, null);
    }
    
    //void OnGUI()
    //{
    //    if (GUILayout.Button("aaa", GUILayout.MinWidth(200), GUILayout.MinHeight(100)))
    //    {
    //        DialogManager.Instance.ShowSelectDialog("aaa", (bool result) => {
    //            Debug.Log("aaa" + result);
    //        });
    //    }
    //    if (GUILayout.Button("bbb", GUILayout.MinWidth(200), GUILayout.MinHeight(100)))
    //    {
    //        DialogManager.Instance.ShowSelectDialog("b title", "bbb", (bool result) => {
    //            Debug.Log("bbb" + result);
    //        });
    //    }
    //    if (GUILayout.Button("ccc", GUILayout.MinWidth(200), GUILayout.MinHeight(100)))
    //    {
    //        DialogManager.Instance.ShowSubmitDialog("ccc", (bool result) => {
    //            Debug.Log("ccc");
    //        });
    //    }
    //    if (GUILayout.Button("ddd", GUILayout.MinWidth(200), GUILayout.MinHeight(100)))
    //    {
    //        DialogManager.Instance.ShowSubmitDialog("d title", "ddd", (bool result) => {
    //            Debug.Log("ddd");
    //        });
    //    }
    //    if (GUILayout.Button("eee auto dissmiss", GUILayout.MinWidth(200), GUILayout.MinHeight(100)))
    //    {
    //        int id = DialogManager.Instance.ShowSelectDialog("eee", (bool result) => {
    //            Debug.Log("eee" + result);
    //        });
    //        StartCoroutine(dissmiss(id, 3f));
    //    }
    //}

    IEnumerator dissmiss(int id, float time)
    {
        yield return new WaitForSeconds(time);
        DialogManager.Instance.DissmissDialog(id);
    }
}
