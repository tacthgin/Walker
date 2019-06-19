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
    private BagButton[] bagButtons;

    //For debugging
    [SerializeField]
    private Item[] items;

    public bool canAddBag
    {
        get { return bags.Count < 5; } 
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
                bags.Add(bag);
                break;
            }
        }
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
            bag.Initialize(20);
            bag.Use();
        }
    }
}
