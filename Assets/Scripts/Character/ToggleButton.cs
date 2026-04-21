using UnityEngine;
using UnityEngine.UI;

public class ToggleButton : MonoBehaviour
{
    public GameObject[] UIs;
    public int activeIndex = 0;
    public Image image;
    public Sprite[] sprites;

    private void OnEnable()
    {
        ToggleUI();
    }

    public void ToggleUI()
    {
        for (int i = 0; i < UIs.Length; i++)
        {
            UIs[i].SetActive(i == activeIndex);
        }

        activeIndex = (activeIndex + 1) % UIs.Length;
        image.sprite = sprites[activeIndex];
    }
}