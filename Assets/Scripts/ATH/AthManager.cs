using System.Collections;
using System.Collections.Generic;
using Game;
using UnityEngine;
using UnityEngine.UI;

public class AthManager : MonoBehaviour
{
    public GameObject _inventory;
    public Text NbrOfBullet;
    public Text WeaponName;
    public Image ImageWeapon;

    // Start is called before the first frame update
    void Start()
    {
        _inventory = FindObjectOfType<Inventory>().currentWeapon;
        Debug.Log(_inventory.name);
        WeaponName.text = _inventory.name;
        NbrOfBullet.text = _inventory.GetComponent<Weapon>().currAmmo + " / " +
                           _inventory.GetComponent<Weapon>().maxAmmo;
        ImageWeapon.sprite = _inventory.GetComponent<SpriteRenderer>().sprite;
    }
    // Update is called once per frame
    void Update()
    {
        _inventory = FindObjectOfType<Inventory>().currentWeapon;
        if (_inventory != null)
        {
            SetWeaponName();
            SetNbrOfBullet();
            SetImageWeapon();
        }
        
        
    }

    public void SetWeaponName()
    {
        if (_inventory != null)
        {
            WeaponName.text = _inventory.name;
        }
        else
        {
            Debug.Log("Inventory est vide ??");
        }
    }

    public void SetNbrOfBullet()
    {
        if (_inventory != null)
        {
            NbrOfBullet.text = _inventory.GetComponent<Weapon>().currAmmo + " / " +
                               _inventory.GetComponent<Weapon>().maxAmmo;
        }
        else
        {
            Debug.Log("Inventory est vide ??");
        }
        
    }

    public void SetImageWeapon()
    {
        if (_inventory != null)
        {
            ImageWeapon.sprite = _inventory.GetComponent<SpriteRenderer>().sprite;
        }
        else
        {
            Debug.Log("Inventory est vide ??");
        }
    }
}
