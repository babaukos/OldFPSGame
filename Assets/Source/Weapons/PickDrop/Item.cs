using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SelectionBase]
public class Item : MonoBehaviour 
{
    public int id;
    public GameObject weaponIcon;
    public Weapons weaponPrefab;

    public bool makeActive;
    public bool inInventory;

    public string destination = "WeaponContainer";

    private GameObject container;
    WeaponContainer weaponContainer;

    //
    private void Start() 
    {
        container = GameObject.Find("WeaponContainer");
        weaponContainer = container.GetComponent<WeaponContainer>();
    }
    //
    private void Update() 
    {
        if (weaponIcon != null)
            weaponIcon.gameObject.SetActive(!inInventory);
        if (weaponPrefab != null)
            weaponPrefab.gameObject.SetActive(inInventory);
    }
    private void OnValidate() 
    {
        if (weaponIcon != null)
        weaponIcon.gameObject.SetActive(!inInventory);
        if (weaponPrefab != null)
        weaponPrefab.gameObject.SetActive(inInventory);
    }
    //
    public void AddItem()
    {
        if (weaponContainer != null)
        {
        //for (int i = 0; i < weaponContainer.weapons.Count; i++)
        //{
                //if (weaponPrefab.id == weaponContainer.weapons[i].id) 
                //{
                //    //weaponContainer.weapons[i].SetAmmoAmount(weaponPrefab.GetAmmoAmount());
                //    //weaponContainer.weapons[i].SetAmmoClipSize(weaponPrefab.GetAmmoClipSize());
                //    Destroy(gameObject);
                //}
                //else 
                //{
            PickWeapon(weaponContainer.transform.forward);
                //}
            //}
        }
    }
    //
    private void PickWeapon(Vector3 rot)
    {
        weaponContainer.weapons.Add(this);
        transform.parent = weaponContainer.transform;
        inInventory = true;
        transform.forward = rot;
        if (makeActive)
            weaponContainer.selectedWeapon = weaponContainer.weapons.Count - 1;
    }
    public void DropWeapon(Vector3 pos)
    {
        weaponContainer.weapons.Remove(this);

        transform.parent = null;
        transform.position = pos;
        inInventory = false;
    }
}
