using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vendor : MonoBehaviour, IInteractable
{
    [SerializeField]
    private VendorItem[] items = null;

    [SerializeField]
    private VendorWindow venderWindow = null;

    public bool IsOpen { get; set; }

    public void Interact()
    {
        if (!IsOpen)
        {
            IsOpen = true;
            venderWindow.CreatePages(items);
            venderWindow.Open(this);
        }
        
    }

    public void StopInteract()
    {
        if (IsOpen)
        {
            IsOpen = false;
            venderWindow.Close();
        }
    }
}
