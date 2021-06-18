using System.Collections;
using System.Collections.Generic;
using Game;
using Map;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TxtforWeaponDrop : MonoBehaviour
{
    public GameObject HereToDisplay;
    private float timeRemaining;
    private bool done;

    void Start()
    {
        done = false;
        timeRemaining = 5;
    }
    // Update is called once per frame
    void Update()
    {
        if (FindObjectOfType<TriggerChest>().HasTriggered && !done)
        {
            Display();
            done = true;
        }
        if (done)
        {
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
            }

            if (timeRemaining <= 0 )
            {
                HereToDisplay.SetActive(false);
            }
        }
        if (FindObjectOfType<Health>().gameObject.CompareTag("Boss"))
        {
            done = false;
            timeRemaining = 5;
        }
    }
    void Display()
    {
        HereToDisplay.SetActive(true);
        int NewWeapon = FindObjectOfType<Inventory>().WeaponListToUse.Count - 1;
        HereToDisplay.GetComponent<Text>().text = "New Weapon !" + FindObjectOfType<Inventory>().WeaponListToUse[NewWeapon].name;
    }
}
