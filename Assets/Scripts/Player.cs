using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    [SerializeField]
    private Stat mana = null;

    /// <summary>
    /// 初始魔法
    /// </summary>
    private float initMana = 50;

    /// <summary>
    /// 法杖发出的位置
    /// </summary>
    [SerializeField]
    private Transform[] exitPoints = null;

    private SpellBook spellBook = null;

    /// <summary>
    /// 技能的位置点起始索引，0是down
    /// </summary>
    private int exitIndex = 0;

    [SerializeField]
    private Block[] blocks = null;

    private Vector3 min, max;

    public Transform MyTarget { get; set; }

    protected override void Start()
    {
        spellBook = GetComponent<SpellBook>();
        mana.Initialize(initMana, initMana);
        base.Start();
    }

    protected override void Update()
    {
        GetInput();

        transform.position = new Vector3(Mathf.Clamp(transform.position.x, min.x, max.x), Mathf.Clamp(transform.position.y, min.y, max.y), transform.position.z);

        base.Update();
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
    }

    private void GetInput()
    {
        Direction = Vector2.zero;
        if (Input.GetKey(KeyCode.W))
        {
            exitIndex = 3;
            Direction += Vector2.up;
        }

        if (Input.GetKey(KeyCode.S))
        {
            exitIndex = 0;
            Direction += Vector2.down;
        }

        if (Input.GetKey(KeyCode.A))
        {
            exitIndex = 1;
            Direction += Vector2.left;
        }

        if (Input.GetKey(KeyCode.D))
        {
            exitIndex = 2;
            Direction += Vector2.right;
        }

        if (Input.GetKeyDown(KeyCode.H))
        {
            health.MyCurrentValue += 10;
        }

        if (Input.GetKeyDown(KeyCode.G))
        {
            health.MyCurrentValue -= 10;
        }

        if (IsMoving)
        {
            StopAttack();
        }
    }

    public void SetLimits(Vector3 min, Vector3 max)
    {
        this.min = min;
        this.max = max;
    }

    private IEnumerator Attack(int spellIndex)
    {
        Transform currentTarget = MyTarget;

        Spell newSpell = spellBook.CastSpell(spellIndex);
        IsAttacking = true;
        MyAnimator.SetBool("attack", IsAttacking);

        yield return new WaitForSeconds(newSpell.MyCastTime);

        if (currentTarget != null && InLineOfSight())
        {
            SpellScript s = Instantiate(newSpell.MySpellPrefab, exitPoints[exitIndex].position, Quaternion.identity).GetComponent<SpellScript>();
            s.Initialize(currentTarget, newSpell.MyDamage);
        }

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
        if (MyTarget != null)
        {
            Vector2 targetDirection = (MyTarget.position - transform.position).normalized;
            RaycastHit2D hit = Physics2D.Raycast(transform.position, targetDirection, Vector2.Distance(transform.position, MyTarget.position), LayerMask.GetMask("Block"));
            return hit.collider == null;
        }

        return false;
    }

    private void Block()
    {
        foreach (Block b in blocks)
        {
            b.Deactivate();
        }

        blocks[exitIndex].Activate();
    }

    public void StopAttack()
    {
        spellBook.StopCasting();

        IsAttacking = false;
        MyAnimator.SetBool("attack", IsAttacking);

        if (attackRoutine != null)
        {
            StopCoroutine(attackRoutine);
            attackRoutine = null;
        }
    }
}
