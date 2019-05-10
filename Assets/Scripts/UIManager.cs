using System.Collections;
using System.Collections.Generic;
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

    private Stat healthStat;

    // Start is called before the first frame update
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
    }

    private void ActionButtonOnClick(int btnIndex)
    {
        actionButtons[btnIndex].onClick.Invoke();
    }

    public void showTargetFrame(Npc target)
    {
        targetFrame.SetActive(true);

        healthStat.Initialize(target.MyHealth.MyCurrentValue, target.MyHealth.MyMaxValue);
        target.healthChanged += new HealthChanged(UpdateTargetFrame);
    }

    public void hideTargetFrame()
    {
        targetFrame.SetActive(false);
    }

    public void UpdateTargetFrame(float health)
    {
        healthStat.MyCurrentValue = health;
    }
}
