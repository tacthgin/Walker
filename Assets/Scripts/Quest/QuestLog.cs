using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestLog : MonoBehaviour
{
    private static QuestLog instance;

    public static QuestLog MyInstance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<QuestLog>();
            }

            return instance;
        }
    }

    [SerializeField]
    private GameObject questPrefab = null;

    [SerializeField]
    private Transform questParent = null;

    [SerializeField]
    private Text questDescription = null;

    private Quest selected = null;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AcceptQuest(Quest quest)
    {
        GameObject o = Instantiate(questPrefab, questParent);
        QuestScript qs = o.GetComponent<QuestScript>();
        quest.MyQuestScrit = qs;
        qs.MyQuest = quest;
        qs.MyQuest.MyQuestScrit = qs;

        o.GetComponent<Text>().text = quest.MyTitle;
    }

    public void ShowDescription(Quest quest)
    {
        if (selected != null)
        {
            selected.MyQuestScrit.DeSelect();
        }

        selected = quest;

        string title = quest.MyTitle;

        questDescription.text = string.Format("<b>{0}</b>\n{1}", quest.MyTitle, "hello world");
    }
}
