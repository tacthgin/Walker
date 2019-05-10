using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
public abstract class Character : MonoBehaviour
{
    /// <summary>
    /// 血条
    /// </summary>
    [SerializeField]
    protected Stat health;

    public Stat MyHealth
    {
        get { return health; }
    }

    /// <summary>
    /// 初始生命
    /// </summary>
    [SerializeField]
    private float initHealth = 100;

    /// <summary>
    /// 角色速度
    /// </summary>
    [SerializeField]
    protected float speed;

    /// <summary>
    /// 初始化角色方向
    /// </summary>
    protected Vector2 direction;

    protected Animator myAnimator;

    private Rigidbody2D myRigidbody;

    public bool IsMoving
    {
        get
        {
            return direction != Vector2.zero;
        }
    }

    protected bool IsAttacking = false;

    protected Coroutine attackRoutine;

    [SerializeField]
    protected Transform hitBox;

    protected virtual void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();

        health.Initialize(initHealth, initHealth);
    }

    protected virtual void Update()
    {
        HandleLayers();
    }

    protected virtual void FixedUpdate()
    {
        Move();
    }

    protected void Move()
    {
        myRigidbody.velocity = direction.normalized * speed;
    }

    public void HandleLayers()
    {
        if (IsMoving)
        {
            activateLayer("WalkLayer");
            myAnimator.SetFloat("x", direction.x);
            myAnimator.SetFloat("y", direction.y);
            StopAttack();
        }
        else if (IsAttacking)
        {
            activateLayer("AttackLayer");
        }else
        {
            activateLayer("IdleLayer");
        }
    }

    public void activateLayer(string layerName)
    {
        for (int i = 0; i < myAnimator.layerCount; i++)
        {
            myAnimator.SetLayerWeight(i, 0);
        }

        myAnimator.SetLayerWeight(myAnimator.GetLayerIndex(layerName), 1);
    }

    public virtual void StopAttack()
    {
        IsAttacking = false;
        myAnimator.SetBool("attack", IsAttacking);

        if (attackRoutine != null)
        {
            StopCoroutine(attackRoutine);
            attackRoutine = null;
        }
    }

    public virtual void TakeDamage(float damage)
    {
        health.MyCurrentValue -= damage;

        if (health.MyCurrentValue <= 0)
        {
            //死掉
            myAnimator.SetTrigger("die");
        }
    }
}
