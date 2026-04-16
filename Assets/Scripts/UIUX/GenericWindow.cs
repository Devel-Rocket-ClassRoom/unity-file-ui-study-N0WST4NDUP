using UnityEngine;
using UnityEngine.EventSystems;

public class GenericWindow : MonoBehaviour
{
    public GameObject FirstSelected;

    protected WindowManager WindowManager;

    public void Init(WindowManager windowManager)
    {
        WindowManager = windowManager;
    }

    public virtual void Open()
    {
        gameObject.SetActive(true);
        EventSystem.current.SetSelectedGameObject(FirstSelected);
    }

    public virtual void Close()
    {
        gameObject.SetActive(false);
    }
}
