using System.Collections;
using System.Collections.Generic;
using Game;
using UnityEngine;
using UnityEngine.UI;

public class AthManager : MonoBehaviour
{
    private GameObject _inventory;
    public Text NbrOfBullet;
    public Text WeaponName;
    public Image ImageWeapon;

    // Start is called before the first frame update
    void Start()
    {
        _inventory = FindObjectOfType<Inventory>().currentWeapon;
        WeaponName.text = _inventory.name;
        NbrOfBullet.text = _inventory.GetComponent<Weapon>().currAmmo + " / " +
                           _inventory.GetComponent<Weapon>().maxAmmo;
        ImageWeapon.sprite = _inventory.GetComponent<SpriteRenderer>().sprite;
    }
    // Update is called once per frame
    void Update()
    {
        GameObject newWeapon = FindObjectOfType<Inventory>().currentWeapon;
        if (newWeapon != _inventory)
        {
            _inventory = newWeapon;
            SetImageWeapon();
            SetWeaponName();
            SetNbrOfBullet();
        }
        SetNbrOfBullet();
    }

    public void SetWeaponName()
    {
        WeaponName.text = _inventory.name;
    }

    public void SetNbrOfBullet()
    {
        NbrOfBullet.text = _inventory.GetComponent<Weapon>().currAmmo + " / " +
                           _inventory.GetComponent<Weapon>().maxAmmo;
    }

    public void SetImageWeapon()
    {
        ImageWeapon.sprite = _inventory.GetComponent<SpriteRenderer>().sprite;
    }
}
