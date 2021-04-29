using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPiouPiouGuy : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("UpdateSound", 2, 14);
    }

    // Update is called once per frame
    void UpdateSound()
    {
        FindObjectOfType<AudioManager>().Play("PiouPiouGuySound");
    }
}
