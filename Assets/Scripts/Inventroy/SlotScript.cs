using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SlotScript : MonoBehaviour, IPointerClickHandler, IClickable
{
    private ObservableStack<Item> items = new ObservableStack<Item>();

    [SerializeField]
    private Image icon;

    [SerializeField]
    private Text stackSize;

    public BagScript MyBag { get; set; }

    public bool IsEmpty
    {
        get => items.Count == 0;
    }

    public bool IsFull
    {
        get
        {
            if (IsEmpty || MyCount < MyItem.MyStackSize)
            {
                return false;
            }

            return true;
        }
    }

    public Item MyItem
    {
        get {
            if (!IsEmpty) { return items.Peek(); }
            return null;
        }
    }

    public Image MyIcon { get => icon; set => icon = value; }

    public int MyCount { get => items.Count; }

    public Text MyStackText { get => stackSize; }

    private void Awake()
    {
        items.OnPop += new UpdateStackEvent(UpdateSlot);
        items.OnPush += new UpdateStackEvent(UpdateSlot);
        items.OnClear += new UpdateStackEvent(UpdateSlot);
    }

    public bool AddItem(Item item)
    {
        items.Push(item);
        icon.sprite = item.MyIcon;
        icon.color = Color.white;
        item.MySlot = this;
        return true;
    }

    public bool AddItems(ObservableStack<Item> newItems)
    {
        if (IsEmpty || newItems.Peek().GetType() == MyItem.GetType())
        {
            int count = newItems.Count;

            for (int i = 0; i < count; i++)
            {
                if (IsFull)
                {
                    return false;
                }

                AddItem(newItems.Pop());
            }

            return true;
        }

        return false;
    }

    public void RemoveItem()
    {
        if (!IsEmpty)
        {
            items.Pop();
        }
    }

    public void Clear()
    {
        if (items.Count > 0)
        {
            items.Clear();
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            if (InventotyScript.MyInstance.FromSlot == null && !IsEmpty)
            {
                HandScript.MyInstance.TakeMoveable(MyItem as IMoveable);
                InventotyScript.MyInstance.FromSlot = this;
            }
            else if (InventotyScript.MyInstance.FromSlot == null && IsEmpty && (HandScript.MyInstance.MyMoveable is Bag))
            {
                Bag bag = (Bag)HandScript.MyInstance.MyMoveable;

                if (bag.MyBagScript != MyBag && InventotyScript.MyInstance.MyEmptySlotCount - bag.Slots > 0)
                {
                    AddItem(bag);
                    bag.MyBagButton.RemoveBag();
                    HandScript.MyInstance.Drop();
                }
            }
            else if (InventotyScript.MyInstance.FromSlot != null)
            {
                if (PutItemBack() || MergeItems(InventotyScript.MyInstance.FromSlot) || Swapitems(InventotyScript.MyInstance.FromSlot) || AddItems(InventotyScript.MyInstance.FromSlot.items))
                {
                    HandScript.MyInstance.Drop();
                    InventotyScript.MyInstance.FromSlot = null;
                }
            }
        }

        if (eventData.button == PointerEventData.InputButton.Right)
        {
            //右键使用物品
            UseItem();
        }
    }

    public void UseItem()
    {
        if (MyItem is IUseable)
        {
            (MyItem as IUseable).Use();
        }
    }

    public bool StackItem(Item item)
    {
        if (!IsEmpty && item.name == MyItem.name && items.Count < MyItem.MyStackSize)
        {
            items.Push(item);
            item.MySlot = this;
            return true;
        }
        return false;
    }

    private bool PutItemBack()
    {
        if (InventotyScript.MyInstance.FromSlot == this)
        {
            InventotyScript.MyInstance.FromSlot.MyIcon.color = Color.white;
            return true;
        }

        return false;
    }

    private bool Swapitems(SlotScript from)
    {
        if (IsEmpty)
        {
            return false;
        }

        if (from.MyItem.GetType() != MyItem.GetType() || from.MyCount + MyCount > MyItem.MyStackSize)
        {
            ObservableStack<Item> tmpFrom = new ObservableStack<Item>(from.items);

            from.items.Clear();
            from.AddItems(items);

            items.Clear();
            AddItems(tmpFrom);

            return true;
        }

        return false;
    }

    public bool MergeItems(SlotScript from)
    {
        if (IsEmpty)
        {
            return false;
        }

        if (from.MyItem.GetType() == MyItem.GetType() || !IsFull)
        {
            int free = MyItem.MyStackSize - MyCount;

            for (int i = 0; i < free; i++)
            {
                AddItem(from.items.Pop());
            }

            return true;
        }

        return false;
    }

    private void UpdateSlot()
    {
        UIManager.MyInstance.UpdateStackSize(this);
    }
}
