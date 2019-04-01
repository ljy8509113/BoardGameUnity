using UnityEngine;
using System.Collections;

public class Turn : MonoBehaviour {

    public UILabel labelTurn;
    TypewriterEffect effect;
    DavinciController.ActionCallback callback;
    bool isNext = false;
    //float time = 0.0f;
    //bool isFinish = false;


    // Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
        //if (isFinish)
        //{
        //    time += Time.deltaTime;
        //    if(time > 1)
        //    {
        //        isFinish = false;
        //        time = 0;
        //        this.gameObject.SetActive(false);
        //    }
        //}
	}

    public void show(DavinciController.ActionCallback callback)
    {
        //labelTurn.text = nickName + "[99ff00] TURN [-]";
        isNext = true;
        if (effect == null)
            effect = labelTurn.gameObject.GetComponent<TypewriterEffect>();
        effect.ResetToBeginning();
        this.callback = callback;
        this.gameObject.SetActive(true);

    }
    
    public void onFinishAnimation()
    {
        //labelTurn.text = "";
        //isFinish = true;
        if(isNext)
            StartCoroutine(waitCallback());
    }

    IEnumerator waitCallback()
    {
        yield return new WaitForSeconds(2);
        this.callback();
        this.gameObject.SetActive(false);
    }
}
