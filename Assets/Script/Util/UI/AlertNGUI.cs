
public class AlertNGUI : BaseAlert {

    public UILabel labelMessage;
    public UISprite oneButton;
    public UISprite twoButtonCancel;
    public UISprite twoButtonAccept;

    public UILabel labelInputField;
    public UIInput inputField;


    // Use this for initialization
    public override void Start () {
		
	}
	
	// Update is called once per frame
	public override void Update () {
		
	}

    public override void showAlert()
    {

        if (data.isInput)
        {
            labelMessage.gameObject.SetActive(false);
            labelInputField.gameObject.SetActive(true);
            inputField.gameObject.SetActive(true);

            labelInputField.text = data.message;
        }
        else
        {
            labelMessage.gameObject.SetActive(true);
            labelInputField.gameObject.SetActive(false);
            inputField.gameObject.SetActive(false);

            labelMessage.text = data.message;
        }

        if (data.isTwoButton)
        {
            //2btn
            twoButtonAccept.gameObject.SetActive(true);
            twoButtonCancel.gameObject.SetActive(true);
            oneButton.gameObject.SetActive(false);
        }
        else
        {
            //1btn
            twoButtonAccept.gameObject.SetActive(false);
            twoButtonCancel.gameObject.SetActive(false);
            oneButton.gameObject.SetActive(true);
        }

        this.gameObject.SetActive(true);
        isShowing = true;
    }

    public override void hide()
    {
        this.gameObject.SetActive(false);
        isShowing = false;
        resetAlert();
    }

    public override void setData(AlertData data, ButtonResult result)
    {
        base.result = result;
        base.data = data;
    }

    public void clickOneButton()
    {
        buttonAction(true);
    }

    public void clickTwoButtonCancel()
    {
        buttonAction(false);
    }

    public void clickTwoButtonAccept()
    {
        buttonAction(true);
    }

    void buttonAction(bool isOn)
    {
        if (result != null)
        {
            result(data, isOn, inputField.value);
        }
        hide();
    }

    void resetAlert()
    {
        labelMessage.text = "";
        labelInputField.text = "";
        inputField.value = "";
    }
}
