using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartPoint : MonoBehaviour 
{
	public bool thisPoint;
	// 
	void OnValidate () 
    {
        gameObject.tag = "StartSpawnPoint";
	}
    //-------------------------------------------------Отладка---------------------------------------------
#if UNITY_EDITOR
    private void  OnDrawGizmos()
    {
        var c1 = new Color(1, 0, 0, 1f); // red
        Custom.Utility.NGizmos.DrawCube(transform.position, new Vector3(0.6f, 2.0f, 0.6f), c1);
        Custom.Utility.NGizmos.DrawRay(transform.position, transform.forward * 1.2f, Color.red);
        Custom.Utility.NGizmos.DrawCircle(transform.position, 0.7f, transform.up, Color.red);
    }
#endif
}
