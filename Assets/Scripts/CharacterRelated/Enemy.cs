using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void HealthChanged(float health);
public delegate void CharacterRemoved();

public class Enemy : Character, IInteractable
{
    public event HealthChanged healthChanged;

    public event CharacterRemoved characterRemoved;

    [SerializeField]
    private CanvasGroup healthGroup = null;

    [SerializeField]
    private Sprite portrait = null;

    private IState currentState = null;

    public float MyAttackRange { get; set; }

    public float MyAttackTime { get; set; }

    [SerializeField]
    private float initAggroRange = 0;

    public float MyAggroRange { get; set; }

    public Vector3 MyStartPosition { get; set; }

    private LootTable lootTable;

    public Sprite MyPortrait
    {
        get { return portrait; }
    }

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

        lootTable = GetComponent<LootTable>();
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

    public Transform Select()
    {
        healthGroup.alpha = 1;
        return hitBox;
    }

    public void DeSelect()
    {
        healthGroup.alpha = 0;

        healthChanged -= new HealthChanged(UIManager.MyInstance.UpdateTargetFrame);
        characterRemoved -= new CharacterRemoved(UIManager.MyInstance.hideTargetFrame);
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

    public void Interact()
    {
        if (!IsAlive)
        {
            lootTable.ShowLoot();
        }
    }

    public void StopInteract()
    {
        LootWindow.MyInstance.Close();
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
}
