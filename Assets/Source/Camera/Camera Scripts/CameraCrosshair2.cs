/*
 * Ver. 0.0.1
 * 3д объект в качестве перекрестия
 * 
 */
using UnityEngine;
using System.Collections;

public class CameraCrosshair2 : MonoBehaviour 
{

   public Transform crosshair;   //
   public Transform endpoint;    //
   public LayerMask raycastLaer; //
	
   void Update()
   {
      bool foundHit = false;
      RaycastHit hit = new RaycastHit();
      
      foundHit = Physics.Raycast(transform.position,transform.forward,out hit,20,raycastLaer);
      //Debug.DrawLine(transform.position,hit.point);
      
      if(foundHit)
      {
         crosshair.position = hit.point;
         //crosshair.position = Quaternion.LookRotation(hit.normal);
      }
      else
      {
         crosshair.position = endpoint.position;
      }
   }
}

