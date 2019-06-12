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
    private float speed;

    /// <summary>
    /// 初始化角色方向
    /// </summary>
    private Vector2 direction;

    protected Animator myAnimator;

    private Rigidbody2D myRigidbody;

    public bool IsMoving
    {
        get
        {
            return Direction != Vector2.zero;
        }
    }

    public Vector2 Direction { get => direction; set => direction = value; }

    public float Speed { get => speed; set => speed = value; }

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
        myRigidbody.velocity = Direction.normalized * Speed;
    }

    public void HandleLayers()
    {
        if (IsMoving)
        {
            activateLayer("WalkLayer");
            myAnimator.SetFloat("x", Direction.x);
            myAnimator.SetFloat("y", Direction.y);
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
