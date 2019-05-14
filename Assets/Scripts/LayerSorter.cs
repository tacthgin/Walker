using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LayerSorter : MonoBehaviour
{
    private SpriteRenderer parentRender = null;

    List<Obstacle> obstacles = new List<Obstacle>();

    // Start is called before the first frame update
    void Start()
    {
        parentRender = transform.parent.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Obstacle")
        {
            Obstacle o = collision.GetComponent<Obstacle>();
            if (obstacles.Count == 0 || o.MySpriteRender.sortingOrder - 1 < parentRender.sortingOrder)
            {
                parentRender.sortingOrder = o.MySpriteRender.sortingOrder - 1;
            }
            
            obstacles.Add(o);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Obstacle")
        {
            Obstacle o = collision.GetComponent<Obstacle>();
            obstacles.Remove(o);
            if (obstacles.Count == 0)
            {
                parentRender.sortingOrder = 200;
            }
            else
            {
                obstacles.Sort();
                parentRender.sortingOrder = obstacles[0].MySpriteRender.sortingOrder - 1;
            }
        }
    }
}
