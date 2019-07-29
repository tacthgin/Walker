using System;
using UnityEngine;

[Serializable]
public class Spell : IUseable, IMoveable, IDescribable
{
    [SerializeField]
    private string name = "";

    [SerializeField]
    private int damage = 0;

    [SerializeField]
    private Sprite icon = null;

    [SerializeField]
    private float speed = 0;

    [SerializeField]
    private float castTime = 0;

    [SerializeField]
    private GameObject spellPrefab = null;

    [SerializeField]
    private string description = string.Empty;

    [SerializeField]
    private Color barColor = Color.white;

    public string MyName { get => name; }

    public int MyDamage { get => damage; }

    public Sprite MyIcon { get => icon; }

    public float MySpeed { get => speed; }

    public float MyCastTime { get => castTime; }

    public GameObject MySpellPrefab { get => spellPrefab; }

    public Color MyBarColor { get => barColor; }

    public string GetDescription()
    {
        return string.Format("{0}\nCast time: {1} seconds(s)\n<color=#ffd111>{2}\nThat causes {3} damage</color>", name, MyCastTime, description, MyDamage);
    }

    public void Use()
    {
        Player.MyInstance.CastSpell(MyName);
    }
}
