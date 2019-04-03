using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    [SerializeField]
    private Stat health;

    [SerializeField]
    private Stat mana;

    /// <summary>
    /// 初始生命
    /// </summary>
    private float initHealth = 100;

    /// <summary>
    /// 初始魔法
    /// </summary>
    private float initMana = 50;

    [SerializeField]
    private GameObject[] spellPrefabs;

    [SerializeField]
    private Transform[] exitPoints;

    /// <summary>
    /// 技能的位置点起始索引，0是down
    /// </summary>
    private int exitIndex = 0;

    [SerializeField]
    private Block[] blocks;

    public Transform MyTarget { get; set; }

    protected override void Start()
    {
        health.Initialize(initHealth, initHealth);
        mana.Initialize(initMana, initMana);
        base.Start();
    }

    protected override void Update()
    {
        GetInput();
        base.Update();
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
    }

    private void GetInput()
    {
        direction = Vector2.zero;
        if (Input.GetKey(KeyCode.W))
        {
            exitIndex = 3;
            direction += Vector2.up;
        }

        if (Input.GetKey(KeyCode.S))
        {
            exitIndex = 0;
            direction += Vector2.down;
        }

        if (Input.GetKey(KeyCode.A))
        {
            exitIndex = 1;
            direction += Vector2.left;
        }

        if (Input.GetKey(KeyCode.D))
        {
            exitIndex = 2;
            direction += Vector2.right;
        }

        if (Input.GetKeyDown(KeyCode.H))
        {
            health.MyCurrentValue += 10;
        }

        if (Input.GetKeyDown(KeyCode.G))
        {
            health.MyCurrentValue -= 10;
        }
    }

    private IEnumerator Attack(int spellIndex)
    {
        IsAttacking = true;
        myAnimator.SetBool("attack", IsAttacking);

        yield return new WaitForSeconds(1);

        Spell s = Instantiate(spellPrefabs[spellIndex], exitPoints[exitIndex].position, Quaternion.identity).GetComponent<Spell>();
        s.MyTarget = MyTarget;

        StopAttack();
    }

    public void CastSpell(int spellIndex)
    {
        Block();
        if (MyTarget != null && !IsAttacking && !IsMoving && InLineOfSight())
        {
            attackRoutine = StartCoroutine(Attack(spellIndex));
        }
    }

    private bool InLineOfSight()
    {
        Vector2 targetDirection = (MyTarget.position - transform.position).normalized;
        RaycastHit2D hit = Physics2D.Raycast(transform.position, targetDirection, Vector2.Distance(transform.position, MyTarget.position), LayerMask.GetMask("Block"));
        return hit.collider == null;
    }

    private void Block()
    {
        foreach (Block b in blocks)
        {
            b.Deactivate();
        }

        blocks[exitIndex].Activate();
    }
}
