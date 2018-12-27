using System;

public class AlertData
{
	public string identifier;
    string message;
    bool isTwoButton;
    bool isInput;
    public ButtonResult callback;

    public AlertData(string identifier, string message, bool isTwoButton, bool isInput, ButtonResult callback){
        this.identifier = identifier;
        this.message = message;
        this.isTwoButton = isTwoButton;
        this.isInput = isInput;
        this.callback = callback;
    }
}
