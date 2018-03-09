using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net;
using System.Text;
using System.IO;
using System;


public class HttpManager : MonoBehaviour {

    // Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void sendRequest(string url)
    {
        //HttpWebRequest myReq = (HttpWebRequest)WebRequest.Create(WWW.EscapeURL(url, Encoding.UTF8));
        try
        {
            HttpWebRequest myReq = (HttpWebRequest)WebRequest.Create(url);
            myReq.Method = "GET"; // 필요 없는듯?

            //HttpWebResponse 객체 받아옴
            HttpWebResponse wRes = (HttpWebResponse)myReq.GetResponse();

            // Response의 결과를 스트림을 생성합니다.
            Stream respGetStream = wRes.GetResponseStream();
            StreamReader readerGet = new StreamReader(respGetStream, Encoding.UTF8);

            // 생성한 스트림으로부터 string으로 변환합니다.
            string resultGet = readerGet.ReadToEnd();

            Debug.Log("result : " + resultGet);
        }
        catch(Exception e)
        {
            Debug.Log("exection : " + e.Message);
        }
        

        

    }

}
