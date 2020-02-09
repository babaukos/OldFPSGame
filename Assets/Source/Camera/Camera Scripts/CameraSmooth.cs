/////////////////////////////////////////////////////////////
//                                                         //
//                                                         //
//                                                         //
/////////////////////////////////////////////////////////////

using UnityEngine;
using System.Collections;
using System.Threading;

[AddComponentMenu("Camera/Controller/CameraSmooth")]
public class CameraSmooth : MonoBehaviour
{  
     public Transform target;
     public float smothPos = 50f;
     public float smothRot = 5f;

     void FixedUpdate()
     {
         if (target)
         {
             transform.position = Vector3.Lerp(transform.position, target.position, smothPos * Time.deltaTime);
             transform.rotation = Quaternion.Slerp(transform.rotation, target.rotation, smothRot * Time.deltaTime);
         }
     }
}