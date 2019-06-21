using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Stat : MonoBehaviour
{
    private Image content;

    [SerializeField]
    private Text statValue = null;

    [SerializeField]
    private float lerpSpeed = 0;

    private float currentFill = 0;

    public float MyMaxValue { get; set; }

    private float currentValue = 0;

    public float MyCurrentValue
    {
        get
        {
            return currentValue;
        }

        set
        {
            currentValue = Mathf.Clamp(value, 0, MyMaxValue);

            currentFill = currentValue / MyMaxValue;

            if (statValue != null)
            {
                statValue.text = currentValue + "/" + MyMaxValue;
            } 
        }
    }

    public bool IsFull
    {
        get => MyCurrentValue == MyMaxValue;
    }

    // Start is called before the first frame update
    void Start()
    {
        content = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        HandleBar();
    }

    public void Initialize(float currentValue, float maxValue)
    {
        if (content == null)
        {
            content = GetComponent<Image>();
        }

        MyMaxValue = maxValue;
        MyCurrentValue = currentValue;
        content.fillAmount = currentFill;
    }

    private void HandleBar()
    {
        if (content.fillAmount != currentFill)
        {
            content.fillAmount = Mathf.Lerp(content.fillAmount, currentFill, Time.deltaTime * lerpSpeed);
        }
    }

}
