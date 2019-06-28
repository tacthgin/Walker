using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventotyScript : MonoBehaviour
{
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

    public void RemoveBag(Bag bag)
    {
        bags.Remove(bag);
        Destroy(bag.MyBagScript.gameObject);
    }

    public void AddItem(Item item)
    {
        if (item.MyStackSize > 0)
        {
            if (PlaceInStack(item))
            {
                return;
            }
        }

        PlaceInEmpty(item);
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

    private void PlaceInEmpty(Item item)
    {
        foreach (Bag bag in bags)
        {
            if (bag.MyBagScript.AddItem(item))
            {
                return;
            }
        }
    }

    private bool PlaceInStack(Item item)
    {
        foreach (Bag bag in bags)
        {
            foreach (SlotScript slots in bag.MyBagScript.MySlots)
            {
                if (slots.StackItem(item))
                {
                    return true;
                }
            }
        }

        return false;
    }
}
