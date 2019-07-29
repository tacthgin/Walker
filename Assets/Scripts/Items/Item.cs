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
    private string title = string.Empty;

    [SerializeField]
    private Quality quality = Quality.Common;

    public virtual string GetDescription()
    {
        return string.Format("<color={0}>{1}</color>", QualityColor.MyColors[quality], title);
    }

    public void Remove()
    {
        if (MySlot != null)
        {
            MySlot.RemoveItem();
        }
    }
}
