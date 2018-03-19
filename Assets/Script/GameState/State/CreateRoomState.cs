using UnityEngine;
using UnityEngine.UI;

public class CreateRoomState : BaseState {

    public InputField titleField;
    public Dropdown dropDownUserCount;
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

    public override void updateState(ResponseBase res)
    {
        GameManager.Instance().stateChange(GameManager.GAME_STATE.WAITING_ROOM, res);
    }

    // Use this for initialization
    void Start () {        
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
            DialogManager.Instance.ShowSubmitDialog("제목을 입력해주세요.", (bool result) => {
            });
            return;
        }

        if (buttonToggle.isOn)
        {
            if(passwordField.text.Length < 1)
            {
                DialogManager.Instance.ShowSubmitDialog("비밀번호를 입력해주세요.", (bool result) => {
                });
                return;
            }
        }

        RequestCreateRoom cr = new RequestCreateRoom(maxUserCount, title, UserInfo.Instance().nickName, passwordField.text);
        SocketManager.Instance().sendMessage(cr);
        
    }
    
    public void onChangeValue()
    {
        Debug.Log("valueChange : " + buttonToggle.isOn);
        passwordField.enabled = buttonToggle.isOn;        
    }

}
