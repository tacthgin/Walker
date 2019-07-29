using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Item : ScriptableObject, IMoveable, IDescribable
{
    [SerializeField]
    private Sprite icon = null;

    [SerializeField]
    private int stackSize = 0;

    public Sprite MyIcon { get => icon; }

    public int MyStackSize { get => stackSize; }

    public SlotScript MySlot { get; set; }

    [SerializeField]
    private string title;

    public string GetDescription()
    {
        return title;
    }

    public void Remove()
    {
        if (MySlot != null)
        {
            MySlot.RemoveItem();
        }
    }
}
