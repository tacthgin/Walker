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
    private Button[] actionButtons = null;

    private KeyCode action1, action2, action3;

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
        action1 = KeyCode.Alpha1;
        action2 = KeyCode.Alpha2;
        action3 = KeyCode.Alpha3;

        healthStat = targetFrame.GetComponentInChildren<Stat>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(action1))
        {
            ActionButtonOnClick(0);
        }

        if (Input.GetKeyDown(action2))
        {
            ActionButtonOnClick(1);
        }

        if (Input.GetKeyDown(action3))
        {
            ActionButtonOnClick(2);
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            OpenCloseMenu();
        }
    }

    private void ActionButtonOnClick(int btnIndex)
    {
        actionButtons[btnIndex].onClick.Invoke();
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
}

