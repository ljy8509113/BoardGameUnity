using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GamePlayManager : MonoBehaviour {

    public GameObject rootObj;
    public GameObject selectableObj;
    
    private static GamePlayManager instance = null;
    public static GamePlayManager Instance()
    {
        if (instance == null)
        {
            instance = GameObject.FindObjectOfType(typeof(GamePlayManager)) as GamePlayManager;
        }

        return instance;
    }

    UserGameData myData;
    List<UserGameData> userDatas = new List<UserGameData>();

    void Awake()
    {
        rootObj.SetActive(false);
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void startGame(ResponseBase res)
    {
        rootObj.SetActive(true);
    }

    public void initSelectableCard(ResponseGameStart res)
    {
        selectableObj.SetActive(true);
        SelectableCard sc = selectableObj.GetComponent<SelectableCard>();
        sc.init(res.cardInfo.mapFieldCards);
    }

   

}
