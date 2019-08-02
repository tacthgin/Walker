using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour, IInteractable
{
    [SerializeField]
    private SpriteRenderer spriteRenderer = null;

    [SerializeField]
    private Sprite openSprite = null, closedSprite = null;

    private bool isOpen = false;

    [SerializeField]
    private CanvasGroup canvasGroup = null;

    private List<Item> items = null;

    [SerializeField]
    private BagScript bag = null;

    public void Interact()
    {
        if (isOpen)
        {
            StopInteract();
        }
        else
        {
            AddItems();
            isOpen = true;
            spriteRenderer.sprite = openSprite;

            canvasGroup.alpha = 1;
            canvasGroup.blocksRaycasts = true;
        }
    }

    public void StopInteract()
    {
        StoreItems();
        bag.Clear();
        isOpen = false;
        spriteRenderer.sprite = closedSprite;

        canvasGroup.alpha = 0;
        canvasGroup.blocksRaycasts = false;
    }

    public void AddItems()
    {
        if (items != null)
        {
            foreach (Item item in items)
            {
                item.MySlot.AddItem(item);
            }
        }
    }

    public void StoreItems()
    {
        items = bag.GetItems();
    }
}
