using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Npc : Character
{
    public virtual void DeSelect()
    {

    }

    public virtual Transform Select()
    {
        return hitBox;
    }
}
