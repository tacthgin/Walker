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
        gameObject.SetActive(true);

        icon.sprite = vendorItem.MyItem.MyIcon;
        title.text = vendorItem.MyItem.MyTitle;

        if (!vendorItem.MyUnlimited)
        {
            quantity.text = vendorItem.MyQuantity.ToString();
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
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
