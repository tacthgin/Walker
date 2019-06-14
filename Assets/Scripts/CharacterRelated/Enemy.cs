using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Npc
{
    [SerializeField]
    private CanvasGroup healthGroup = null;

    private IState currentState = null;

    public float MyAttackRange { get; set; }

    public float MyAttackTime { get; set; }

    [SerializeField]
    private float initAggroRange = 0;

    public float MyAggroRange { get; set; }

    public Vector3 MyStartPosition { get; set; }


    public bool InRange
    {
        get
        {
            return Vector2.Distance(transform.position, MyTarget.position) <= MyAggroRange;
        }
    }

    protected void Awake()
    {
        MyStartPosition = transform.position;
        MyAggroRange = initAggroRange;
        MyAttackRange = 1;
        ChangeState(new IdleState());
    }

    protected override void Update()
    {
        if (IsAlive)
        {
            if (!IsAttacking)
            {
                MyAttackTime += Time.deltaTime;
            }
            currentState.Update();
        }

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

    public override void TakeDamage(float damage, Transform source)
    {
        if (!(currentState is EvadeState))
        {
            setTarget(source);
            base.TakeDamage(damage, source);

            onHealthChanged(health.MyCurrentValue);
        }
    }

    public void ChangeState(IState newState)
    {
        if (currentState != null)
        {
            currentState.Exit();
        }

        currentState = newState;

        currentState.Enter(this);
    }

    public void setTarget(Transform target)
    {
        if (MyTarget == null && !(currentState is EvadeState))
        {
            float distance = Vector2.Distance(transform.position, target.position);
            MyAggroRange = initAggroRange;
            MyAggroRange += distance;
            MyTarget = target;
        }
    }

    public void Reset()
    {
        MyTarget = null;
        MyAggroRange = initAggroRange;
        MyHealth.MyCurrentValue = MyHealth.MyMaxValue;
        onHealthChanged(health.MyCurrentValue);
    }
}
