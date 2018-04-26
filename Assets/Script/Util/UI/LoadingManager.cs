using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadingManager : MonoBehaviour {

    public static string nextScene;
    float loadingTime = 5f;
    
    [SerializeField]
    Image progressBar;

    static bool isStart = false;
    
    private void Start()
    {
        StartCoroutine(LoadScene());
        if (nextScene.Equals("game"))
        {
            if (UserManager.Instance().isMaster)
            {                
                RequestInitGame req = new RequestInitGame(UserManager.Instance().roomNo);
                SocketManager.Instance().sendMessage(req);
            }            
        }
    }
   
    public static void LoadScene(string sceneName)
    {
        nextScene = sceneName;
        SceneManager.LoadScene("loading");
    }

    public static void resMessage(string res)
    {
        ResponseInitGame resObj = JsonUtility.FromJson<ResponseInitGame>(res);
        //CardController.Instance().setCardInfo(resObj.cardInfo);
		CardController.Instance().setCardInfo(resObj.arrayUser, resObj.arrayFieldCards);
        isStart = true;
    }
    
    IEnumerator LoadScene()
    {
        yield return null;

        AsyncOperation op = SceneManager.LoadSceneAsync(nextScene);
        op.allowSceneActivation = false;

        float timer = 0.0f;
        progressBar.fillAmount = 0.0f;

        while (!op.isDone)
        {
            yield return null;

            timer += Time.deltaTime;
            
            if(op.progress >= 0.9f)
            {
				progressBar.fillAmount = Mathf.Lerp(progressBar.fillAmount, 1f, timer);
				Debug.Log ("fill : " + progressBar.fillAmount + " // " + isStart);
                if(progressBar.fillAmount == 1.0f && isStart)
                {
					Debug.Log ("next ? ");
                    isStart = false;
                    op.allowSceneActivation = true;                        
                }   
            }
            else
            {
				progressBar.fillAmount = Mathf.Lerp(progressBar.fillAmount, 0.9f, timer);
                if (progressBar.fillAmount >= op.progress)
                {
					timer = 0f;
                }
            }

            //if (op.progress >= 0.9f)
            //{
            //    progressBar.fillAmount = Mathf.Lerp(progressBar.fillAmount, 1f, timer);
            //    Debug.Log("progress if : " + op.progress);
            //    Debug.Log("progressbar fillamount : " + progressBar.fillAmount);

            //    if (progressBar.fillAmount == 1.0f)
            //        op.allowSceneActivation = true;
            //}
            //else
            //{
            //    progressBar.fillAmount = Mathf.Lerp(progressBar.fillAmount, op.progress, timer);
            //    Debug.Log("progress else : " + op.progress);
            //    Debug.Log("progressbar fillamount : " + progressBar.fillAmount);

            //    if (progressBar.fillAmount >= op.progress)
            //    {
            //        timer = 0f;
            //    }
            //}
        }
    }    
}
