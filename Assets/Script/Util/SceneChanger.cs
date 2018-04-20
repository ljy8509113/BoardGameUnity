using UnityEngine;

public class SceneChanger : MonoBehaviour {

    private static SceneChanger instance = null;

    public static SceneChanger Instance()
    {
        if (instance == null)
            instance = GameObject.FindObjectOfType(typeof(SceneChanger)) as SceneChanger;
        
        return instance;
    }

    bool isChange = false;
    string sceneName;

    public void changeScene(string name)
    {
        sceneName = name;
        isChange = true;
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (isChange)
        {
            isChange = false;
            LoadingManager.LoadScene(sceneName);            
        }
	}
}
