using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TheLongShotAudio : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("UpdateSound", 4, 19);
    }

    // Update is called once per frame
    void UpdateSound()
    {
        FindObjectOfType<AudioManager>().Play("TheLongShotAudio");
    }
}
