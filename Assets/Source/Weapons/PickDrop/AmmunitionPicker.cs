using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmunitionPicker : MonoBehaviour 
{
    public int id;
    public int ammoClips;

    private GameObject container;
    WeaponContainer weaponContainer;

    public void AddAmmunition() 
    {
        container = GameObject.Find("WeaponContainer");
        weaponContainer = container.GetComponent<WeaponContainer>();

        //if (weaponContainer != null)
        //{
        //    for (int wp = 0; wp < weaponContainer.weapons.Count; wp++)
        //    {
        //        if (id == weaponContainer.weapons[wp].id)
        //            weaponContainer.weapons[wp].ChangeValue(ammoClips);
        //        return;
        //    }
        //}
    }
}
