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

    private GameObject[] keybindButtons;

    private Stat healthStat;

    void Awake()
    {
        keybindButtons = GameObject.FindGameObjectsWithTag("Keybind");
    }

    void Start()
    {
        healthStat = targetFrame.GetComponentInChildren<Stat>();

        SetUseable(actionButtons[0], SpellBook.MyInstance.GetSpell("Fireball"));
        SetUseable(actionButtons[1], SpellBook.MyInstance.GetSpell("Frostbolt"));
        SetUseable(actionButtons[2], SpellBook.MyInstance.GetSpell("Thunderbolt"));
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            OpenCloseMenu();
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

    public void OpenCloseMenu()
    {
        keyBindMenu.alpha = keyBindMenu.alpha > 0 ? 0 : 1;
        keyBindMenu.blocksRaycasts = !keyBindMenu.blocksRaycasts;
        Time.timeScale = Time.timeScale > 0 ? 0 : 1;
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

    public void SetUseable(ActionButton btn, IUseable useable)
    {
        btn.MyButton.image.sprite = useable.MyIcon;
        btn.MyButton.image.color = Color.white;
        btn.MyUseable = useable;
    }
}

