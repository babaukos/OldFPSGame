using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SelectionBase]
public class WayPoint : MonoBehaviour
{
    public int number;
    [Space]
    public float radiuse;


	// Update is called once per frame
	void Update () 
    {
		
	}

    //-------------------------------------------------Отладка---------------------------------------------	
#if UNITY_EDITOR
    void OnDrawGizmos() 
    {
        Custom.Utility.NGizmos.DrawRay(transform.position, Vector3.up * 0.5f, Color.red);
        Custom.Utility.NGizmos.DrawSphere(transform.position, 0.09f, Color.red);
        Custom.Utility.NGizmos.DrawCircle(transform.position, radiuse, Vector3.up);
        Custom.Utility.NGizmos.DrawString(number.ToString(), transform.position, Color.cyan, Color.black);
    }
    void OnDrawGizmosSelected()
    {
        Custom.Utility.NGizmos.DrawCircle(transform.position, 0.11f, Vector3.up,Color.white);
    }
#endif
}
