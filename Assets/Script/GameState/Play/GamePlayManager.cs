using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GamePlayManager : MonoBehaviour {
    
    UserGameData myData;
    List<UserGameData> userDatas = new List<UserGameData>();
    
    public SelectableCard selectableCard;
    public GameObject userCardObj;
    public GameObject playObj;
    public GameObject myCardObj;

    private static GamePlayManager instance = null;
    public static GamePlayManager Instance()
    {
        if (instance == null)
        {
            instance = GameObject.FindObjectOfType(typeof(GamePlayManager)) as GamePlayManager;
        }

        return instance;
    }

    void Awake()
    {
        
    }

    // Use this for initialization
    void Start () {
        //cardInfo = new GameCardInfo();
        //cardInfo = LoadingManager.cardInfo;
        //LoadingManager.cardInfo = null;

        //Debug.Log("cardInfo : " + cardInfo);

        selectableCard.init();
        

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
