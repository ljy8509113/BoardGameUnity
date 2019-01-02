using UnityEngine;
using UnityEngine.UI;

public class CreateRoomState : BaseState {

    public InputField titleField;
    public Dropdown dropDownGame;
    public InputField passwordField;
    public Toggle buttonToggle;
    bool isUpdate = false;
    List<ResGameData> gameList = new List<ResGameData>();
    
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
    override void Start () {
        base.Start();
        passwordField.enabled = false;
        dropDownGame.options.Clear();
        RequestGameList req = new RequestGameList();
        SocketManager.Instance().sendMessage(req);
	}
	
	// Update is called once per frame
	override void Update () {		
        base.Update();
        if(isUpdate){
            isUpdate = false;
            List<string> list = new List<string>();
            foreach(ResGameData data in gameList){
                list.Add(data.title);
            }
            dropDownGame.AddOptions(list);
        }
    }

    public void makeRoom()
    {
        Debug.Log("title : " + titleField.text);
        Debug.Log("user : " + dropDownUserCount.options[dropDownUserCount.value].text);

        string title = titleField.text;
        // int maxUserCount = int.Parse(dropDownUserCount.options[dropDownUserCount.value].text);
        string selectGame =  dropDownGame.options[dropDownGame.value].text;
        int gameNo = 0;

        foreach(ResGameData data in gameList){
            if(data.title.Equals(selectGame)){
                gameNo = data.gameNo;
                break;
            }
        }

        if(gameNo == 0){
            showAlert("errorGame", "게임을 선택해주세요.", false, false, (AlertData data, bool isOn, string fieldText) => {
            } );
            return;
        }

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
        RequestCreateRoom cr = new RequestCreateRoom(titleField.text, UserManager.Instance().nickName, passwordField.text, gameNo);
        SocketManager.Instance().sendMessage(cr);
    }
    
    public void onChangeValue()
    {
        Debug.Log("valueChange : " + buttonToggle.isOn);
        passwordField.enabled = buttonToggle.isOn;        
    }

    public override void responseString(bool isSuccess, string identifier, string json)
    {
        if(isSuccess){
            switch(identifier){
                case Common.IDENTIFIER_GANE_LIST :
                {
                    ResponseGameList res = JsonUtility.FromJson<ResponseGameList>(json);
                    gameList = res.gameList;
                    isUpdate = true;
                }
                break;
                case Common.IDENTIFIER_CREATE_ROOM:
                {
                    ResponseCreateRoom res = JsonUtility.FromJson<ResponseCreateRoom>(json);
                    StateManager.Instance().changeState(GAME_STATE.WAITING_ROOM, res);
                }
                break;
            }
        }else{
            showAlert("errorCreate", res.message, false, false, (AlertData data, bool isOn, string fieldText) => {
                } );
        }
    }

}
