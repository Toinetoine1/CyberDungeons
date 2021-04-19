using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public List<GameObject> WeaponList;
    private int onHandWeapon;
    public GameObject currentWeapon;

    public Inventory()
    {
        WeaponList = new List<GameObject>();
    }

    private void setCurrentWeapon()
    {
        currentWeapon = WeaponList[onHandWeapon];
    }

    public void nextWeapon()
    {
        onHandWeapon = (onHandWeapon + 1) % WeaponList.Count;
        setCurrentWeapon();
    }
    
    public void previousWeapon()
    {
        onHandWeapon = (onHandWeapon - 1) % WeaponList.Count;
        setCurrentWeapon();
    }

    public void addWeapon(GameObject Weapon)
    {
        WeaponList.Add(Weapon);
        if (onHandWeapon == null)
            onHandWeapon = 0;
    }
}
