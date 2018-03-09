using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MakeRoom : MonoBehaviour {

    public InputField titleFile;
    public Dropdown dropDownUserCount;
    //public GameObject socketObj;
    //SocketManager socketManager;

	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void makeRoom()
    {
        Debug.Log("title : " + titleFile.text);
        Debug.Log("user : " + dropDownUserCount.options[dropDownUserCount.value].text);

        string title = titleFile.text;
        int maxUserCount = int.Parse(dropDownUserCount.options[dropDownUserCount.value].text);

        RequestCreateRoom cr = new RequestCreateRoom(maxUserCount, title);
        SocketManager.Instance().sendMessage(cr);

    }

    public void sendMessage()
    {
        RequestTest test = new RequestTest();
        SocketManager.Instance().sendMessage(test);
    }

}
