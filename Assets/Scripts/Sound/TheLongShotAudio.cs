using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TheLongShotAudio : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("UpdateSound", 0, 8);// permet la répétition dans un intervalle donnée.
    }

    // Update is called once per frame
    void UpdateSound()
    {
        FindObjectOfType<AudioManager>().Play("TheLongShotAudio");// comme déja vu, cette méthode permet
        // de jouer le son approprié dans la liste des sons de l'AudioManager
    }
}
