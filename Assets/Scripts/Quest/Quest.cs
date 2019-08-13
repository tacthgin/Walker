using UnityEngine;

[System.Serializable]
public class Quest
{
    [SerializeField]
    private string title;

    [SerializeField]
    private string description;

    [SerializeField]
    private CollectObjective[] collectObjectives = null;

    public string MyTitle { get => title; }

    public QuestScript MyQuestScrit { get; set; }

    public string MyDescription { get => description; }

    public CollectObjective[] MyCollectObjectives { get => collectObjectives; }
}

[System.Serializable]
public abstract class Objective
{
    [SerializeField]
    private int amount = 0;

    private int currentAmount = 0;

    [SerializeField]
    private string type;

    public int MyAmount { get => amount; }

    public int MyCurrentAmount { get => currentAmount; }

    public string MyType { get => type; }
}

[System.Serializable]
public class CollectObjective : Objective
{
  
}
