﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestGiver : Npc
{
    [SerializeField]
    private Quest[] quests = null;

    //debuging
    [SerializeField]
    private Questlog tmpLog = null;

    private void Awake()
    {
        foreach (Quest quest in quests)
        {
            tmpLog.AcceptQuest(quest);
        }
    }
}
