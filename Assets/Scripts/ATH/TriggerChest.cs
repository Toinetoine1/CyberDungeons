using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;
using Random = System.Random;

public class TriggerChest : MonoBehaviour
{
    private bool HasTriggered;

    public List<GameObject> WeaponList;
    
    void Start()
    {
        HasTriggered = false;
    }
    

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!HasTriggered)
        {
            List<GameObject> WeaponListToUse = FindObjectOfType<Inventory>().WeaponListToUse;
            if (WeaponList.Count != WeaponListToUse.Count)
            {
                GameObject newWeapon = ChooseWeapon(WeaponListToUse);
                while (newWeapon == null)
                {
                    newWeapon = ChooseWeapon(WeaponListToUse);
                }

                FindObjectOfType<Inventory>().addWeapon(newWeapon);
            }
            HasTriggered = true;
        }
    }

    private int GenerateRandom()
    {
        Random rand = new Random();
        int randomWeapon = rand.Next(8);
        return randomWeapon;
    }

    public GameObject ChooseWeapon(List<GameObject> WeaponListToUse)
    {
        int randomWeapon = GenerateRandom();
        GameObject newWeapon = WeaponList[randomWeapon];
        int length = WeaponListToUse.Count;
        for (int i = 0; i < length; i++)
        {
            if (WeaponListToUse[i] == newWeapon)
            {
                return null;
            }
        }
        return newWeapon;
    }
    
}
