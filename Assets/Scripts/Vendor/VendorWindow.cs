using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VendorWindow : MonoBehaviour
{
    [SerializeField]
    private CanvasGroup canvasGroup = null;

    [SerializeField]
    private VendorButton[] vendorButtons = null;

    [SerializeField]
    private Text pageNumber = null;

    private int pageIndex = 0;

    private List<List<VendorItem>> pages = new List<List<VendorItem>>();

    private Vendor vendor;

    public void CreatePages(VendorItem[] items)
    {
        pages.Clear();
        pageIndex = 0;

        List<VendorItem> page = new List<VendorItem>();

        for (var i = 0; i < items.Length; i++)
        {
            page.Add(items[i]);
            if (page.Count == 10 || i == items.Length - 1)
            {
                pages.Add(page);
                page = new List<VendorItem>();
            }
        }

        AddItems();
    }

    public void AddItems()
    {
        pageNumber.text = pageIndex + 1 + "/" + pages.Count;

        if (pages.Count > 0)
        {
            for (int i = 0; i < pages[pageIndex].Count; i++)
            {
                if (pages[pageIndex][i] != null)
                {
                    vendorButtons[i].AddItem(pages[pageIndex][i]);
                }
            }
        }
    }

    public void Open(Vendor vendor)
    {
        this.vendor = vendor;
        canvasGroup.alpha = 1;
        canvasGroup.blocksRaycasts = true;
    }

    public void Close()
    {
        vendor.IsOpen = false;
        canvasGroup.alpha = 0;
        canvasGroup.blocksRaycasts = false;
    }

    public void NextPage()
    {
        if (pageIndex < pages.Count - 1)
        {
            ClearButtons();
            ++pageIndex;
            AddItems();
        }
    }

    public void PrevousPage()
    {
        if (pageIndex > 0)
        {
            ClearButtons();
            --pageIndex;
            AddItems();
        }
    }

    public void ClearButtons()
    {
        foreach (VendorButton button in vendorButtons)
        {
            button.gameObject.SetActive(false);
        }
    }
}
