using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoomItem : MonoBehaviour {

    public Text txtTitle;
    public Text txtUserCount;

    int roomNo;
    string title;
    int currentUserCount;
    int maxUserCount;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void setData(string title, int maxUserCount, int currentUserCount, int roomNo)
    {
        this.roomNo = roomNo;
        this.title = title;
        this.maxUserCount = maxUserCount;
        this.currentUserCount = currentUserCount;

        txtTitle.text = title;
        txtUserCount.text = currentUserCount + "/" + maxUserCount;

    }



}
