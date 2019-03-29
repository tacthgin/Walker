using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Stat : MonoBehaviour
{
    private Image content;

    [SerializeField]
    private Text statValue;

    [SerializeField]
    private float lerpSpeed;

    private float currentFill;

    public float MyMaxValue { get; set; }

    private float currentValue;

    public float MyCurrentValue
    {
        get
        {
            return currentValue;
        }

        set
        {
            if (value > MyMaxValue)
            {
                value = MyMaxValue;
            }else if (value < 0)
            {
                value = 0;
            }
            currentValue = value;

            currentFill = currentValue / MyMaxValue;

            statValue.text = currentValue + "/" + MyMaxValue;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        content = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        if (content.fillAmount != currentFill)
        {
            content.fillAmount = Mathf.Lerp(content.fillAmount, currentFill, Time.deltaTime * lerpSpeed);
        }
    }

    public void Initialize(float currentValue, float maxValue)
    {
        MyMaxValue = maxValue;
        MyCurrentValue = currentValue;
    }
}
