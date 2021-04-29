using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomatorAudio : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("UpdateSound", 0, 10);
    }

    // Update is called once per frame
    void UpdateSound()
    {
        FindObjectOfType<AudioManager>().Play("TheRandomatorSound");
    }
}
