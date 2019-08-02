using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vendor : MonoBehaviour, IInteractable
{
    [SerializeField]
    private VendorItem[] items = null;

    [SerializeField]
    private VendorWindow venderWindow = null;

    public void Interact()
    {
        venderWindow.CreatePages(items);
        venderWindow.Open();
    }

    public void StopInteract()
    {
        venderWindow.Close();
    }
}
