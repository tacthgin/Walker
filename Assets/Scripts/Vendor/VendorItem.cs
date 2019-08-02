using UnityEngine;

[System.Serializable]
public class VendorItem
{
    [SerializeField]
    private Item item = null;

    [SerializeField]
    private int quantity = 0;

    [SerializeField]
    private bool unlimited = false;

    public Item MyItem { get => item; }

    public int MyQuantity { get => quantity; }

    public bool MyUnlimited { get => unlimited; }
}
