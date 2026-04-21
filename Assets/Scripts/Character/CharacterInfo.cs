using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CharacterInfo : MonoBehaviour
{
    public Image icon;

    public LocalizationText_Answer textName;
    public LocalizationText_Answer textDesc;
    public LocalizationText_Answer textAttack;
    public LocalizationText_Answer textDefense;
    public TextMeshProUGUI textAttackStat;
    public TextMeshProUGUI textDefenseStat;
    public Image WeaponIcon;
    public Image DefenseIcon;

    private void Start()
    {
        SetEmpty();
    }

    public void SetEmpty()
    {
        icon.sprite = null;
        textName.id = string.Empty;
        textDesc.id = string.Empty;
        textAttack.id = string.Empty;
        textDefense.id = string.Empty;


        textName.text.text = string.Empty;
        textDesc.text.text = string.Empty;
        textAttack.text.text = string.Empty;
        textDefense.text.text = string.Empty;
        textAttackStat.text = string.Empty;
        textDefenseStat.text = string.Empty;
    }

    public void SetCharacterData(string characterId)
    {
        CharacterData data = DataTableManager.CharacterTable.Get(characterId);
        SetCharacterData(data);
    }

    public void SetCharacterData(CharacterData data)
    {
        SaveCharacterData characterData = new();
        characterData.CharacterData = data;
        SetCharacterData(characterData);
    }

    public void SetCharacterData(SaveCharacterData data)
    {
        icon.sprite = data.CharacterData.SpriteIcon;
        textName.id = data.CharacterData.Name;
        textDesc.id = data.CharacterData.Desc;
        textAttack.id = data.CharacterData.KeyAttack;
        textDefense.id = data.CharacterData.KeyDeffense;

        textAttackStat.text =
            (data.CharacterData.Attack + (data.WeaponData != null ? data.WeaponData.ItemData.Value : 0)).ToString();
        textDefenseStat.text =
            (data.CharacterData.Attack + (data.ArmorData != null ? data.ArmorData.ItemData.Value : 0)).ToString();

        WeaponIcon.sprite =
            data.WeaponData != null ? data.WeaponData.ItemData.SpriteIcon : Resources.Load<Sprite>("Icon/Icon_Close01");
        DefenseIcon.sprite =
            data.ArmorData != null ? data.ArmorData.ItemData.SpriteIcon : Resources.Load<Sprite>("Icon/Icon_Close01");

        textName.OnChangedId();
        textDesc.OnChangedId();
        textAttack.OnChangedId();
        textDefense.OnChangedId();
    }
}
