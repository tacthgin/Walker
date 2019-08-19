using UnityEngine;
using UnityEngine.UI;

public class QuestGiverWindow : Window
{
    private static QuestGiverWindow instance;

    public static QuestGiverWindow MyInstance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<QuestGiverWindow>();
            }

            return instance;
        }
    }

    private QuestGiver questGiver;

    [SerializeField]
    private GameObject backBtn = null, accpetBtn = null, questDescription = null;

    [SerializeField]
    private GameObject questPrefab = null;

    [SerializeField]
    private Transform questArea = null;

    public void ShowQuests(QuestGiver questGiver)
    {
        this.questGiver = questGiver;

        foreach (Quest quest in questGiver.MyQuests)
        {
            GameObject go = Instantiate(questPrefab, questArea);
            go.GetComponent<Text>().text = quest.MyTitle;
            go.GetComponent<QGQuestScript>().MyQuest = quest;
        }
    }

    public override void Open(Npc npc)
    {
        ShowQuests((npc as QuestGiver));
        base.Open(npc);
    }

    public void showQuestInfo(Quest quest)
    {
        backBtn.SetActive(true);
        accpetBtn.SetActive(true);
        questArea.gameObject.SetActive(false);
        questDescription.SetActive(true);

        string objective = "\nObjectives\n";

        foreach (Objective obj in quest.MyCollectObjectives)
        {
            objective += obj.MyType + ": " + obj.MyCurrentAmount + "/" + obj.MyAmount + "\n";
        }

        questDescription.GetComponent<Text>().text = string.Format("{0}\n<size=20>{1}</size>\n<size=20>{2}</size>", quest.MyTitle, quest.MyDescription, objective);
    }
}
