using System;
using System.Collections;
using System.Collections.Generic;
using Game;
using Photon.Realtime;
using UnityEngine;
using Object = UnityEngine.Object;

public class Inventory : MonoBehaviour
{
    public List<GameObject> WeaponListToUse;
    public List<GameObject> WeaponList;
    public GameObject currentWeapon;
    private int onHandWeapon;

    void Awake()
    {
        WeaponListToUse = new List<GameObject>();
        WeaponList[0].SetActive(true);
        onHandWeapon = 0;
        currentWeapon = WeaponList[0];
        addWeapon(currentWeapon);
    }

    private void Start()
    {
        Debug.Log(currentWeapon.name);
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(2))
        {
            nextWeapon();
            FindObjectOfType<AudioManager>().Play("SwapSound");
            if (currentWeapon != null)
            {
                Debug.Log(currentWeapon.name);
            }
            else
            {
                Debug.Log("currentWeapon est vide ??");
            }
        }
    }

    public int IndexOfWeaponToDisappear()
    {
        for (int i = 0; i < WeaponList.Count; i++)
        {
            if (WeaponList[i].name == currentWeapon.name)
            {
                return i;
            }
        }
        return -1;
    }

    private void setCurrentWeapon()
    {
        int indexToChange = IndexOfWeaponToDisappear();
        Debug.Log(indexToChange);
        WeaponList[indexToChange].SetActive(false);
        currentWeapon = WeaponListToUse[onHandWeapon];
        indexToChange = IndexOfWeaponToDisappear();
        if (indexToChange == -1)
        {
            Debug.Log("petit soucis d'index");
        }
        else
        {
            Debug.Log(indexToChange);
            WeaponList[indexToChange].SetActive(true);
        }
    }

    public void nextWeapon()
    {
        onHandWeapon = (onHandWeapon + 1) % WeaponListToUse.Count;
        setCurrentWeapon();
    }
    
    public void previousWeapon()
    {
        onHandWeapon = (onHandWeapon - 1) % WeaponListToUse.Count;
        setCurrentWeapon();
    }

    public void addWeapon(GameObject Weapon)
    {
        WeaponListToUse.Add(Weapon);
    }
}
