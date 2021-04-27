using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamesEvents : MonoBehaviour
{
    public static GamesEvents current;
    // Start is called before the first frame update
    private void Awake()
    {
        current = this;
    }
    
    public event Action OnColliderSpawnEvent;

    public void ColliderSpawnTrigger()
    {
        if (OnColliderSpawnEvent != null)
        {
            OnColliderSpawnEvent();
        }
    }
}
