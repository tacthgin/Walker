﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Bag", menuName ="Items/Bag", order = 1)]
public class Bag : Item, IUseable
{
    [SerializeField]
    private GameObject bagPrefab = null;

    [SerializeField]
    private int slots = 0;

    public BagScript MyBagScript { get; set; }

    public BagButton MyBagButton { get; set; }

    public int MySlots { get => slots; }

    public void Use()
    {
        if (InventotyScript.MyInstance.canAddBag)
        {
            Remove();
            MyBagScript = Instantiate(bagPrefab, InventotyScript.MyInstance.transform).GetComponent<BagScript>();
            MyBagScript.AddSlots(slots);

            if (MyBagButton == null)
            {
                InventotyScript.MyInstance.AddBag(this);
            }else
            {
                InventotyScript.MyInstance.AddBag(this, MyBagButton);
            }
        }
    }

    public override string GetDescription()
    {
        return base.GetDescription() + string.Format("\n<color=#ffffffff>{0} slot bag</color>", slots);
    }
}
