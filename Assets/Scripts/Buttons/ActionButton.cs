﻿using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ActionButton : MonoBehaviour, IPointerClickHandler, IClickable, IPointerEnterHandler, IPointerExitHandler
{
    public Button MyButton { get; private set; }

    public IUseable MyUseable { get; set; }

    private Stack<IUseable> useables = new Stack<IUseable>();

    [SerializeField]
    private Image icon;

    [SerializeField]
    private Text stackSize = null;

    public Image MyIcon { get => icon; set => icon = value; }

    public Text MyStackText { get => stackSize; }

    public int MyCount { get; private set; } = 0;

    public Stack<IUseable> MyUseables {
        get
        {
            return useables;
        }

        set
        {
            if (value.Count > 0)
            {
                MyUseable = value.Peek();
            }else
            {
                MyUseable = null;
            }

            useables = value;
        }
    }

    void Start()
    {
        MyButton = GetComponent<Button>();
        MyButton.onClick.AddListener(OnClick);

        InventotyScript.MyInstance.itemCountChangedEvent += new ItemCountChanged(UpdateItemCount);
    }

    public void OnClick()
    {
        if (HandScript.MyInstance.MyMoveable == null)
        {
            if (MyUseable != null)
            {
                MyUseable.Use();
            }else if (MyUseables != null && MyUseables.Count > 0)
            {
                MyUseables.Peek().Use();
            }
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            if (HandScript.MyInstance.MyMoveable != null && HandScript.MyInstance.MyMoveable is IUseable)
            {
                SetUseable(HandScript.MyInstance.MyMoveable as IUseable);
            }
        }
    }

    public void SetUseable(IUseable useable)
    {
        if (useable is Item)
        {
            MyUseables = InventotyScript.MyInstance.GetUseables(useable);
            InventotyScript.MyInstance.FromSlot.MyIcon.color = Color.white;
            InventotyScript.MyInstance.FromSlot = null;
        }
        else
        {
            MyUseables.Clear();
            MyUseable = useable;
        }

        MyCount = MyUseables.Count;

        UpdateVisual();

        UIManager.MyInstance.RefreshTooltip(MyUseable as IDescribable);
    }

    public void UpdateVisual()
    {
        MyIcon.sprite = HandScript.MyInstance.Put().MyIcon;
        MyIcon.color = Color.white;

        if (MyCount > 1)
        {
            UIManager.MyInstance.UpdateStackSize(this);
        }else if (MyUseable is Spell)
        {
            UIManager.MyInstance.ClearStackCount(this);
        }
    }

    public void UpdateItemCount(Item item)
    {
        if (item is IUseable && MyUseables.Count > 0)
        {
            if (MyUseables.Peek().GetType() == item.GetType())
            {
                MyUseables = InventotyScript.MyInstance.GetUseables(item as IUseable);

                MyCount = MyUseables.Count;

                UIManager.MyInstance.UpdateStackSize(this);
            }
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        IDescribable tmp = null;
        if (MyUseable != null && MyUseable is IDescribable)
        {
            tmp = (IDescribable)MyUseable;
        }else if (MyUseables.Count > 0 && MyUseables.Peek() is IDescribable)
        {
            tmp = (IDescribable)MyUseables.Peek();
        }

        if (tmp != null)
        {
            UIManager.MyInstance.ShowTooltip(Vector2.right, transform.position, tmp);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        UIManager.MyInstance.HideTooltip();
    }
}
