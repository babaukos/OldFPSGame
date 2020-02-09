/////////////////////////////////////////////////////////
//                   v 0.0.2                           //
//    Осмотр камерой не привязанной жестко камеры      //
//                                                     //
/////////////////////////////////////////////////////////

using UnityEngine;
using System.Collections;

[AddComponentMenu("Camera/Controller/Camera Mouse Look and Fixed")]
public class CameraLookFollow : MonoBehaviour 
{
    public GetTargetMethod targetObject;
    [DrawIf("targetObject", GetTargetMethod.manual)]
    public Transform targetPoint;
    [DrawIf("targetObject", GetTargetMethod.byName)]
    public string targetPointName = "";
    [DrawIf("targetObject", GetTargetMethod.bayTag)]
    public string targetPointTag = "";

    public enum RotationAxes { MouseXAndY = 0, MouseX = 1, MouseY = 2 };
    public RotationAxes axes = RotationAxes.MouseXAndY;

    public Vector2 minimum = new Vector2(-360F, -60F);
    public Vector2 maximum = new Vector2(360F, 60F);

    [Space]
    public float sensitivityX = 15F;
    public float sensitivityY = 15F;

    float rotationX = 0F;
    float rotationY = 0F;

    Quaternion originalRotation;

    public enum GetTargetMethod 
    {
        manual,
        byName,
        bayTag,
    }

    void Start()
    {
        //originalRotation = transform.localRotation;

        if (GetComponent<Rigidbody>())
            GetComponent<Rigidbody>().freezeRotation = true;
    }

    void LateUpdate()
    {
        if (targetPoint != null)
        {
            GetCamRotation();
        }
        else
        {
            GetTargetObject();
        }
    }
    void GetCamRotation() 
    {
        originalRotation = targetPoint.localRotation;
        transform.position = targetPoint.position;
        if (axes == RotationAxes.MouseXAndY)
        {
            // Read the mouse input axis
            rotationX += Input.GetAxis("Mouse X") * sensitivityX;
            rotationY += Input.GetAxis("Mouse Y") * sensitivityY;

            rotationX = ClampAngle(rotationX, minimum.x, maximum.x);
            rotationY = ClampAngle(rotationY, minimum.y, maximum.y);

            Quaternion xQuaternion = Quaternion.AngleAxis(targetPoint.eulerAngles.y + rotationX, Vector3.up);
            Quaternion yQuaternion = Quaternion.AngleAxis(rotationY, -Vector3.right);

            transform.localRotation = originalRotation * xQuaternion * yQuaternion;
        }
        else if (axes == RotationAxes.MouseX)
        {
            rotationX += Input.GetAxis("Mouse X") * sensitivityX;
            rotationX = ClampAngle(rotationX, minimum.x, maximum.x);

            Quaternion xQuaternion = Quaternion.AngleAxis(targetPoint.eulerAngles.y + rotationX, Vector3.up);
            transform.localRotation = originalRotation * xQuaternion;
        }
        else
        {
            rotationY += Input.GetAxis("Mouse Y") * sensitivityY;
            rotationY = ClampAngle(rotationY, minimum.y, maximum.y);

            Quaternion xQuaternion = Quaternion.Euler(0, targetPoint.eulerAngles.y, 0);
            Quaternion yQuaternion = Quaternion.AngleAxis(-rotationY, Vector3.right);
            transform.localRotation = originalRotation * xQuaternion * yQuaternion;
        }
        transform.position = targetPoint.position;
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
    float ClampAngle(float angle, float min, float max)
    {
        if (angle < -360F)
            angle += 360F;
        if (angle > 360F)
            angle -= 360F;
        return Mathf.Clamp(angle, min, max);
    }
}