using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManagement : MonoBehaviour
{
    private Inventory _inventory;
    public GameObject FirstWeapon;

    void Start()
    {
        _inventory = new Inventory();
        _inventory.addWeapon(FirstWeapon);
    }

    void Update()
    {
        _inventory.currentWeapon.transform.LookAt(Input.mousePosition);
    }

    public void fire()
    {
        _inventory.currentWeapon.BroadcastMessage("fire");
    }

    public void nextWeapon()
    {
        Destroy(_inventory.currentWeapon);
        _inventory.nextWeapon();
        GameObject.Instantiate(_inventory.currentWeapon, transform);
    }

    public void previousWeapon()
    {
        Destroy(_inventory.currentWeapon);
        _inventory.previousWeapon();
        GameObject.Instantiate(_inventory.currentWeapon, transform);
    }
}
