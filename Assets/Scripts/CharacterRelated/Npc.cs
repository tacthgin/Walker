using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void HealthChanged(float health);
public delegate void CharacterRemoved();

public class Npc : Character
{
    public event HealthChanged healthChanged;

    public event CharacterRemoved characterRemoved;

    [SerializeField]
    private Sprite portrait = null;

    public Sprite MyPortrait
    {
        get { return portrait; }
    }

    public virtual void DeSelect()
    {
        healthChanged -= new HealthChanged(UIManager.MyInstance.UpdateTargetFrame);
        characterRemoved -= new CharacterRemoved(UIManager.MyInstance.hideTargetFrame);
    }

    public virtual Transform Select()
    {
        return hitBox;
    }

    public void onHealthChanged(float health)
    {
        healthChanged?.Invoke(health);
    }

    public void onCharacterRemoved()
    {
        characterRemoved?.Invoke();
        Destroy(gameObject);
    }

    public virtual void Interact()
    {
        Debug.Log("this will open a dialog with the NPC");
    }
}
