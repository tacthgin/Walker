using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CharButton : MonoBehaviour, IPointerClickHandler
{
    [SerializeField]
    private ArmorType armorType = ArmorType.Helmet;

    private Armor armor = null;

    [SerializeField]
    private Image icon = null;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            if (HandScript.MyInstance.MyMoveable is Armor)
            {
                Armor tmp = (Armor)HandScript.MyInstance.MyMoveable;

                if (tmp.MyArmorType == armorType)
                {
                    EquipArmor(tmp);
                }
            }
        }
    }

    public void EquipArmor(Armor armor)
    {
        icon.enabled = true;
        icon.sprite = armor.MyIcon;
        this.armor = armor;

        HandScript.MyInstance.DeleteItem();
    }
}
