using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPicker : MonoBehaviour
{
    public string tag = "";
    public float healthValue = 0;

    public void Pick()
    {
        GameObject[] tagObjects  = GameObject.FindGameObjectsWithTag(tag);
        for (int tgo = 0; tgo < tagObjects.Length; tgo++)
        {
            tagObjects[tgo].SendMessage("AddHealth", healthValue);
        }
        Destroy(gameObject);
    }
}
