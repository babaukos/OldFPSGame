////////////////////////////////////////////////////
//                   v 0.0.2                      //
//     Вращение камеры по орбите с учетом         //
//             расстояния до таргета              //
///////////////////////////////////////////////////
/////////////////////////////////////////////////////////
//                   v 0.0.2                           //
//          Осмотр камерой по орбите                   //
//                                                     //
/////////////////////////////////////////////////////////

using UnityEngine;
using System.Collections;

[AddComponentMenu("Camera/Controller/Camera Mouse Orbit")]
public class CameraOrbit : MonoBehaviour 
{
	public Transform target;
	public float distance = 10.0f;

    public Vector2 minimum = new Vector2(-360F, -60F);
    public Vector2 maximum = new Vector2(360F, 60F);

    [Space]
    public float sensitivityX = 15F;
    public float sensitivityY = 15F;

    private Quaternion rotation;
	private Vector3 position;

    float x, y, InputX, InputY;
    //------------------------------------
    void Inputs()
    {
        InputX = Input.GetAxis("Mouse X");
        InputY = Input.GetAxis("Mouse Y");
    }

	// Use this for initialization
	private void Start () 
	{
         x = transform.eulerAngles.y;
         y = transform.eulerAngles.x;

	   	 if (GetComponent<Rigidbody>())
			GetComponent<Rigidbody>().freezeRotation = true;
	 }
	
	// Update is called once per frame
    private void LateUpdate() 
	{
	      if (target) 
		  {
             Inputs();
             x += InputX * sensitivityX * 0.02f;
             y -= InputY * sensitivityY * 0.02f;

             x = ClampAngle(x, minimum.x, maximum.x);
             y = ClampAngle(y, minimum.y, maximum.y);
 
             rotation = Quaternion.Euler(y, x, 0.0f);
             position = rotation * new Vector3(0.0f, 0.0f, -distance) + target.position;
		        
		     transform.rotation = rotation;
		     transform.position = position;
         }
	}

	//
    private float ClampAngle(float angle, float min, float max) 
	{
		if (angle < -360)
			angle += 360;
		if (angle > 360)
			angle -= 360;
		return Mathf.Clamp (angle, min, max);
	}
}
