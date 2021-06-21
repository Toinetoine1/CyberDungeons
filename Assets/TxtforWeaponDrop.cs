using System.Collections;
using System.Collections.Generic;
using Game;
using Map;
using Photon.Pun;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TxtforWeaponDrop : MonoBehaviour
{
    public GameObject HereToDisplay;
    private float timeRemaining;
    private bool[] arrayForDisplay;
    private int i;

    void Start()
    {
        i = 0;
        arrayForDisplay = new[] {false, false, false};
        timeRemaining = 5;
    }
    // Update is called once per frame
    void Update()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            bool yeet = FindObjectOfType<TriggerChest>().HasTriggered;
            if (yeet != null)
            {
                if (yeet && !arrayForDisplay[i])
                {
                    Display();
                    arrayForDisplay[i] = true;
                }

                if (arrayForDisplay[i])
                {
                    if (timeRemaining > 0)
                    {
                        timeRemaining -= Time.deltaTime;
                    }

                    if (timeRemaining <= 0)
                    {
                        HereToDisplay.SetActive(false);
                    }
                }

                if (MapGenerator.level == 2 && !arrayForDisplay[1])
                {
                    i = 1;
                    timeRemaining = 5;
                }

                if (MapGenerator.level == 3 && !arrayForDisplay[2])
                {
                    i = 2;
                    timeRemaining = 5;
                }
            }
        }
    }
    void Display()
    {
        HereToDisplay.SetActive(true);
        int NewWeapon = FindObjectOfType<Inventory>().WeaponListToUse.Count - 1;
        HereToDisplay.GetComponent<Text>().text = "New Weapon !" + FindObjectOfType<Inventory>().WeaponListToUse[NewWeapon].name;
    }
}
