using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Bag", menuName ="Items/Bag", order = 1)]
public class Bag : Item, IUseable
{
    [SerializeField]
    private GameObject bagPrefab;

    public BagScript MyBagScript { get; set; }

    public int Slots { get; private set; }

    public void Initialize(int slots)
    {
        Slots = slots;
    }

    public void Use()
    {
        MyBagScript = Instantiate(bagPrefab, InventotyScript.MyInstance.transform).GetComponent<BagScript>();
        MyBagScript.AddSlots(Slots);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
