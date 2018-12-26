using System;

public class AlertData
{
	string identifier;
    string message;
    bool isTwoButton;
    bool isInput;

    public AlertData(string identifier, string message, bool isTwoButton, bool isInput){
        this.identifier = identifier;
        this.message = message;
        this.isTwoButton = isTwoButton;
        this.isInput = isInput;
    }
}
