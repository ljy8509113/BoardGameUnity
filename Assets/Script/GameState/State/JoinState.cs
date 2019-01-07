using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class JoinState : BaseState {

    public InputField email;
    public InputField password;
    public InputField nickName;
    
    public override void initState(ResponseBase res)
    {
        base.initState(res);
        this.gameObject.SetActive(true);
    }

    public override void hideState()
    {
        this.gameObject.SetActive(false);
    }

    // Use this for initialization
    public override void Start () {
		base.Start();
	}
	
	// Update is called once per frame
	public override void Update () {
		base.Update();
	}
    
    public void autoID(){
        showAlert("autoId", "임시 계정의 경우 데이터가 동기화 되지 않습니다.", true, false, (AlertData data, bool isOn, string fieldText) => {
            if(isOn){
                if(nickName.text.Length < 2)
                {
                    showAlert("errorJoin", "닉네임은 최소 2글자 이상 입력하셔야합니다.", false, false, (AlertData d, bool i, string ft) => {
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

        string passwordStr = Security.Instance().cryption(password.text, false);
        RequestJoin req = new RequestJoin(email.text, nickName.text, passwordStr);
        SocketManager.Instance().sendMessage(req);
        
    }

    public void cancel()
    {
        StateManager.Instance().changeState(GAME_STATE.LOGIN, null);
    }
    

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
                //string pw =  Security.Instance().deCryption(res.password, false);
                //PlayerPrefs.SetString(Common.KEY_EMAIL, res.email);
                //PlayerPrefs.SetString(Common.KEY_PASSWORD, Security.Instance().cryption(pw, true));
                UserManager.Instance().setData(res.email, password.text, res.nickName);
                RequestLogin login = new RequestLogin(res.email, res.password);
                SocketManager.Instance().sendMessage(login);
            }else{
                showAlert("errorJoin", res.message, false, false, (AlertData data, bool isOn, string fieldText) => {
                
                } );
            }
        }else if(identifier.Equals(Common.IDENTIFIER_LOGIN)){
            ResponseLogin res = JsonUtility.FromJson<ResponseLogin>(json);
            if(res.isSuccess()){
                //UserManager.Instance().setData(res.email, res.nickName);
                StateManager.Instance().changeState(BaseState.GAME_STATE.ROOM_LIST, null);
            }else{
                showAlert("errorLogin", res.message, false, false, (AlertData data, bool isOn, string fieldText) => {
                
                } );
            }
        }
        
    }
}
