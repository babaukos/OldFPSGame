/////////////////////////////////////////////////////////////
//                                                         //
//                                                         //
//                                                         //
/////////////////////////////////////////////////////////////

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("Camera/Controller/Camera Look At")]
public class CameraLookAt : MonoBehaviour
{
    public GetTargetMethod targetObject;
    [DrawIf("targetObject", GetTargetMethod.manual)]
    public Transform targetPoint;
    [DrawIf("targetObject", GetTargetMethod.byName)]
    public string targetPointName = "";
    [DrawIf("targetObject", GetTargetMethod.bayTag)]
    public string targetPointTag = "";

    public float damping = 6.0f;
    public bool smooth = true;

    public enum GetTargetMethod
    {
        manual,
        byName,
        bayTag,
    }

    void Start () 
    {
	    // Make the rigid body not change rotation
   	    if (GetComponent<Rigidbody>())
		    GetComponent<Rigidbody>().freezeRotation = true;
    }

    void LateUpdate () 
    {
        GetTargetObject();

        if (targetPoint) 
        {
		    if (smooth)
		    {
			    // Look at and dampen the rotation
                var rotation = Quaternion.LookRotation(targetPoint.position - transform.position);
			    transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * damping);
		    }
		    else
		        {
			        // Just lookat
                    transform.LookAt(targetPoint);
		        }
	    }
    }

    void GetTargetObject()
    {
        if (targetObject == GetTargetMethod.byName)
        {
            targetPoint = GameObject.Find(targetPointName).transform;
        }
        else
            if (targetObject == GetTargetMethod.bayTag)
            {
                targetPoint = GameObject.FindGameObjectWithTag(targetPointTag).transform;
            }
    }
}
