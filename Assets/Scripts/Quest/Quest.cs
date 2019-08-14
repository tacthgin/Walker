using UnityEngine;

[System.Serializable]
public class Quest
{
    [SerializeField]
    private string title = string.Empty;

    [SerializeField]
    private string description = string.Empty;

    [SerializeField]
    private CollectObjective[] collectObjectives = null;

    public string MyTitle { get => title; }

    public QuestScript MyQuestScrit { get; set; }

    public string MyDescription { get => description; }

    public CollectObjective[] MyCollectObjectives { get => collectObjectives; }

    public bool IsComplete
    {
        get
        {
            foreach (Objective o in collectObjectives)
            {
                if (!o.IsComplete)
                {
                    return false;
                }
            }

            return true;
        }
    }
}

[System.Serializable]
public abstract class Objective
{
    [SerializeField]
    private int amount = 0;

    [SerializeField]
    private string type = string.Empty;

    public int MyAmount { get => amount; }

    public int MyCurrentAmount { get; set; }

    public string MyType { get => type; }

    public bool IsComplete
    {
        get
        {
            return MyCurrentAmount >= MyAmount;
        }
    }
}

[System.Serializable]
public class CollectObjective : Objective
{
    public void UpdateItemCount(Item item)
    {
        if (MyType.ToLower() == item.MyTitle.ToLower())
        {
            MyCurrentAmount = InventotyScript.MyInstance.GetItemCount(item.MyTitle);
            Questlog.MyInstance.UpdateSelected();
            Questlog.MyInstance.CheckCompletion();
        }
    }
}
