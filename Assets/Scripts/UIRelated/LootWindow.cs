using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootWindow : MonoBehaviour
{
    [SerializeField]
    private LootButton[] lootButtons = null;

    [SerializeField]
    private Item[] items = null;

    // Start is called before the first frame update
    void Start()
    {
        AddLoot();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void AddLoot()
    {
        int itemIndex = 3;

        lootButtons[itemIndex].MyIcon.sprite = items[itemIndex].MyIcon;

        lootButtons[itemIndex].gameObject.SetActive(true);

        string title = string.Format("<color={0}>{1}</color>", QualityColor.MyColors[items[itemIndex].MyQuality], items[itemIndex].MyTitle);

        lootButtons[itemIndex].MyTitle.text = title;
    }
}
