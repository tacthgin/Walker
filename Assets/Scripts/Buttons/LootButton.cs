using UnityEngine;
using UnityEngine.UI;

public class LootButton : MonoBehaviour
{
    [SerializeField]
    private Image icon = null;

    [SerializeField]
    private Text title = null;

    public Image MyIcon { get => icon; }

    public Text MyTitle { get => title; }
}
