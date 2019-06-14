using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class KeybindManager : MonoBehaviour
{
    private static KeybindManager instance;

    public static KeybindManager MyInstance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<KeybindManager>();
            }

            return instance;
        }
    }

    public Dictionary<string, KeyCode> Keybinds { get; private set; }

    public Dictionary<string, KeyCode> ActionBinds { get; private set; }

    private string bindName = "";

    void Start()
    {
        Keybinds = new Dictionary<string, KeyCode>();

        ActionBinds = new Dictionary<string, KeyCode>();

        BindKey("UP", KeyCode.W);
        BindKey("LEFT", KeyCode.A);
        BindKey("DOWN", KeyCode.S);
        BindKey("RIGHT", KeyCode.D);

        BindKey("ACT1", KeyCode.Alpha1);
        BindKey("ACT2", KeyCode.Alpha2);
        BindKey("ACT3", KeyCode.Alpha3);
    }

    public void BindKey(string key, KeyCode keybind)
    {
        Dictionary<string, KeyCode> currentDictionary = Keybinds;

        if (key.Contains("ACT"))
        {
            currentDictionary = ActionBinds;
        }

        if (!currentDictionary.ContainsValue(keybind))
        {
            currentDictionary.Add(key, keybind);
        }else
        {
            string myKey = currentDictionary.FirstOrDefault(x => x.Value == keybind).Key;
            currentDictionary[myKey] = KeyCode.None;
            UIManager.MyInstance.UpdateKeyText(myKey, KeyCode.None);
        }

        currentDictionary[key] = keybind;
        UIManager.MyInstance.UpdateKeyText(key, keybind);
        bindName = string.Empty;
    }
}
