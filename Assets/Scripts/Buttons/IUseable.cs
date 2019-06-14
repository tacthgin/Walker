using UnityEngine;
using UnityEditor;

public interface IUseable
{
    Sprite MyIcon
    {
        get;
    }

    void Use();
}