using UnityEngine;
using UnityEngine.UI;

public class Questlog : MonoBehaviour
{
    private static Questlog instance;

    public static Questlog MyInstance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<Questlog>();
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
        foreach (CollectObjective co in quest.MyCollectObjectives)
        {
            InventotyScript.MyInstance.itemCountChangedEvent += new ItemCountChanged(co.UpdateItemCount);
        }

        GameObject o = Instantiate(questPrefab, questParent);
        QuestScript qs = o.GetComponent<QuestScript>();
        quest.MyQuestScrit = qs;
        qs.MyQuest = quest;
        qs.MyQuest.MyQuestScrit = qs;

        o.GetComponent<Text>().text = quest.MyTitle;
    }

    public void ShowDescription(Quest quest)
    {
        if (quest != null)
        {
            if (selected != null && selected != quest)
            {
                selected.MyQuestScrit.DeSelect();
            }

            string objective = string.Empty;

            selected = quest;

            string title = quest.MyTitle;

            foreach (Objective obj in quest.MyCollectObjectives)
            {
                objective += obj.MyType + ": " + obj.MyCurrentAmount + "/" + obj.MyAmount + "\n";
            }

            questDescription.text = string.Format("{0}\n<size=20>{1}</size>\n\nObjectives\n<size=20>{2}</size>", quest.MyTitle, quest.MyDescription, objective);
        }
    }

    public void UpdateSelected()
    {
        ShowDescription(selected);
    }
}
