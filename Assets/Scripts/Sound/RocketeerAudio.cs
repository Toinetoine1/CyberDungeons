using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketeerAudio : MonoBehaviour
{
    void Start()
    {
        InvokeRepeating("UpdateSound", 0, 10);// permet la répétition dans un intervalle donnée.
    }
    void UpdateSound()
    {
        FindObjectOfType<AudioManager>().Play("RocketeerSound"); // comme déja vu, cette méthode permet
                                                                     // de jouer le son approprié dans la liste des sons de l'AudioManager
    }
}
