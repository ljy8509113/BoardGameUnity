using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Alert : MonoBehaviour {

    // class AlertState
    // {
    //     public bool isChange = false;
    //     public string message;
    //     public bool isTwoButton = false;
	// 	public bool isShowField = false;
    // }

    public Button oneButton;
    public Button buttonCancel;
    public Button buttonSubmit;
    public Text textMessage;
	public InputField field;

	public delegate void ButtonResult(AlertData data, bool isOn, string fieldText);

    ButtonResult result;
    // AlertState state;
    public AlertData data;
    public bool isShowing = false;
    void Awake()
    {
        // state = new AlertState();
        data = new AlertData();
        this.gameObject.SetActive(false);
    }

    // Use this for initialization
    void Start () {
        
    }

    // Update is called once per frame
    void Update () {
   //     if (state.isChange)
   //     {
   //         Debug.Log("alert update in");
   //         state.isChange = false;
   //         if (state.isTwoButton)
   //         {
   //             Debug.Log("alert two");
   //             buttonCancel.gameObject.SetActive(true);
   //             buttonSubmit.gameObject.SetActive(true);
   //             oneButton.gameObject.SetActive(false);
   //             textMessage.text = state.message;
   //         }
   //         else
   //         {
   //             Debug.Log("alert one");
   //             buttonCancel.gameObject.SetActive(false);
   //             buttonSubmit.gameObject.SetActive(false);
   //             oneButton.gameObject.SetActive(true);
   //         }
   //         textMessage.text = state.message;
			//field.gameObject.SetActive (state.isShowField);
   //         this.gameObject.SetActive(true);
   //     }
	}
    
    public void showAlert()
    {
            // Debug.Log("alert update in");
            // state.isChange = false;
            // if (state.isTwoButton)
            // {
            //     Debug.Log("alert two");
            //     buttonCancel.gameObject.SetActive(true);
            //     buttonSubmit.gameObject.SetActive(true);
            //     oneButton.gameObject.SetActive(false);
            //     textMessage.text = state.message;
            // }
            // else
            // {
            //     Debug.Log("alert one");
            //     buttonCancel.gameObject.SetActive(false);
            //     buttonSubmit.gameObject.SetActive(false);
            //     oneButton.gameObject.SetActive(true);
            // }
            // textMessage.text = state.message;
            // field.gameObject.SetActive(state.isShowField);
            // this.gameObject.SetActive(true);
        if(data.isTwoButton){
            //2btn
            buttonCancel.gameObject.SetActive(true);
            buttonSubmit.gameObject.SetActive(true);
            oneButton.gameObject.SetActive(false);
        }else{
            //1btn
            buttonCancel.gameObject.SetActive(false);
            buttonSubmit.gameObject.SetActive(false);
            oneButton.gameObject.SetActive(true);
        }
        textMessage.text = data.message;
        field.gameObject.SetActive(data.isShowField);
        this.gameObject.SetActive(true);
        isShowing = true;
    }

	public void setData(AlertData data, ButtonResult result)
    {
        this.result = result;
        this.data = data;
        // state.isTwoButton = isTwoButton;
        // state.message = message;
		// state.isShowField = isShowField;
		// state.isChange = true;

    }
    
    public void buttonAction(bool isOn)
    {
        resetAlert ();
        hide();
		if (result != null) {
			result(data, isOn, field.text);
		}
    }

    public void hide()
    {
        this.gameObject.SetActive(false);
        isShowing = false;
    }

    // public void setState(bool isChange)
    // {
        // state.isChange = isChange;
    // }

    // public bool getState()
    // {
        // return state.isChange;
    // }

	void resetAlert(){
		// state.isChange = false;
		// state.isTwoButton = false;
		// state.message = "";
		// state.isShowField = false;

		oneButton.gameObject.SetActive(true);
		buttonCancel.gameObject.SetActive(false);
		buttonSubmit.gameObject.SetActive(false);
		textMessage.text = "";
		field.text = "";
		field.gameObject.SetActive (false);

	}
}
