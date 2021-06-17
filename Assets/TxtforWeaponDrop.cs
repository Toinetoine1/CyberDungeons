using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TxtforWeaponDrop : MonoBehaviour
{
    public GameObject HereToDisplay;
    private bool done;
    private float timeTowait;
    private bool[] arrayForDisplay;
    private int i;
    
    void Start()
    {
        HereToDisplay.SetActive(false);
        done = false;
        timeTowait = 5;
        arrayForDisplay = new[] {false, false, false};
        i = 0;
    }
    // Update is called once per frame
    void Update()
    {
        int NbrOfWeapon = FindObjectOfType<Inventory>().WeaponListToUse.Count;
        if (NbrOfWeapon == 3  && arrayForDisplay[1] == false || NbrOfWeapon == 4 && arrayForDisplay[2] == false)
        {
            done = false;
            i++;
        }
        bool HasToDisplay = FindObjectOfType<TriggerChest>().HasTriggered;
        if (HasToDisplay && !done)
        {
            Display();
            if (timeTowait > 0)
            {
                timeTowait -= Time.deltaTime;
            }
            if (timeTowait <= 0)
            {
                UnDisplay();
                done = true;
                arrayForDisplay[i] = true;
            }
        }
    }

    void Display()
    {
        HereToDisplay.SetActive(true);
        int NewWeapon = FindObjectOfType<Inventory>().WeaponListToUse.Count - 1;
        HereToDisplay.GetComponent<Text>().text = "New Weapon !" + FindObjectOfType<Inventory>().WeaponListToUse[NewWeapon].name;
    }

    void UnDisplay()
    {
        HereToDisplay.SetActive(false);
    }
}
