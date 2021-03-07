using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KeyBindingsMenu : MonoBehaviour
{

    private GameObject[] KeyBindButton;
    private KeyBinding _keyBinding;

    private string BindingName = string.Empty;
    
    // Start is called before the first frame update
    void Start()
    {
        _keyBinding = GetComponent<KeyBinding>();
        KeyBindButton = GameObject.FindGameObjectsWithTag("KeyBind");
        foreach (var button in KeyBindButton)
        {
            string name = button.name;
            Text tmp = button.GetComponentInChildren<Text>();
            tmp.text = _keyBinding.KeyCodes[name].ToString();
        }
    }

    public void onClickSet(string key)
    {
        BindingName = key;
        Debug.Log(key);
    }

    private void SetText(string key, KeyCode code)
    {
        Text tmp = Array.Find(KeyBindButton, x => x.name == key).GetComponentInChildren<Text>();
        tmp.text = code.ToString();
    }

    private void OnGUI()
    {
        if (BindingName != string.Empty)
        {
            Event e = Event.current;
            if (e.isKey)
            {
                _keyBinding.setKey(BindingName, e.keyCode);
                SetText(BindingName, e.keyCode);
                BindingName = string.Empty;
            }
        }
    }
}
