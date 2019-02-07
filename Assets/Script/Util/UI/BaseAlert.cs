using UnityEngine;

public abstract class BaseAlert : MonoBehaviour
{
    public delegate void ButtonResult(AlertData data, bool isOn, string fieldText);

    protected ButtonResult result;
    public AlertData data;
    public bool isShowing = false;

    public virtual void Awake() { }
    public virtual void Start() { }
    public virtual void Update() { }
    public abstract void showAlert();
    public abstract void hide();

    public abstract void setData(AlertData data, ButtonResult result);
}
