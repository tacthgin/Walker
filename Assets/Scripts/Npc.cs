using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void HealthChanged(float health);

public class Npc : Character
{
    public event HealthChanged healthChanged;

    public virtual void DeSelect()
    {

    }

    public virtual Transform Select()
    {
        return hitBox;
    }

    public void onHealthChanged(float health)
    {
        healthChanged?.Invoke(health);
    }
}
