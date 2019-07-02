using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Bag", menuName ="Items/Bag", order = 1)]
public class Bag : Item, IUseable
{
    [SerializeField]
    private GameObject bagPrefab = null;

    public BagScript MyBagScript { get; set; }

    public BagButton MyBagButton { get; set; }

    public int Slots { get; private set; }

    public void Initialize(int slots)
    {
        Slots = slots;
    }

    public void Use()
    {
        if (InventotyScript.MyInstance.canAddBag)
        {
            Remove();
            MyBagScript = Instantiate(bagPrefab, InventotyScript.MyInstance.transform).GetComponent<BagScript>();
            MyBagScript.AddSlots(Slots);

            if (MyBagButton == null)
            {
                InventotyScript.MyInstance.AddBag(this);
            }else
            {
                InventotyScript.MyInstance.AddBag(this, MyBagButton);
            }
        }
    }

    
}
