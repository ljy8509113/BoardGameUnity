using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestController : MonoBehaviour {

    public GameObject testState;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void onClick()
    {
        Debug.Log("onClick");
        if (testState.activeSelf)
        {
            Debug.Log("onClick hide");
            testState.GetComponent<TestState>().hide();
        }
        else
        {
            Debug.Log("onClick show");
            testState.GetComponent<TestState>().show();
        }
    }
}
