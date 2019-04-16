using UnityEngine;

public class IndicatorManager : MonoBehaviour {

    private static IndicatorManager instance = null;
    public static IndicatorManager Instance()
    {
        if (instance == null)
        {
            instance = GameObject.FindObjectOfType(typeof(IndicatorManager)) as IndicatorManager;
        }

        return instance;
    }

    public UISprite indicator;
    public UILabel labelMsg;
    public bool isShow = false;
    string message;
    bool isUpdate = false;
    // Use this for initialization
    void Start () {
        
    }
    
    float z = 0;
    float speed = 0;
    float addSpeed = 0;

    public float showingTime = 10;
    float time = 0;

    // Update is called once per frame
    void Update () {

        if (isUpdate)
        {
            isUpdate = false;
            if (isShow)
            {
                gameObject.SetActive(true);                
                if (message == null || message.Equals(""))
                {
                    labelMsg.text = "";
                    labelMsg.gameObject.SetActive(false);
                }
                else
                {
                    labelMsg.text = message;
                    labelMsg.gameObject.SetActive(true);
                }

            }
            else
            {
                gameObject.SetActive(false);                
            }           
        }

        if (isShow)
        {
            time += Time.deltaTime;
            addSpeed += Time.deltaTime * 10;
            speed += addSpeed;
            z += speed;
            if (z >= 360)
            {
                initValue();
            }
            indicator.transform.rotation = Quaternion.Euler(0, 0, z);

            if(time > showingTime)
            {
                isShow = false;
                gameObject.SetActive(false);                
            }
        }        
    }

    void initValue()
    {
        z = 0;
        speed = 0;
        addSpeed = 0;
        time = 0;
    }

    public void show(string message)
    {
        initValue();
        this.message = message;
        isShow = true;
        isUpdate = true;
        
    }

    public void hide()
    {   
        isShow = false;
        isUpdate = true;        
    }
}
