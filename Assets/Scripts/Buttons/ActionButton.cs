using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ActionButton : MonoBehaviour, IPointerClickHandler
{
    public Button MyButton { get; private set; }

    public IUseable MyUseable { get; set; }

    private Stack<IUseable> useables;

    private int count = 0;

    [SerializeField]
    private Image icon;

    public Image MyIcon { get => icon; set => icon = value; }

    // Start is called before the first frame update
    void Start()
    {
        MyButton = GetComponent<Button>();
        MyButton.onClick.AddListener(OnClick);
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

            if (useables != null && useables.Count > 0)
            {
                useables.Pop().Use();
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
            count = useables.Count;
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
    }
}
