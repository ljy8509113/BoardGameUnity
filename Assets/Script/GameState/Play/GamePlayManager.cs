using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GamePlayManager : MonoBehaviour {

    public GameObject playObj;
    
    UserGameData myData;
    List<UserGameData> userDatas = new List<UserGameData>();
    public GameCardInfo cardInfo = null;


    void Awake()
    {
        
    }

    // Use this for initialization
    void Start () {
        cardInfo = new GameCardInfo();
        cardInfo = LoadingManager.cardInfo;
        LoadingManager.cardInfo = null;

        Debug.Log("cardInfo : " + cardInfo);
    }
	
	// Update is called once per frame
	void Update () {
		
	}
    
    public void initSelectableCard(ResponseGameCardInfo res)
    {
        //selectableObj.SetActive(true);
        //SelectableCard sc = selectableObj.GetComponent<SelectableCard>();
        //sc.init(res.cardInfo.mapFieldCards);
    }

    public void resData(string res)
    {

    }   

}
