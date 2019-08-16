using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Npc : Character, IInteractable
{
    public virtual void Interact()
    {
        Debug.Log("this will open a dialog with the NPC");
    }

    public virtual void StopInteract()
    {
        Debug.Log("end interactable  with the NPC");
    }
}
