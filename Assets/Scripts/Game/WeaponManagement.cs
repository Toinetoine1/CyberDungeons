using System;
using System.Collections;
using System.Collections.Generic;
using Game;
using UnityEngine;

public class WeaponManagement : MonoBehaviour
{
    public Inventory _inventory;
    public Camera CurrentCamera;

    void Update()
    {
        /*Camera cam = Camera.main;
        Vector3 dir = cam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x,
            Input.mousePosition.y, -cam.transform.position.z));
        Debug.Log(dir);*/      
    }

    public void fire()
    {
        Weapon currWeapon = _inventory.currentWeapon.GetComponent<Weapon>();
        currWeapon.fire(CurrentCamera, transform);
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
