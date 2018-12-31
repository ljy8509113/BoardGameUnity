using UnityEngine;
using UnityEngine.UI;

public class CreateRoomState : BaseState {

    public InputField titleField;
    public Dropdown dropDownGame;
    public InputField passwordField;
    public Toggle buttonToggle;

    
    public override void initState(ResponseBase res)
    {
        this.gameObject.SetActive(true);
        buttonToggle.isOn = false;
    }
    
    public override void hideState()
    {
        this.gameObject.SetActive(false);
    }

    // public override void updateState(ResponseBase res)
    // {
    //     GameManager.Instance().stateChange(GameManager.GAME_STATE.WAITING_ROOM, res);
    // }

    // Use this for initialization
    void Start () {
        passwordField.enabled = false;
        dropDownGame.options.Clear();
	}
	
	// Update is called once per frame
	void Update () {		
	}

    public void makeRoom()
    {
        Debug.Log("title : " + titleField.text);
        Debug.Log("user : " + dropDownUserCount.options[dropDownUserCount.value].text);

        string title = titleField.text;
        int maxUserCount = int.Parse(dropDownUserCount.options[dropDownUserCount.value].text);
        
        if(title == "")
        {
            // GameManager.Instance().showAlert("제목을 입력해주세요.", false, null, false);
            showAlert("errorTitle", "제목을 입력해주세요.", false, false, (AlertData data, bool isOn, string fieldText) => {
            } );
            return;
        }

        if (buttonToggle.isOn)
        {
            if(passwordField.text.Length < 1)
            {
                // GameManager.Instance().showAlert("비밀번호를 입력해주세요.", false, null, false);
                showAlert("errorPassword", "비밀번호를 입력해주세요.", false, false, (AlertData data, bool isOn, string fieldText) => {
                } );
                return;
            }
        }

        // RequestCreateRoom cr = new RequestCreateRoom(maxUserCount, title, UserManager.Instance().nickName, passwordField.text);
        RequestCreateRoom cr = new RequestCreateRoom(titleField.text, UserManager.Instance().nickName, passwordField.text, Common.GAME_NO);
        SocketManager.Instance().sendMessage(cr);
        
    }
    
    public void onChangeValue()
    {
        Debug.Log("valueChange : " + buttonToggle.isOn);
        passwordField.enabled = buttonToggle.isOn;        
    }

    public override void responseString(bool isSuccess, string identifier, string json)
    {
        ResponseCreateRoom res = JsonUtility.FromJson<ResponseCreateRoom>(json);
        if(isSuccess){
            StateManager.Instance().changeState(GAME_STATE.WAITING_ROOM, res);
        }else{
            showAlert("errorCreate", res.message, false, false, (AlertData data, bool isOn, string fieldText) => {
                } );
        }
    }

}
