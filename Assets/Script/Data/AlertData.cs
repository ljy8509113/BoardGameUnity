using System;

public class AlertData
{
	public string identifier;
    public string message;
    public bool isTwoButton;
    public bool isInput;
    public Alert.ButtonResult callback;

    public AlertData(string identifier, string message, bool isTwoButton, bool isInput, Alert.ButtonResult callback){
        this.identifier = identifier;
        this.message = message;
        this.isTwoButton = isTwoButton;
        this.isInput = isInput;
        this.callback = callback;
    }
}
