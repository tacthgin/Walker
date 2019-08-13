using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestGiver : MonoBehaviour
{
    [SerializeField]
    private Quest[] quests;

    //debuging
    [SerializeField]
    private QuestLog tmpLog;

    private void Awake()
    {
        foreach (Quest quest in quests)
        {
            tmpLog.AcceptQuest(quest);
        }
    }
}
