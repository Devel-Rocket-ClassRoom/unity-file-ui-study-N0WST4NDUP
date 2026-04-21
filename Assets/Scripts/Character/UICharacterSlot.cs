using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UICharacterSlot : MonoBehaviour
{
    public int slotIndex = -1;

    public Image imageIcon;
    public Button button;
    public SaveCharacterData SaveCharacterData { get; private set; }

    public void SetEmpty()
    {
        imageIcon.sprite = null;
        SaveCharacterData = null;
    }

    public void SetCharacter(SaveCharacterData data)
    {
        SaveCharacterData = data;
        imageIcon.sprite = SaveCharacterData.CharacterData.SpriteIcon;
    }
}
