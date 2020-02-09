/*
 * Ver 0.0.1
 * 2д текстура качестве перекрестия
 * вешать накамеру
 */

using UnityEngine;
using System.Collections;

public class CameraCrosshair1 : MonoBehaviour
{
   public LayerMask raycastLaer;  //
   public Transform Weapon;       // оружие
	
   public Texture2D croshair;     // текстура <span class="posthilit">прицел</span>а размером 15х15
   public float textureSize = 32; // размер текстуры
   public float distance = 100f;  //

   private RaycastHit hitCrosh;
   private Transform MyCamera;    // игровая камера
   private Transform trnsfMuzzle;
   private Vector3 Point;
	
   // Use this for initialization
   void Start () 
   {
      trnsfMuzzle = GameObject.Find("tag_flash").transform;
   }
   //
   void FixedUpdate() 
   {
       MyCamera = Camera.main.transform;
   }
   void OnGUI()
   {
	  if (Physics.Raycast(trnsfMuzzle.transform.position, trnsfMuzzle.transform.forward, out hitCrosh, distance, raycastLaer))
      {
        Point = hitCrosh.point;
      }
	   else
		   {
               Point = trnsfMuzzle.transform.position + trnsfMuzzle.transform.forward * distance;
		   } 

	 if (Vector3.Dot(MyCamera.transform.forward, Point - MyCamera.transform.position) >= 0)
     {
        Vector3 screenPos = MyCamera.GetComponent<Camera>().WorldToScreenPoint(Point);
        if (new Rect(0, 0, Screen.width, Screen.height).Contains(screenPos))
        {
           GUI.DrawTexture(new Rect(screenPos.x-(textureSize/2), Screen.height-screenPos.y-(textureSize/2), textureSize, textureSize), croshair);
        }
     }
   }
   //
   void OnDrawGizmos()
   {
	 Gizmos.DrawRay(trnsfMuzzle.transform.position, trnsfMuzzle.transform.forward * distance);
   }
}