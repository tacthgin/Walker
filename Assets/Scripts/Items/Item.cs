using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : ScriptableObject
{
    [SerializeField]
    private Sprite icon;

    [SerializeField]
    private int stackSize;

    public Sprite Icon { get => icon; }
    public int StackSize { get => stackSize; set => stackSize = value; }
    protected ScriptableObject Slot { get; set; }
}
