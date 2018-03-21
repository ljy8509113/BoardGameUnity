using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Alert : MonoBehaviour {

    class AlertState
    {
        public bool isChange = false;
        public string message;
        public bool isTwoButton = false;
    }

    public Button oneButton;
    public Button buttonCancel;
    public Button buttonSubmit;
    public Text textMessage;

    public delegate void ButtonResult(bool isOn);
    ButtonResult result;
    AlertState state = new AlertState();

    private static Alert instance = null;
    public static Alert Instance()
    {
        if (instance == null)
        {
            instance = GameObject.FindObjectOfType(typeof(Alert)) as Alert;
        }

        return instance;
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (state.isChange)
        {
            state.isChange = false;
            if (state.isTwoButton)
            {
                buttonCancel.gameObject.SetActive(true);
                buttonSubmit.gameObject.SetActive(true);
                oneButton.gameObject.SetActive(false);
                textMessage.text = state.message;
            }
            else
            {
                buttonCancel.gameObject.SetActive(false);
                buttonSubmit.gameObject.SetActive(false);
                oneButton.gameObject.SetActive(true);
            }
            textMessage.text = state.message;
            gameObject.SetActive(true);
        }
	}
    
    public void show(string message, bool isTwoButton, ButtonResult result)
    {
        this.result = result;
        state.isTwoButton = isTwoButton;
        state.message = message;
        state.isChange = true;
    }

    public void buttonAction(bool isOn)
    {
        result(isOn);
        gameObject.SetActive(false);
    }    
}
