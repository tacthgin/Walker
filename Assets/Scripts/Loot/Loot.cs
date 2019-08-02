using UnityEngine;

[System.Serializable]
public class Loot
{
    [SerializeField]
    private Item item = null;

    [SerializeField]
    private float dropChance = .0f;

    public Item MyItem { get => item; }

    public float MyDropChance { get => dropChance; }
}
  
