using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class LootButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    [SerializeField]
    private Image icon = null;

    [SerializeField]
    private Text title = null;

    public Image MyIcon { get => icon; }

    public Text MyTitle { get => title; }

    public Item MyLoot { get; set; }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (InventotyScript.MyInstance.AddItem(MyLoot))
        {
            gameObject.SetActive(false);
            UIManager.MyInstance.HideTooltip();
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        UIManager.MyInstance.ShowTooltip(transform.position, MyLoot);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        UIManager.MyInstance.HideTooltip();
    }
}
