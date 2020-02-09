// ver. 0.1
// Ограничивающее поле
// Michael Khmelevsky

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class ClampArea : MonoBehaviour 
{
    public Vector3 trackZone;

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update () 
    {
		
	}

    //
#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        Gizmos.matrix = Matrix4x4.TRS(transform.position, transform.rotation, transform.lossyScale);
        //Custom.Utility.NGizmos.DrawCube(Vector3.zero, langth, c);
        Custom.Utility.NGizmos.DrawWireCube(Vector3.zero, trackZone,  Color.yellow);
    }
#endif
    //
    public Vector3 GetTruePos(Vector3 qp)
    {
        // zone
        var p = transform.position;
        var x = Mathf.Clamp(qp.x, p.x - trackZone.x/2, p.x + trackZone.x/2);     
        var y = Mathf.Clamp(qp.y, p.y - trackZone.y/2, p.y + trackZone.y/2);
        var z = Mathf.Clamp(qp.z, p.z - trackZone.z/2, p.z + trackZone.z/2);
        //
        return new Vector3(x, y, z);
    }
	public bool IsContainePos(Vector3 qp)
	{
		// zone
		var p = transform.position;
		var xmin = p.x - trackZone.x/2;     
		var xmax = p.x + trackZone.x/2;   

		var ymin = p.y - trackZone.y/2;
		var ymax = p.y + trackZone.y/2;

		var zmin = p.z - trackZone.z/2;
		var zmax = p.z + trackZone.z/2;

		if(qp.x > xmin && qp.x < xmax 
		&& qp.y > ymin && qp.y < ymax
		&& qp.z > zmin && qp.z < zmax )
		{
			return true;
		}
		else
		{
			return false;
		}
	}
}
