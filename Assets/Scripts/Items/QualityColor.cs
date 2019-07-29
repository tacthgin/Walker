using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Quality { Common, Uncommon, Rare, Epic }

public class QualityColor
{
    public static Dictionary<Quality, string> MyColors { get; } = new Dictionary<Quality, string>()
    {
        { Quality.Common, "#ffffffff" },
        { Quality.Uncommon, "#00ff00ff" },
        { Quality.Rare, "#0e6becff" },
        { Quality.Epic, "#a712dbff" },
    };
}
