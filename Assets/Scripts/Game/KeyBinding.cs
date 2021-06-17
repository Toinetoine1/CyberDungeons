using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class KeyBinding : MonoBehaviour
{

    public Dictionary<string, KeyCode> KeyCodes;

    private void Awake()
    {
        KeyCodes = new Dictionary<string, KeyCode>();
        RetrieveBindings();
    }

    public void setKey(string key, KeyCode code)
    {
        if (KeyCodes.ContainsKey(key))
        {
            KeyCodes[key] = code;
        }
        else
        {
            KeyCodes.Add(key, code);         
        }
    }

    public void saveBindings()
    {
        foreach (var kvp in KeyCodes)
        {
            PlayerPrefs.SetString(kvp.Key, kvp.Value.ToString());
            Debug.Log("Bind " + kvp.Key + " to " + kvp.Value);
        }
        
        PlayerPrefs.Save();
    }

    public void RetrieveBindings()
    {
        
        setKey("UP", (KeyCode) System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("UP", "Z")));
        setKey("DOWN", (KeyCode) System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("DOWN", "S")));
        setKey("RIGHT", (KeyCode) System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("RIGHT", "D")));
        setKey("LEFT", (KeyCode) System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("LEFT", "Q")));
        setKey("USE", (KeyCode) System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("USE", "E")));
        setKey("DODGE", (KeyCode) System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("DODGE", "Space")));
        setKey("FIRE", (KeyCode) System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("FIRE", "Mouse0")));
        setKey("CHANGEWEAPON", (KeyCode) System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("CHANGEWEAPON", "Mouse2")));
        setKey("RELOAD", (KeyCode) System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("RELOAD", "R")));
        Debug.Log("Bindings retrieved !");
    }

    public void setDefaultBind()
    {
        setKey("UP", KeyCode.Z);
        setKey("DOWN", KeyCode.S);
        setKey("LEFT", KeyCode.Q);
        setKey("RIGHT", KeyCode.D);
        setKey("USE", KeyCode.E);
    }
    
}
