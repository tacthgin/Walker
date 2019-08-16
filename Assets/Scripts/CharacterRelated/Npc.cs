using UnityEngine;

public class Npc : MonoBehaviour, IInteractable
{
    [SerializeField]
    private Window window = null;

    public bool IsInteracting { get; set; }

    public virtual void Interact()
    {
        if (!IsInteracting)
        {
            IsInteracting = true;
            window.Open(this);
        }
    }

    public virtual void StopInteract()
    {
        if (IsInteracting)
        {
            IsInteracting = false;
            window.Close();
        }
    }
}
