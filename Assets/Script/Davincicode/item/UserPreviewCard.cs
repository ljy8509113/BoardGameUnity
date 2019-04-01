using UnityEngine;

//01 23 45 67 89 1011 1213 1415 1617 1819 2021 2223 2425
//0  1  2  3  4  5    6    7    8    9    10   11   -   

public class UserPreviewCard : MonoBehaviour {

    DavinciController.ResultCallback callback;

    public UISprite oneNumber;
    public UISprite twoNumberLeft;
    public UISprite twoNumberRight;

    public GameObject attackEffect;
    public GameObject openEffect;

    public UISprite bgColor;

    Card card = new Card();
    int attackIndex = -1;
    
    bool isMy = false;
    
    void Awake()
    {
        openEffect.SetActive(false);
        attackEffect.SetActive(false);

        attackEffect.GetComponent<ParticleCallback>().setCallback(()=>{
            if(attackIndex == card.index)
            {
                openEffect.SetActive(true);
                card.isOpen = true;
                reload();
            }
            else
            {                
                finish(false);
            }            
        });
        
        openEffect.GetComponent<ParticleCallback>().setCallback(() => {
            Debug.Log("open end");
            finish(true);
        });
        
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        
	}

    void finish(bool isSuccess)
    {
        if (isSuccess)
        {
            bgColor.color = Color.white;
        }
        else
        {
            bgColor.color = Color.white;
            bgColor.gameObject.SetActive(false);
        }

        attackEffect.SetActive(false);
        openEffect.SetActive(false);
        attackIndex = -1;

        if (callback != null)
            callback(isSuccess);
    }

    public void init()
    {
        oneNumber.gameObject.SetActive(false);
        twoNumberLeft.gameObject.SetActive(false);
        twoNumberRight.gameObject.SetActive(false);
    }

    public void reload()
    {

        string color = "";
        if (card.index % 2 == 0)
        {
            color = "b_number_";
        }
        else
        {
            color = "w_number_";
        }

        if (card.isOpen || isMy)
        {
            bgColor.gameObject.SetActive(card.isOpen);
            if (card.index >= 20 && card.index < 24)
            {
                oneNumber.gameObject.SetActive(false);
                twoNumberLeft.gameObject.SetActive(true);
                twoNumberRight.gameObject.SetActive(true);

                string one = "";
                if (card.index < 22)
                {
                    one = "0";
                }
                else
                {
                    one = "1";
                }

                twoNumberLeft.spriteName = color + "1";
                twoNumberRight.spriteName = color + one;
            }
            else
            {
                oneNumber.gameObject.SetActive(true);
                twoNumberLeft.gameObject.SetActive(false);
                twoNumberRight.gameObject.SetActive(false);
                if (card.index < 24)
                {
                    oneNumber.spriteName = color + card.index / 2;
                }
                else
                {
                    oneNumber.spriteName = color + "j";
                }

            }
        }
        else
        {
            bgColor.gameObject.SetActive(false);
            oneNumber.gameObject.SetActive(true);
            twoNumberLeft.gameObject.SetActive(false);
            twoNumberRight.gameObject.SetActive(false);

            oneNumber.spriteName = color + "wait";
        }
        
    }
    
    public void setData(Card c, bool isMy)
    {
        card = c;
        this.isMy = isMy;
        //isUpdate = true;
    }

    public int getIndex()
    {
        return card.index;
    }

    public bool isOpen()
    {
        return card.isOpen;
    }

    public void selectCard()
    {
        bgColor.gameObject.SetActive(true);
        bgColor.color = Color.red;
    }

    public void attackCard(int index, DavinciController.ResultCallback callback)
    {   
        this.callback = callback;
        attackIndex = index;
        attackEffect.SetActive(true);
        //isUpdate = true;
    }

    public void openCard(DavinciController.ResultCallback callback)
    {
        Debug.Log("openCard : i : " + card.index + " / open : " + (card.isOpen == true?"t":"f"));
        this.callback = callback;
        openEffect.SetActive(true);
        reload();
    }
}
