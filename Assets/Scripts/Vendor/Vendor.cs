using UnityEngine;

public class Vendor : Npc
{
    [SerializeField]
    private VendorItem[] items = null;

    public VendorItem[] MyItems { get => items; }
}
