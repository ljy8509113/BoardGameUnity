using UnityEngine;
using UnityEngine.UI;
using System;

public class LoginState : BaseState
{
    public InputField fieldEmail;
    public InputField fieldPassword;
    //public Toggle toggleAuto;

    public override void initState(ResponseBase res)
    {
        base.initState(res);
        this.gameObject.SetActive(true);
        //toggleAuto.isOn = true;
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

    public void onLogin()
    {
        string email = fieldEmail.text;
        string password = fieldPassword.text;

        if (Common.isMailCheck(email))
        {
            string passwordCryp = Security.Instance().cryption(password, false);
            Debug.Log("check email : true // password : " + passwordCryp);
            Debug.Log("password : " + Security.Instance().deCryption(passwordCryp, false));

            RequestLogin login = new RequestLogin(email, passwordCryp);
            SocketManager.Instance().sendMessage(login);
        }
        else
        {
            //Debug.Log("check email : " + false);
            showAlert("emailError", "email 형식이 올바르지 않습니다.", false, false, (AlertData data, bool isOn, string fieldText) =>
            {
            });
        }

        //SceneChanger.Instance().changeScene("game");
    }

    public void onJoin()
    {
        StateManager.Instance().changeState(GAME_STATE.JOIN, null);
    }

    public override void responseString(bool isSuccess, string identifier, string json)
    {

        ResponseLogin res = JsonUtility.FromJson<ResponseLogin>(json);
        UserManager.Instance().setData(res.email, fieldPassword.text, res.nickName);
        Debug.Log("email : " + UserManager.Instance().email);
        Debug.Log("nickName : " + UserManager.Instance().nickName);

        if (res.isSuccess())
        {
            StateManager.Instance().changeState(GAME_STATE.ROOM_LIST, null);
        }
        else
        {
            Debug.Log("loginError");
            showAlert("loginError", res.message, false, false, (AlertData data, bool isOn, string fieldText) =>
            {
            });
        }
    }
}

