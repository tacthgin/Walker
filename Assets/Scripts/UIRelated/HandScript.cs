using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class HandScript : MonoBehaviour
{
    private static HandScript instance;

    public static HandScript MyInstance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<HandScript>();
            }

            return instance;
        }
    }

    public IMoveable MyMoveable { get; set; }

    private Image icon;

    [SerializeField]
    private Vector3 offset = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {
        icon = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        icon.transform.position = Input.mousePosition + offset;

        if (Input.GetMouseButton(0) && !EventSystem.current.IsPointerOverGameObject() && MyInstance.MyMoveable != null)
        {
            DeleteItem();
        }
    }

    public void TakeMoveable(IMoveable moveable)
    {
        MyMoveable = moveable;
        icon.sprite = moveable.MyIcon;
        icon.color = Color.white;
    }

    public IMoveable Put()
    {
        IMoveable tmp = MyMoveable;
        MyMoveable = null;
        icon.sprite = null;
        icon.color = new Color(0, 0, 0, 0);
        return tmp;
    }

    public void Drop()
    {
        MyMoveable = null;
        icon.sprite = null;
        icon.color = new Color(0, 0, 0, 0);
        InventotyScript.MyInstance.FromSlot = null;
    }

    public void DeleteItem()
    {
        if (MyMoveable is Item && InventotyScript.MyInstance.FromSlot != null)
        {
            (MyMoveable as Item).MySlot.Clear();
        }

        Drop();
    }
}
