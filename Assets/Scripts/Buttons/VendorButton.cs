using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class VendorButton : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField]
    private Image icon = null;

    [SerializeField]
    private Text title = null;

    [SerializeField]
    private Text price = null;

    [SerializeField]
    private Text quantity = null;

    public void AddItem(VendorItem vendorItem)
    {
        if (vendorItem.MyQuantity > 0 || (vendorItem.MyQuantity == 0 && vendorItem.MyUnlimited))
        {
            icon.sprite = vendorItem.MyItem.MyIcon;
            title.text = string.Format("<color={0}>{1}</color>", QualityColor.MyColors[vendorItem.MyItem.MyQuality], vendorItem.MyItem.MyTitle);
            price.text = "Price: " + vendorItem.MyItem.MyPrice.ToString();

            if (!vendorItem.MyUnlimited)
            {
                quantity.text = vendorItem.MyQuantity.ToString();
            }

            gameObject.SetActive(true);
        }
        else
        {
            gameObject.SetActive(false);
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        
    }
}
