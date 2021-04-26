using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public List<GameObject> WeaponList;
    private int onHandWeapon;
    public GameObject currentWeapon;

    private void Awake()
    {
        WeaponList = new List<GameObject>();
        addWeapon(GameObject.Find("The Gun"));
        onHandWeapon = 0;
        currentWeapon = WeaponList[onHandWeapon];
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
    }
}
