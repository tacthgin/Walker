using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Item : ScriptableObject
{
    [SerializeField]
    private Sprite icon = null;

    [SerializeField]
    private int stackSize;

    public Sprite MyIcon { get => icon; }

    public int StackSize { get => stackSize; set => stackSize = value; }

    protected ScriptableObject Slot { get; set; }
}
