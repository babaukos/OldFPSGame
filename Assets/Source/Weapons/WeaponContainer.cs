using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponContainer : MonoBehaviour 
{
    public int selectedWeapon;
    public List<Item> weapons;


    void Start () 
    {
        UpdateWeapon();
	}
	
	void Update () 
    {
        UpdateWeapon();

        if (weapons.Count != 0)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
                selectedWeapon = 0;
            if (Input.GetKeyDown(KeyCode.Alpha2) && weapons.Count > 1)
                selectedWeapon = 1;

            if (Input.GetAxis("Mouse ScrollWheel") > 0)
                selectedWeapon = (selectedWeapon + 1) % weapons.Count;
            if (Input.GetAxis("Mouse ScrollWheel") < 0)
                selectedWeapon = Mathf.Abs(selectedWeapon - 1) % weapons.Count;

            if (Input.GetButtonDown("DropItem"))
                weapons[selectedWeapon].DropWeapon(transform.forward * 0.5f);
        }
    }

    void UpdateWeapon()
    {
        for (int i = 0; i < weapons.Count; i++)
        {
            if (weapons[i] != null)
            {
                if (i == selectedWeapon)
                    weapons[i].gameObject.SetActive(true);
                else
                    weapons[i].gameObject.SetActive(false);
            }
        }
    }
}
