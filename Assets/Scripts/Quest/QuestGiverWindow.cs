using UnityEngine;
using UnityEngine.UI;

public class QuestGiverWindow : Window
{
    private QuestGiver questGiver;

    [SerializeField]
    private GameObject questPrefab;

    [SerializeField]
    private Transform questArea;

    public void ShowQuests(QuestGiver questGiver)
    {
        this.questGiver = questGiver;

        foreach (Quest quest in questGiver.MyQuests)
        {
            GameObject go = Instantiate(questPrefab, questArea);
            go.GetComponent<Text>().text = quest.MyTitle;
        }
    }

    public override void Open(Npc npc)
    {
        ShowQuests((npc as QuestGiver));
        base.Open(npc);
    }
}
