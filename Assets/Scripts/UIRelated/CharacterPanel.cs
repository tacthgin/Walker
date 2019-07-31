using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterPanel : MonoBehaviour
{
    private static CharacterPanel instance;

    public static CharacterPanel MyInstance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<CharacterPanel>();
            }

            return instance;
        }
    }

    [SerializeField]
    private CanvasGroup canvasGroup = null;

    [SerializeField]
    private CharButton[] charButtons;

    public CharButton MySelectedButton { get; set; }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OpenClose()
    {
        canvasGroup.alpha = canvasGroup.alpha > 0 ? 0 : 1;

        canvasGroup.blocksRaycasts = !canvasGroup.blocksRaycasts;
    }

    public void EquipArmor(Armor armor)
    {
        charButtons[(int)armor.MyArmorType].EquipArmor(armor);
    }
}
