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
    AlertState state;

    void Awake()
    {
        state = new AlertState();
        this.gameObject.SetActive(false);
    }

    // Use this for initialization
    void Start () {
        
    }

    // Update is called once per frame
    void Update () {
        if (state.isChange)
        {
            Debug.Log("alert update in");
            state.isChange = false;
            if (state.isTwoButton)
            {
                Debug.Log("alert two");
                buttonCancel.gameObject.SetActive(true);
                buttonSubmit.gameObject.SetActive(true);
                oneButton.gameObject.SetActive(false);
                textMessage.text = state.message;
            }
            else
            {
                Debug.Log("alert one");
                buttonCancel.gameObject.SetActive(false);
                buttonSubmit.gameObject.SetActive(false);
                oneButton.gameObject.SetActive(true);
            }
            textMessage.text = state.message;
            this.gameObject.SetActive(true);
        }
	}
    
    public void setData(string message, bool isTwoButton, ButtonResult result)
    {
        this.result = result;
        state.isTwoButton = isTwoButton;
        state.message = message;
        state.isChange = true;
    }
    
    public void buttonAction(bool isOn)
    {
        if(result != null)
            result(isOn);
        hide();
    }

    public void hide()
    {
        this.gameObject.SetActive(false);
    }

    public void setState(bool isChange)
    {
        state.isChange = isChange;
    }

    public bool getState()
    {
        return state.isChange;
    }
}
