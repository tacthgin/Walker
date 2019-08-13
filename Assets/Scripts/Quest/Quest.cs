using UnityEngine;

[System.Serializable]
public class Quest
{
    [SerializeField]
    private string title;

    public string MyTitle { get => title; }
}
