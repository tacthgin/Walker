using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSocket : GearSocket
{
    private float currentY = .0f;

    [SerializeField]
    private SpriteRenderer parentSpriteRenderer = null;

    public override void SetXAndY(float x, float y)
    {
        base.SetXAndY(x, y);

        if (currentY != y)
        {
            if (y == 1)
            {
                //背面
                spriteRenderer.sortingOrder = parentSpriteRenderer.sortingOrder - 1;
            }
            else
            {
                //正面
                spriteRenderer.sortingOrder = parentSpriteRenderer.sortingOrder + 5;
            }
        }
    }
}
