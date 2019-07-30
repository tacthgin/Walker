﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void ItemCountChanged(Item item);

public class InventotyScript : MonoBehaviour
{
    public ItemCountChanged itemCountChangedEvent = null;

    private static InventotyScript instance;

    public static InventotyScript MyInstance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<InventotyScript>();
            }

            return instance;
        }
    }

    private List<Bag> bags = new List<Bag>();

    [SerializeField]
    private BagButton[] bagButtons = null;

    //For debugging
    [SerializeField]
    private Item[] items = null;

    private SlotScript fromSlot;

    public bool canAddBag
    {
        get => bags.Count < 5;
    }

    public int MyEmptySlotCount
    {
        get
        {
            int count = 0;
            foreach (Bag bag in bags)
            {
                count += bag.MyBagScript.MyEmptySlotCount;
            }

            return count;
        }
    }

    public int MyTotalSlotCount
    {
        get
        {
            int count = 0;
            foreach (Bag bag in bags)
            {
                count += bag.MyBagScript.MySlots.Count;
            }

            return count;
        }
    }

    public int MyFullSlotCount
    {
        get
        {
            return MyTotalSlotCount - MyEmptySlotCount;
        }
    }

    /// <summary>
    /// 选择背包的物品
    /// </summary>
    public SlotScript FromSlot
    {
        get => fromSlot;
        set {
            fromSlot = value;
            if (value != null)
            {
                fromSlot.MyIcon.color = Color.grey;
            }
        }
    }

    private void Awake()
    {
       
    }

    public void AddBag(Bag bag)
    {
        foreach (BagButton bagButton in bagButtons)
        {
            if (bagButton.MyBag == null)
            {
                bagButton.MyBag = bag;
                bag.MyBagButton = bagButton;
                bags.Add(bag);
                break;
            }
        }
    }

    public void AddBag(Bag bag, BagButton bagButton)
    {
        bags.Add(bag);
        bagButton.MyBag = bag;
    }

    public void RemoveBag(Bag bag)
    {
        bags.Remove(bag);
        Destroy(bag.MyBagScript.gameObject);
    }

    public void SwapBags(Bag oldBag, Bag newBag)
    {
        int newSlotCount = (MyTotalSlotCount - oldBag.Slots) + newBag.Slots;

        if (newSlotCount - MyFullSlotCount >= 0)
        {
            List<Item> bagItems = oldBag.MyBagScript.GetItems();

            RemoveBag(oldBag);

            newBag.MyBagButton = oldBag.MyBagButton;

            newBag.Use();

            foreach (Item item in bagItems)
            {
                if (item != newBag)
                {
                    AddItem(item);
                }
            }

            AddItem(oldBag);

            HandScript.MyInstance.Drop();
            MyInstance.fromSlot = null;
        }
    }

    public bool AddItem(Item item)
    {
        if (item.MyStackSize > 0 && PlaceInStack(item))
        {
            return true;
        }

        return PlaceInEmpty(item);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.J))
        {
            Bag bag = (Bag)Instantiate(items[0]);
            bag.Initialize(16);
            bag.Use();
        }

        if (Input.GetKeyDown(KeyCode.G))
        {
            Bag bag = (Bag)Instantiate(items[0]);
            bag.Initialize(8);
            AddItem(bag);
        }

        if (Input.GetKeyDown(KeyCode.K))
        {
            Bag bag = (Bag)Instantiate(items[0]);
            bag.Initialize(16);
            AddItem(bag);
        }

        if (Input.GetKeyDown(KeyCode.L))
        {
            HealthPotion potion = (HealthPotion)Instantiate(items[1]);
            AddItem(potion);
        }

        if (Input.GetKeyDown(KeyCode.H))
        {
            Armor armor = (Armor)Instantiate(items[2]);
            AddItem(armor);
            armor = (Armor)Instantiate(items[3]);
            AddItem(armor);
            armor = (Armor)Instantiate(items[4]);
            AddItem(armor);
            armor = (Armor)Instantiate(items[5]);
            AddItem(armor);
            armor = (Armor)Instantiate(items[6]);
            AddItem(armor);
            armor = (Armor)Instantiate(items[7]);
            AddItem(armor);
            armor = (Armor)Instantiate(items[8]);
            AddItem(armor);
            armor = (Armor)Instantiate(items[9]);
            AddItem(armor);
            armor = (Armor)Instantiate(items[10]);
            AddItem(armor);
        }
    }

    public void OpenClose()
    {
        bool closedBag = bags.Find(x => !x.MyBagScript.IsOpen);

        //closedBag 为true，打开所有背包，否则关闭所有背包
        foreach (Bag bag in bags)
        {
            if (bag.MyBagScript.IsOpen != closedBag)
            {
                bag.MyBagScript.OpenClose();
            }
        }
    }

    private bool PlaceInEmpty(Item item)
    {
        foreach (Bag bag in bags)
        {
            if (bag.MyBagScript.AddItem(item))
            {
                return true;
            }
        }

        return false;
    }

    private bool PlaceInStack(Item item)
    {
        foreach (Bag bag in bags)
        {
            foreach (SlotScript slots in bag.MyBagScript.MySlots)
            {
                if (slots.StackItem(item))
                {
                    OnItemCountChanged(item);
                    return true;
                }
            }
        }

        return false;
    }

    public Stack<IUseable> GetUseables(IUseable type)
    {
        Stack<IUseable> useables = new Stack<IUseable>();

        foreach (Bag bag in bags)
        {
            foreach (SlotScript slot in bag.MyBagScript.MySlots)
            {
                if (!slot.IsEmpty && slot.MyItem.GetType() == type.GetType())
                {
                    foreach (Item item in slot.MyItems)
                    {
                        useables.Push(item as IUseable);
                    }
                }
            }
        }

        return useables;
    }

    public void OnItemCountChanged(Item item)
    {
        if (itemCountChangedEvent != null)
        {
            itemCountChangedEvent.Invoke(item);
        }
    }
}
