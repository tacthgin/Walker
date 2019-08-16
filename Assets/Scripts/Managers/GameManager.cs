using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private Player player = null;

    private Enemy currentTarget;

    void Update()
    {
        ClickTarget();
    }

    private void ClickTarget()
    {
        if(Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero, Mathf.Infinity, LayerMask.GetMask("Clickable"));
            if (hit.collider != null && hit.collider.tag == "Enemy")
            {
                if (currentTarget != null)
                {
                    currentTarget.DeSelect();
                }

                currentTarget = hit.collider.GetComponent<Enemy>();
                player.MyTarget = currentTarget.Select();

                UIManager.MyInstance.showTargetFrame(currentTarget);
            }else
            {
                UIManager.MyInstance.hideTargetFrame();

                if (currentTarget != null)
                {
                    currentTarget.DeSelect();
                }

                currentTarget = null;
                player.MyTarget = null;
            }
        }else if (Input.GetMouseButtonDown(1) && !EventSystem.current.IsPointerOverGameObject())
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero, Mathf.Infinity, LayerMask.GetMask("Clickable"));

            if (hit.collider != null && 
                (hit.collider.tag == "Enemy" || hit.collider.tag == "Interactable") && 
                hit.collider.gameObject.GetComponent<IInteractable>() == player.MyInteractable
               )
            {
                player.Interact();
            }
        }
    }
}
