using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using Random = UnityEngine.Random;

public class AudioThug : MonoBehaviour
{
     void Start()
    {
        InvokeRepeating("UpdateSound",0,10);
    }

     public void UpdateSound()
     {
         FindObjectOfType<AudioManager>().Play("ThugAudio");
     }
    
}
