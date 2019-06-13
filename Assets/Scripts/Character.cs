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

    public Animator MyAnimator { get; set; }

    private Rigidbody2D myRigidbody;

    public bool IsMoving
    {
        get
        {
            return Direction != Vector2.zero;
        }
    }

    public Vector2 Direction { get; set; }

    public float Speed { get => speed; set => speed = value; }

    public bool IsAttacking { get; set; }

    public bool IsAlive
    {
        get
        {
            return MyHealth.MyCurrentValue > 0;
        }
    }

    protected Coroutine attackRoutine;

    [SerializeField]
    protected Transform hitBox;

    protected virtual void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        MyAnimator = GetComponent<Animator>();

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
        if (IsAlive)
        {
            if (IsMoving)
            {
                activateLayer("WalkLayer");
                MyAnimator.SetFloat("x", Direction.x);
                MyAnimator.SetFloat("y", Direction.y);
            }
            else if (IsAttacking)
            {
                activateLayer("AttackLayer");
            }
            else
            {
                activateLayer("IdleLayer");
            }
        }else
        {
            activateLayer("DeathLayer");
        }
    }

    public void activateLayer(string layerName)
    {
        for (int i = 0; i < MyAnimator.layerCount; i++)
        {
            MyAnimator.SetLayerWeight(i, 0);
        }

        MyAnimator.SetLayerWeight(MyAnimator.GetLayerIndex(layerName), 1);
    }

    public virtual void TakeDamage(float damage)
    {
        health.MyCurrentValue -= damage;

        if (health.MyCurrentValue <= 0)
        {
            Direction = Vector2.zero;
            myRigidbody.velocity = Direction;
            //死掉
            MyAnimator.SetTrigger("die");
        }
    }
}
