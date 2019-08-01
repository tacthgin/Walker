using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour, IInteractable
{
    [SerializeField]
    private SpriteRenderer spriteRenderer = null;

    [SerializeField]
    private Sprite openSprite = null, closedSprite = null;

    private bool isOpen = false;

    public void Interact()
    {
        if (isOpen)
        {
            StopInteract();
        }
        else
        {
            isOpen = true;
            spriteRenderer.sprite = openSprite;
        }
    }

    public void StopInteract()
    {
        isOpen = false;
        spriteRenderer.sprite = closedSprite;
    }
}
