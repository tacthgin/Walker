using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    private static Player instance;

    public static Player MyInstance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<Player>();
            }

            return instance;
        }
    }

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

    /// <summary>
    /// 技能的位置点起始索引，0是down
    /// </summary>
    private int exitIndex = 0;

    [SerializeField]
    private Block[] blocks = null;

    private Vector3 min, max;

    [SerializeField]
    private GearSocket[] gearSockets = null;

    protected override void Start()
    {
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
        if (Input.GetKey(KeybindManager.MyInstance.Keybinds["UP"]))
        {
            exitIndex = 3;
            Direction += Vector2.up;
        }

        if (Input.GetKey(KeybindManager.MyInstance.Keybinds["DOWN"]))
        {
            exitIndex = 0;
            Direction += Vector2.down;
        }

        if (Input.GetKey(KeybindManager.MyInstance.Keybinds["LEFT"]))
        {
            exitIndex = 1;
            Direction += Vector2.left;
        }

        if (Input.GetKey(KeybindManager.MyInstance.Keybinds["RIGHT"]))
        {
            exitIndex = 2;
            Direction += Vector2.right;
        }

        if (IsMoving)
        {
            StopAttack();
        }

        foreach(string action in KeybindManager.MyInstance.ActionBinds.Keys)
        {
            if (Input.GetKeyDown(KeybindManager.MyInstance.ActionBinds[action]))
            {
                UIManager.MyInstance.ClickActionButton(action);
            }
        }

        //testing
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            MyHealth.MyCurrentValue -= 10;
        }
        //testing
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            MyHealth.MyCurrentValue += 10;
        }
    }

    public void SetLimits(Vector3 min, Vector3 max)
    {
        this.min = min;
        this.max = max;
    }

    private IEnumerator Attack(string spellName)
    {
        Transform currentTarget = MyTarget;

        Spell newSpell = SpellBook.MyInstance.CastSpell(spellName);
        IsAttacking = true;
        MyAnimator.SetBool("attack", IsAttacking);

        foreach (GearSocket gearSocket in gearSockets)
        {
            gearSocket.MyAnimator.SetBool("attack", IsAttacking);
        }

        yield return new WaitForSeconds(newSpell.MyCastTime);

        if (currentTarget != null && InLineOfSight())
        {
            SpellScript s = Instantiate(newSpell.MySpellPrefab, exitPoints[exitIndex].position, Quaternion.identity).GetComponent<SpellScript>();
            s.Initialize(currentTarget, newSpell.MyDamage, transform);
        }

        StopAttack();
    }

    public void CastSpell(string spellName)
    {
        Block();
        if (MyTarget != null && MyTarget.GetComponentInParent<Character>().IsAlive && !IsAttacking && !IsMoving && InLineOfSight())
        {
            attackRoutine = StartCoroutine(Attack(spellName));
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
        SpellBook.MyInstance.StopCasting();

        IsAttacking = false;
        MyAnimator.SetBool("attack", IsAttacking);

        if (attackRoutine != null)
        {
            StopCoroutine(attackRoutine);
            attackRoutine = null;
        }
    }

    public override void HandleLayers()
    {
        base.HandleLayers();

        if (IsMoving)
        {
            foreach (GearSocket gearSocket in gearSockets)
            {
                gearSocket.SetXAndY(Direction.x, Direction.y);
            }
        }
    }

    public override void ActivateLayer(string layerName)
    {
        base.ActivateLayer(layerName);

        foreach (GearSocket gearSocket in gearSockets)
        {
            gearSocket.ActivateLayer(layerName);
        }
    }
}
