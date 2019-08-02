using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VendorWindow : MonoBehaviour
{
    [SerializeField]
    private CanvasGroup canvasGroup = null;

    [SerializeField]
    private VendorButton[] vendorButtons;

    public void CreatePages(VendorItem[] items)
    {
        for (var i = 0; i < items.Length; i++)
        {
            vendorButtons[i].AddItem(items[i]);
        }
    }

    public void Open()
    {
        canvasGroup.alpha = 1;
        canvasGroup.blocksRaycasts = true;
    }

    public void Close()
    {
        canvasGroup.alpha = 0;
        canvasGroup.blocksRaycasts = false;
    }
}
