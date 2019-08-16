using UnityEngine;
using UnityEngine.UI;

public class QGQuestScript : MonoBehaviour
{
    public Quest MyQuest { get; set; }

    public void Select()
    {
        GetComponent<Text>().color = Color.red;
        QuestGiverWindow.MyInstance.showQuestInfo(MyQuest);
    }

    public void DeSelect()
    {
        GetComponent<Text>().color = Color.white;
    }
}
