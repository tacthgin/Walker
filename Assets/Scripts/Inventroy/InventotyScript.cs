using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventotyScript : MonoBehaviour
{
    private static InventotyScript instance;

    public static InventotyScript MyInstance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<InventotyScript>();
            }

            return instance;
        }
    }

    //For debugging
    [SerializeField]
    private Item[] items;

    private void Awake()
    {
        Bag bag = (Bag)Instantiate(items[0]);
        bag.Initialize(20);
        bag.Use();
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
