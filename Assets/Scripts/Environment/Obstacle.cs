using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour, IComparable<Obstacle>
{
    public SpriteRenderer MySpriteRender
    {
        get;
        private set;
    }

    private Color defaultColor = Color.white;

    private Color fadeColor = Color.white;

    public int CompareTo(Obstacle other)
    {
        if (MySpriteRender.sortingOrder > other.MySpriteRender.sortingOrder)
        {
            return 1;
        }else if (MySpriteRender.sortingOrder < other.MySpriteRender.sortingOrder)
        {
            return -1;
        }
        return 0;
    }

    // Start is called before the first frame update
    void Start()
    {
        MySpriteRender = GetComponent<SpriteRenderer>();

        defaultColor = MySpriteRender.color;

        fadeColor = defaultColor;
        fadeColor.a = 0.7f;
    }

    public void FadeOut()
    {
        MySpriteRender.color = fadeColor;
    }

    public void FadeIn()
    {
        MySpriteRender.color = defaultColor;
    }
}
