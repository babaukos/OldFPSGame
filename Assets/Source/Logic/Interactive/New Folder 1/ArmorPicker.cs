using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmorPicker : MonoBehaviour 
{
    public string tag = "";
    public float armorValue = 0;

    public void Pick()
    {
        GameObject[] tagObjects = GameObject.FindGameObjectsWithTag(tag);


        for (int tgo = 0; tgo < tagObjects.Length; tgo++)
        {
            tagObjects[tgo].SendMessage("AddArmor", armorValue);
        }
        Destroy(gameObject);
    }
}
