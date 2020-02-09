/////////////////////////////////////////////////////////////
//                                                         //
//                                                         //
//                                                         //
/////////////////////////////////////////////////////////////

using UnityEngine;
using System.Collections;
 
[AddComponentMenu("Camera/Controller/Camera Mouse Orbit and Zoom")]
public class CameraOrbitZoom : MonoBehaviour
{
    public GetTargetMethod targetObject;
    [DrawIf("targetObject", GetTargetMethod.manual)]
    public Transform targetPoint;
    [DrawIf("targetObject", GetTargetMethod.byName)]
    public string targetPointName = "";
    [DrawIf("targetObject", GetTargetMethod.bayTag)]
    public string targetPointTag = "";

    public float distance = 5.0f;
    public float xSpeed = 120.0f;
    public float ySpeed = 120.0f;
 
    public float yMinLimit = -20f;
    public float yMaxLimit = 80f;
 
    public float distanceMin = .5f;
    public float distanceMax = 15f;
  
    float x = 0.0f;
    float y = 0.0f;

    public enum GetTargetMethod
    {
        manual,
        byName,
        bayTag,
    }

    // Use this for initialization
    void Start () 
    {
        x = transform.eulerAngles.y;
        y = transform.eulerAngles.x;
 
        if (GetComponent<Rigidbody>())
            GetComponent<Rigidbody>().freezeRotation = true;
    }
 
    void LateUpdate () 
    {
        GetTargetObject();

        if (targetPoint) 
        {
            x += Input.GetAxis("Mouse X") * xSpeed * distance * 0.02f;
            y -= Input.GetAxis("Mouse Y") * ySpeed * 0.02f;
 
            y = ClampAngle(y, yMinLimit, yMaxLimit);
 
            Quaternion rotation = Quaternion.Euler(y, x, 0);
 
            distance = Mathf.Clamp(distance - Input.GetAxis("Mouse ScrollWheel")*5, distanceMin, distanceMax);
 
            RaycastHit hit;
            if (Physics.Linecast(targetPoint.position, transform.position, out hit)) 
            {
                distance -=  hit.distance;
            }
            Vector3 negDistance = new Vector3(0.0f, 0.0f, -distance);
            Vector3 position = rotation * negDistance + targetPoint.position;
 
            transform.rotation = rotation;
            transform.position = position;
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
 
    public static float ClampAngle(float angle, float min, float max)
    {
        if (angle < -360F)
            angle += 360F;
        if (angle > 360F)
            angle -= 360F;
        return Mathf.Clamp(angle, min, max);
    }
}