using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Text.RegularExpressions;



public class Login : MonoBehaviour
{

    public InputField fieldEmail;
    public InputField fieldPassword;
    public Toggle toggleAuto;

    Regex mailValidator = new System.Text.RegularExpressions.Regex(@"^((([a-z]|\d|[!#\$%&'\*\+\-\/=\?\^_`{\|}~]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])+(\.([a-z]|\d|[!#\$%&'\*\+\-\/=\?\^_`{\|}~]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])+)*)|((\x22)((((\x20|\x09)*(\x0d\x0a))?(\x20|\x09)+)?(([\x01-\x08\x0b\x0c\x0e-\x1f\x7f]|\x21|[\x23-\x5b]|[\x5d-\x7e]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(\\([\x01-\x09\x0b\x0c\x0d-\x7f]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF]))))*(((\x20|\x09)*(\x0d\x0a))?(\x20|\x09)+)?(\x22)))@((([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])*([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])))\.)+(([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])*([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])))\.?$");

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

        string passwordCryp = Security.Instance().cryption(password);
        Debug.Log("check email : true // password : " + passwordCryp);
        Debug.Log("password : " + Security.Instance().deCryption(passwordCryp));

        RequestLogin login = new RequestLogin(email, passwordCryp, toggleAuto.isOn);
        SocketManager.Instance().sendMessage(login);

        if ( mailValidator.IsMatch(email) )
        {
           

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

