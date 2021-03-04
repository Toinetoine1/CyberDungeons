using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class AudioSpeedy : MonoBehaviour
{
     void Start()
    {
        InvokeRepeating("UpdateSound",0,8);
    }

    void UpdateSound()
    {
        FindObjectOfType<AudioManager>().Play("SpeedyAudio");
    }
}
