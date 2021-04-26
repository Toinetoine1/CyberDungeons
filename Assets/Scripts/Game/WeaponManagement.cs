using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManagement : MonoBehaviour
{
    public Inventory _inventory;
    

    void Update()
    {
        /*Vector3 dir = cam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x,
            Input.mousePosition.y, -cam.transform.position.z));
        dir.y = 0;
        transform.LookAt(dir);*/
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

    private Vector3 setRotation()
    {
        throw new NotImplementedException();
    }
}
