using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class LootButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    [SerializeField]
    private Image icon = null;

    [SerializeField]
    private Text title = null;

    private LootWindow lootWindow = null;

    public Image MyIcon { get => icon; }

    public Text MyTitle { get => title; }

    public Item MyLoot { get; set; }

    void Awake()
    {
        lootWindow = gameObject.GetComponentInParent<LootWindow>();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (InventotyScript.MyInstance.AddItem(MyLoot))
        {
            gameObject.SetActive(false);
            lootWindow.TakeLoot(MyLoot);
            UIManager.MyInstance.HideTooltip();
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        UIManager.MyInstance.ShowTooltip(Vector2.right, transform.position, MyLoot);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        UIManager.MyInstance.HideTooltip();
    }
}
