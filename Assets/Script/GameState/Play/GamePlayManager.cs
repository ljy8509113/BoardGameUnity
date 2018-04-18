using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GamePlayManager : MonoBehaviour {

    public GameObject playObj;
    

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
        
    }

    // Use this for initialization
    void Start () {
		
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
