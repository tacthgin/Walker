using System;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    private static UIManager instance;

    public static UIManager MyInstance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<UIManager>();
            }

            return instance;
        }
    }

    [SerializeField]
    private ActionButton[] actionButtons = null;

    [SerializeField]
    private GameObject targetFrame = null;

    [SerializeField]
    private Image portraitFrame = null;

    [SerializeField]
    private CanvasGroup keyBindMenu = null;

    [SerializeField]
    private CanvasGroup spellBook = null;

    private GameObject[] keybindButtons;

    private Stat healthStat;

    [SerializeField]
    private GameObject tooltip;

    private Text tooltipText;

    void Awake()
    {
        keybindButtons = GameObject.FindGameObjectsWithTag("Keybind");
        tooltipText = tooltip.GetComponentInChildren<Text>();
    }

    void Start()
    {
        healthStat = targetFrame.GetComponentInChildren<Stat>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            OpenClose(keyBindMenu);
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            OpenClose(spellBook);
        }


        if (Input.GetKeyDown(KeyCode.B))
        {
            InventotyScript.MyInstance.OpenClose();
        }
    }

    public void showTargetFrame(Npc target)
    {
        targetFrame.SetActive(true);

        healthStat.Initialize(target.MyHealth.MyCurrentValue, target.MyHealth.MyMaxValue);

        portraitFrame.sprite = target.MyPortrait;

        target.healthChanged += new HealthChanged(UpdateTargetFrame);

        target.characterRemoved += new CharacterRemoved(hideTargetFrame);
    }

    public void hideTargetFrame()
    {
        targetFrame.SetActive(false);
    }

    public void UpdateTargetFrame(float health)
    {
        healthStat.MyCurrentValue = health;
    }

    public void OpenClose(CanvasGroup canvasGroup)
    {
        canvasGroup.alpha = canvasGroup.alpha > 0 ? 0 : 1;
        canvasGroup.blocksRaycasts = !canvasGroup.blocksRaycasts;
    }

    public void UpdateKeyText(string key, KeyCode code)
    {
        Text tmp = Array.Find(keybindButtons, x => x.name == key).GetComponentInChildren<Text>();
        tmp.text = code.ToString();
    }

    public void ClickActionButton(string buttonName)
    {
        Array.Find(actionButtons, x => x.gameObject.name == buttonName).MyButton.onClick.Invoke();
    }

    public void UpdateStackSize(IClickable clickable)
    {
        if (clickable.MyCount > 1)
        {
            clickable.MyStackText.text = clickable.MyCount.ToString();
            clickable.MyStackText.color = Color.white;
            clickable.MyIcon.color = Color.white;
        }else
        {
            clickable.MyStackText.color = new Color(0, 0, 0, 0);
            clickable.MyIcon.color = Color.white;
        }

        if (clickable.MyCount == 0)
        {
            clickable.MyStackText.color = new Color(0, 0, 0, 0);
            clickable.MyIcon.sprite = null;
            clickable.MyIcon.color = new Color(0, 0, 0, 0);
        }
    } 

    public void ShowTooltip(Vector3 position, IDescribable description)
    {
        tooltip.SetActive(true);
        tooltip.transform.position = position;
        tooltipText.text = description.GetDescription();
    }

    public void HideTooltip()
    {
        tooltip.SetActive(false);
    }
}

