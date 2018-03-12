using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class Login : MonoBehaviour
{

    public InputField fieldEmail;
    public InputField fieldPassword;
    public Toggle toggleAuto;

    

    // Use this for initialization
    void Start()
    {
        bool isCheck = PlayerPrefs.GetInt(Common.KEY_AUTO_LOGIN) == 1 ? true : false;

        Debug.Log("save data : " + PlayerPrefs.GetInt(Common.KEY_AUTO_LOGIN));
        Debug.Log("boolean " + isCheck);
        toggleAuto.isOn = isCheck;
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void onLogin()
    {
        string email = fieldEmail.text;
        string password = fieldPassword.text;
        
        if ( Common.isMailCheck(email) )
        {
            string passwordCryp = Security.Instance().cryption(password);
            Debug.Log("check email : true // password : " + passwordCryp);
            Debug.Log("password : " + Security.Instance().deCryption(passwordCryp));

            RequestLogin login = new RequestLogin(email, passwordCryp, toggleAuto.isOn);
            SocketManager.Instance().sendMessage(login);
        }
        else
        {
            Debug.Log("check email : " + false);
        }

    }

    public void onJoin()
    {
        WebView.Instance().show(Common.JOIN_URL);
    }

    public void onChangeValue()
    {
        PlayerPrefs.SetInt(Common.KEY_AUTO_LOGIN, toggleAuto.isOn == true ? 1 : 0);
        Debug.Log("valueChange : " + toggleAuto.isOn);

    }
    


}

