using UnityEngine;

public abstract class BaseDavincicodeState : MonoBehaviour
{
    TweenScale scale;
    bool isHide = false;
    public virtual void show()
    {
        Debug.Log("show");
        this.gameObject.transform.localScale = Vector3.zero;
        scale.from = Vector3.zero;
        scale.to = Vector3.one;
        scale.ResetToBeginning();
        isHide = false;
        this.gameObject.SetActive(true);
        scale.enabled = true;
    }

    public virtual void hide()
    {
        Debug.Log("hide");
        scale.from = Vector3.one;
        scale.to = Vector3.zero;
        scale.ResetToBeginning();
        scale.enabled = true;
        isHide = true;        
    }
    
    public virtual void Awake()
    {
        scale = this.gameObject.GetComponent<TweenScale>();
        scale.enabled = false;
    }

    public virtual void Start()
    {

    }

    public virtual void Update()
    {
    }
    
    public virtual void finish()
    {
        Debug.Log("finish");
        if (isHide)
        {            
            this.gameObject.SetActive(false);
        }            
    }
}
