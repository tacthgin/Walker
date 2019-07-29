using System.Collections.Generic;
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

    // Start is called before the first frame update
    void Start()
    {
        MyButton = GetComponent<Button>();
        MyButton.onClick.AddListener(OnClick);

        InventotyScript.MyInstance.itemCountChangedEvent += new ItemCountChanged(UpdateItemCount);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClick()
    {
        if (HandScript.MyInstance.MyMoveable == null)
        {
            if (MyUseable != null)
            {
                MyUseable.Use();
            }

            if (useables.Count > 0)
            {
                useables.Peek().Use();
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
            useables = InventotyScript.MyInstance.GetUseables(useable);
            MyCount = useables.Count;
            InventotyScript.MyInstance.FromSlot.MyIcon.color = Color.white;
            InventotyScript.MyInstance.FromSlot = null;
        }
        else
        {
            MyUseable = useable;
        }

        UpdateVisual();
    }

    public void UpdateVisual()
    {
        MyIcon.sprite = HandScript.MyInstance.Put().MyIcon;
        MyIcon.color = Color.white;

        if (MyCount > 1)
        {
            UIManager.MyInstance.UpdateStackSize(this);
        }
    }

    public void UpdateItemCount(Item item)
    {
        if (item is IUseable && useables.Count > 0)
        {
            if (useables.Peek().GetType() == item.GetType())
            {
                useables = InventotyScript.MyInstance.GetUseables(item as IUseable);

                MyCount = useables.Count;

                UIManager.MyInstance.UpdateStackSize(this);
            }
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (MyUseable != null || useables.Count > 0)
        {
            //UIManager.MyInstance.ShowTooltip(transform.position);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        UIManager.MyInstance.HideTooltip();
    }
}
