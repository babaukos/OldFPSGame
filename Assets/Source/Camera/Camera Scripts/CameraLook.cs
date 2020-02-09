/////////////////////////////////////////////////////////
//                   v 0.0.2                           //
//          Осмотр камерой и поворот                   //
//                                                     //
/////////////////////////////////////////////////////////

using UnityEngine;
using System.Collections;

[AddComponentMenu("Camera/Controller/Camera Mouse Look")]
public class CameraLook : MonoBehaviour 
{
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

    void Start()
    {
        originalRotation = transform.localRotation;

        if (GetComponent<Rigidbody>())
            GetComponent<Rigidbody>().freezeRotation = true;
    }

    void LateUpdate()
    {
        if (axes == RotationAxes.MouseXAndY)
        {
            // Read the mouse input axis
            rotationX += Input.GetAxis("Mouse X") * sensitivityX;
            rotationY += Input.GetAxis("Mouse Y") * sensitivityY;

            rotationX = ClampAngle(rotationX, minimum.x, maximum.x);
            rotationY = ClampAngle(rotationY, minimum.y, maximum.y);

            Quaternion xQuaternion = Quaternion.AngleAxis(rotationX, Vector3.up);
            Quaternion yQuaternion = Quaternion.AngleAxis(rotationY, -Vector3.right);

            transform.localRotation = originalRotation * xQuaternion * yQuaternion;
        }
        else if (axes == RotationAxes.MouseX)
        {
            rotationX += Input.GetAxis("Mouse X") * sensitivityX;
            rotationX = ClampAngle(rotationX, minimum.x, maximum.x);

            Quaternion xQuaternion = Quaternion.AngleAxis(rotationX, Vector3.up);
            transform.localRotation = originalRotation * xQuaternion;
        }
        else
        {
            rotationY += Input.GetAxis("Mouse Y") * sensitivityY;
            rotationY = ClampAngle(rotationY, minimum.y, maximum.y);

            Quaternion yQuaternion = Quaternion.AngleAxis(-rotationY, Vector3.right);
            transform.localRotation = originalRotation * yQuaternion;
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