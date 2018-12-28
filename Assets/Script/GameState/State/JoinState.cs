using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class JoinState : BaseState {

    public InputField email;
    public InputField password;
    public InputField nickName;
    // public InputField year;
    // public InputField month;
    // public InputField day;

    public override void initState(ResponseBase res)
    {
        this.gameObject.SetActive(true);
    }

    public override void hideState()
    {
        this.gameObject.SetActive(false);
    }

    // Use this for initialization
    override void Start () {
		base.Start();
	}
	
	// Update is called once per frame
	override void Update () {
		base.Update();
	}
    
    public void autoID(){
        showAlert("autoId", "임시 계정의 경우 데이터가 동기화 되지 않습니다.", true, false, (AlertData data, bool isOn, string fieldText) => {
            if(isOn){
                if(nickName.text.Length < 2)
                {
                    showAlert("errorJoin", "닉네임은 최소 2글자 이상 입력하셔야합니다.", false, false, (AlertData data, bool isOn, string fieldText) => {
                    } );
                }else{
                    RequestJoin req = new RequestJoin("auto", nickName.text, "imsi1234");
                    SocketManager.Instance().sendMessage(req);
                }
            }
        } );
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
            // DialogManager.Instance.ShowSubmitDialog("이메일 형식이 잘못되었습니다.", (bool result) => {
            // });
            // Debug.Log("이메일 실패");
            showAlert("errorJoin", "이메일 형식이 잘못되었습니다.", false, false, (AlertData data, bool isOn, string fieldText) => {
            } );
            return;
        }
            
        if(password.text.Length < 4)
        {
            // DialogManager.Instance.ShowSubmitDialog("비밀번호는 최소 4글자 이상 입력해야합니다.", (bool result) => {
            // });
            // Debug.Log("비밀번호 실패");
            showAlert("errorJoin", "비밀번호는 최소 4글자 이상 입력해야합니다.", false, false, (AlertData data, bool isOn, string fieldText) => {
            } );
            return;
        }

        if(nickName.text.Length < 2)
        {
            // DialogManager.Instance.ShowSubmitDialog("닉네임은 최소 2글자 이상 입력하셔야합니다.", (bool result) => {
            // });
            // Debug.Log("닉네임 실패");
            showAlert("errorJoin", "닉네임은 최소 2글자 이상 입력하셔야합니다.", false, false, (AlertData data, bool isOn, string fieldText) => {
            } );
            return;
        }

        string password = Security.Instance().cryption(password.text, false);
        RequestJoin req = new RequestJoin(email.text, nickName.text, password);
        SocketManager.Instance().sendMessage(req);

        // DateTime birthDay = new DateTime();
        // string dayStr;
        // try
        // {
        //     dayStr = year.text + "-" + String.Format("{0,2}", int.Parse(month.text).ToString("D2")) + "-" + String.Format("{0,2}", int.Parse(day.text).ToString("D2"));
        //     birthDay = DateTime.ParseExact(dayStr, "yyyy-MM-dd", null);
        //     Debug.Log("birthDay : " + birthDay);

        //     RequestJoin req = new RequestJoin(email.text, nickName.text, Security.Instance().cryption(password.text, false));
        //     SocketManager.Instance().sendMessage(req);
        // }
        // catch(Exception e)
        // {
        //     Debug.Log("날짜 파싱 실패 : " + e.Message );
        //     DialogManager.Instance.ShowSubmitDialog("날짜 입력 형식이 잘못되었습니다.", (bool result) => {
        //     });
        //     return;
        // }
        
        
    }

    public void cancel()
    {
        //GameController.Instance().changeState(GameController.OBJECT_INDEX.LOGIN);
        // GameManager.Instance().stateChange(GameManager.GAME_STATE.LOGIN, null);
        StateManager.Instance().changeState(GAME_STATE.LOGIN, null);
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

    public override void responseString(bool isSuccess, string identifier, string json)
    {
        if(identifier.Equals(Common.IDENTIFIER_JOIN)){
            ResponseJoin res = JsonUtility.FromJson<ResponseJoin>(json);
            if(res.isSuccess()){
                string pw =  Security.Instance().deCryption(res.password, false);
                PlayerPrefs.SetString(Common.KEY_EMAIL, res.email);
                PlayerPrefs.SetString(Common.KEY_PASSWORD, Security.Instance().cryption(pw, true));

                RequestLogin login = new RequestLogin(res.email, res.password);
                SocketManager.Instance().sendMessage(login);
            }else{
                showAlert("errorJoin", res.message, false, false, (AlertData data, bool isOn, string fieldText) => {
                
                } );
            }
        }else if(identifier.Equals(Common.IDENTIFIER_LOGIN)){
            ResponseLogin res = JsonUtility.FromJson<ResponseLogin>(json);
            if(res.isSuccess()){
                UserManager.Instance().setData(res.email, res.nickName);
                StateManager.Instance().changeState(GAME_STATE.GAME_LIST);
            }else{
                showAlert("errorLogin", res.message, false, false, (AlertData data, bool isOn, string fieldText) => {
                
                } );
            }
        }
        
    }
}
