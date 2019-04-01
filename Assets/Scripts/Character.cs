using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Character : MonoBehaviour
{
    /// <summary>
    /// 角色速度
    /// </summary>
    [SerializeField]
    protected float speed;

    /// <summary>
    /// 初始化角色方向
    /// </summary>
    protected Vector2 direction = Vector2.zero;

    protected Vector2 walkDirection = Vector2.zero;

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

    protected virtual void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
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

    public void StopAttack()
    {
        if (attackRoutine != null)
        {
            Debug.Log("attack stop");
            StopCoroutine(attackRoutine);
            attackRoutine = null;
            IsAttacking = false;
            myAnimator.SetBool("attack", IsAttacking);
        }
    }
}
