using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class ItemInfo : MonoBehaviour
{
    public TextMeshProUGUI Name;
    public TextMeshProUGUI Info;
    public Image Image;

    //public static event Action<string, string, Sprite> OnChangeInfo;

    public void Change(string returnName, string returnInfo, Sprite sprite)
    {
        Name.text = returnName;
        Info.text = returnInfo;
        Image.sprite = sprite;
    }
}
