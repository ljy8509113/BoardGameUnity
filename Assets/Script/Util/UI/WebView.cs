using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WebView : MonoBehaviour {

    private static WebView instance = null;
    private WebViewObject webViewObject;

    public static WebView Instance()
    {
        if (instance == null)
        {
            instance = GameObject.FindObjectOfType(typeof(WebView)) as WebView;
        }

        return instance;
    }

    // Use this for initialization 
    void Start()
    {
        initWebView();
    }
    
    // Update is called once per frame 
    void Update()
    {
        //if (Application.platform == RuntimePlatform.Android)
        //{
        //    if (Input.GetKey(KeyCode.Escape))
        //    {
        //        Destroy(webViewObject);
        //        return;
        //    }
        //}
    }

    public void initWebView()
    {
        webViewObject = (new GameObject("WebViewObject")).AddComponent<WebViewObject>();
        webViewObject.Init((msg) => {
            Debug.Log(string.Format("CallFromJS[{0}]", msg));
        });
        webViewObject.SetVisibility(true);
    }

    public void StartWebView(string url)
    {
        string strUrl = "http://192.168.0.8:8080/BoardGame/gameList.do";        
        webViewObject = (new GameObject("WebViewObject")).AddComponent<WebViewObject>();

        webViewObject.Init((msg) => {
            Debug.Log(string.Format("CallFromJS[{0}]", msg));
        });

        webViewObject.LoadURL(strUrl);
        webViewObject.SetVisibility(true);
        webViewObject.SetMargins(50, 50, 50, 50);
    }

    public void show(string url)
    {
        webViewObject.SetVisibility(true);
        webViewObject.LoadURL(url);
    }

    public void hide()
    {
        webViewObject.SetVisibility(false);
    }
}
