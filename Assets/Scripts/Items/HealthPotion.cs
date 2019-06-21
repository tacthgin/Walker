﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="HealthPotion",menuName ="Items/Potion",order =1)]
public class HealthPotion : Item, IUseable
{
    [SerializeField]
    private int health = 0;

    public void Use()
    {
        if (!Player.MyInstance.MyHealth.IsFull)
        {
            Remove();

            Player.MyInstance.MyHealth.MyCurrentValue += health;
        }
    }
}
