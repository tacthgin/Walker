using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Npc
{
    [SerializeField]
    private CanvasGroup healthGroup = null;

    private Transform target;

    public Transform Target { set => target = value; }

    protected override void Update()
    {
        FollowTarget();
        base.Update();
    }

    public override Transform Select()
    {
        healthGroup.alpha = 1;
        return base.Select();
    }

    public override void DeSelect()
    {
        healthGroup.alpha = 0;
        base.DeSelect();
    }

    public override void TakeDamage(float damage)
    {
        base.TakeDamage(damage);

        onHealthChanged(health.MyCurrentValue);
    }

    private void FollowTarget()
    {
        if (target != null)
        {
            direction = (target.transform.position - transform.position).normalized;
            transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
        }else
        {
            direction = Vector2.zero;
        }
    }
}
