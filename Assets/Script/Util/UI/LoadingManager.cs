using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadingManager : MonoBehaviour {

    public static string nextScene;

    float loadingTime = 5f;
    public static GameCardInfo cardInfo = null;

    [SerializeField]
    Image progressBar;
    
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
        cardInfo = resObj.cardInfo;        
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
            
            if(timer >= loadingTime)
            {
                if(op.progress >= 0.9f)
                {
                    progressBar.fillAmount = Mathf.Lerp(progressBar.fillAmount, 1f, timer);
                    if(progressBar.fillAmount == 1.0f && cardInfo != null)
                    {
                        op.allowSceneActivation = true;                        
                    }   
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
