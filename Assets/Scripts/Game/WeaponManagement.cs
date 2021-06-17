using System;
using System.Collections;
using System.Collections.Generic;
using Game;
using UnityEngine;

public class WeaponManagement : MonoBehaviour
{
    public Inventory _inventory;
    public Camera CurrentCamera;

    public void fire()
    {
        Weapon currWeapon = _inventory.currentWeapon.GetComponent<Weapon>();
        currWeapon.fire(CurrentCamera, transform);
    }

    public void reloadOnPress()
    {
        Weapon currWeapon = _inventory.currentWeapon.GetComponent<Weapon>();
        currWeapon.Reload();
    }

}
