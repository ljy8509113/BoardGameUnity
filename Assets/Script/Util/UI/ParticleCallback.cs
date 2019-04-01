using UnityEngine;

public class ParticleCallback : MonoBehaviour {

    public delegate void EndParticle();
    EndParticle callback;

    public float callbackTime;
    float time;
    bool isUpdate = false;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (isUpdate)
        {
            time += Time.deltaTime;
            if(time > callbackTime)
            {
                isUpdate = false;
                if(callback != null)
                {
                    callback();
                }
            }
        }
	}
    
    public void setCallback(EndParticle callback)
    {
        this.callback = callback;
    }

    void OnEnable()
    {
        //isCallback = false;   
        time = 0;
        isUpdate = true;
    }
}
