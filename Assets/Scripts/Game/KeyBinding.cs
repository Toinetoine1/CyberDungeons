using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class KeyBinding : MonoBehaviour
{
    
    public List<string> Names;
    public List<KeyCode> KeyCodes;

    private void Awake()
    {
        RetrieveBindings();
    }

    public void setKey(string key, KeyCode code)
    {
        if (Names.Contains(key))
        {
            int indexKey = Names.IndexOf(key);
            KeyCodes[indexKey] = code;
        }
        else
        {
            Names.Add(key);
            KeyCodes.Add(code);            
        }
    }

    public void saveBindings()
    {
        Debug.Log(JsonUtility.ToJson(Names));
        Debug.Log(JsonUtility.ToJson(KeyCodes));
        PlayerPrefs.SetString("Key", JsonUtility.ToJson(Names));
        PlayerPrefs.SetString("KeyCodes", JsonUtility.ToJson(KeyCodes));
        PlayerPrefs.Save();
    }

    public void RetrieveBindings()
    {
        if (PlayerPrefs.HasKey("Key"))
        {
            Names.Clear();
            KeyCodes.Clear();
            string KeyJson = PlayerPrefs.GetString("Key");
            string KeyCodesJson = PlayerPrefs.GetString("KeyCodes");
            Names = JsonUtility.FromJson<List<string>>(KeyJson);
            KeyCodes = JsonUtility.FromJson<List<KeyCode>>(KeyCodesJson);
        }
        else
        {
            setDefaultBind();
        }
    }

    public void setDefaultBind()
    {
        setKey("UP", KeyCode.Z);
        setKey("DOWN", KeyCode.S);
        setKey("LEFT", KeyCode.Q);
        setKey("RIGHT", KeyCode.D);
        setKey("USE", KeyCode.E);
    }

    public KeyCode getKeyCode(string key)
    {
        int tmp = Names.IndexOf(key);
        return KeyCodes[tmp];
    }

    private string namesToString()
    {
        string output = "";
        foreach (var Key in Names)
        {
            
        }
    }
}
