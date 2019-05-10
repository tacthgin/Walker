using System;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField]
    private Texture2D[] mapData = null;

    [SerializeField]
    private MapElement[] mapElements = null;

    [SerializeField]
    private Sprite defaultTile;

    private Vector3 WorldStartPos
    {
        get
        {
            return Camera.main.ScreenToWorldPoint(new Vector3(0, 0));
        }
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

[Serializable]
public class MapElement
{
    [SerializeField]
    private string tileTag;

    [SerializeField]
    private Color color;

    private GameObject elementPrefab;

    public string TileTag { get => tileTag; }

    public Color Color { get => color; }

    public GameObject ElementPrefab { get => elementPrefab; }
    
}
