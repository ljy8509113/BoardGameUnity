using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoomItem : MonoBehaviour {

    public Text txtTitle;
    public Text txtUserCount;
    public Button button;
    private int index = 0;
    public GameObject listView;

    int roomNo;
    string title;
    int currentUserCount;
    int maxUserCount;

    public delegate void onClickDelegate(int index);
    public onClickDelegate delegateClick = null;

    // Use this for initialization
    void Start () {
        button.onClick.AddListener(onClick);
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

    public void setIndex(int index)
    {
        this.index = index;
    }

    public int getIndex()
    {
        return index;
    }

    public void onClick()
    {
        delegateClick(index);
    }
}
