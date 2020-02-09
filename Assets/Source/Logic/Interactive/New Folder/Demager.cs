using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class Demager : MonoBehaviour 
{
    public float demage = 1;
    public ReactObj reactObj;
    [DrawIf("reactObj", ReactObj.ByTag)]
    public string textTag;

    public enum ReactObj 
    {
        All,
        ByTag,
    }


    void OnTriggerEnter(Collider other) 
    {
        if (reactObj == ReactObj.ByTag)
        {
            if (other.gameObject.CompareTag(textTag))
            other.SendMessage("Damage", demage);        
        }
        else
        {
            other.SendMessage("Damage", demage);
        }
    }

    void OnCollisionEnter(Collision col) 
    {
        if (reactObj == ReactObj.ByTag)
        {
            if (col.gameObject.CompareTag(textTag))
                col.gameObject.SendMessage("Damage", demage);
        }
        else
        {
            col.gameObject.SendMessage("Damage", demage);
        }
    }
}
