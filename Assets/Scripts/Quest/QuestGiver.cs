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
        tmpLog.AcceptQuest(quests[0]);
    }
}
