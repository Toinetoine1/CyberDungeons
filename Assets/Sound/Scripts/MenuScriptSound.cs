using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using UnityEditor;
using UnityEngine;

public class MenuScriptSound : MonoBehaviour
{
    private static MenuScriptSound instance = null;
    
    public void Restart()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    private void Awake()
    {
        DontDestroyOnLoad(transform.gameObject);
    }
}
