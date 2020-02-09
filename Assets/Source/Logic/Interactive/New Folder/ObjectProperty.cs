//
//
//

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectProperty : MonoBehaviour 
{
    public float materialСoeff = 1;
    public GameObject hitDecal;

	// Use this for initialization
	void Start () 
    {
		
	}
	//
    public void Hit(int damage, RaycastHit hit)
    {
        if (hitDecal != null)
        Instantiate(hitDecal, hit.point, Quaternion.FromToRotation(Vector3.forward, hit.normal)).transform.parent = hit.transform;
    }
}
