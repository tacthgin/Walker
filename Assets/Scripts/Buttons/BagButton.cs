using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BagButton : MonoBehaviour, IPointerClickHandler
{
    private Bag bag;

    [SerializeField]
    private Sprite full = null, empty = null;

    [SerializeField]
    private int bagIndex = 0;

    public Bag MyBag {
        get => bag;
        set {
            if (value != null)
            {
                GetComponent<Image>().sprite = full;
            }
            else
            {
                GetComponent<Image>().sprite = empty;
            }
            bag = value;
        }
    }

    public int MyBagIndex { get => bagIndex; }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            if (InventotyScript.MyInstance.FromSlot != null && HandScript.MyInstance.MyMoveable != null && HandScript.MyInstance.MyMoveable is Bag)
            {
                if (MyBag != null)
                {
                    InventotyScript.MyInstance.SwapBags(MyBag, HandScript.MyInstance.MyMoveable as Bag);
                }
                else
                {
                    Bag tmp = (Bag)HandScript.MyInstance.MyMoveable;
                    tmp.MyBagButton = this;
                    tmp.Use();
                    MyBag = tmp;
                    HandScript.MyInstance.Drop();
                    InventotyScript.MyInstance.FromSlot = null;
                }
            }
            else if (Input.GetKey(KeyCode.LeftShift))
            {
                HandScript.MyInstance.TakeMoveable(MyBag);
            }
            else if (bag != null)
            {
                bag.MyBagScript.OpenClose();
            }
        }
    }

    public void RemoveBag()
    {
        InventotyScript.MyInstance.RemoveBag(MyBag);
        MyBag.MyBagButton = null;

        foreach (Item item in MyBag.MyBagScript.GetItems())
        {
            InventotyScript.MyInstance.AddItem(item);
        }

        MyBag = null;
    }
}
