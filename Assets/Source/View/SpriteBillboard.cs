//
//
//

using UnityEngine;
using System.Collections;

public class SpriteBillboard : MonoBehaviour
{
    public Axis axis;
 	private Camera camera;

    public Transform billboardingObj;

    public enum Axis
    {
        horizontal,
        vertical,
        horizontalAndHorizontal,
    }
 
	void Awake ()
	{
        if(billboardingObj == null)
        billboardingObj = transform;
	}
 
	void Start()
	{
		if (!camera)
		camera = Camera.main; 
	}
	
	void Update ()
	{
        if (camera != null)
        {
            Vector3 dir = transform.position - camera.transform.position;
            // вращаем обьект относительно камеры
            Vector3 targetOrientation = ReturnVector(axis, dir);
            //transform.LookAt (targetPos, targetOrientation);
            billboardingObj.transform.rotation = Quaternion.LookRotation(targetOrientation);
        }
	}
	// функция возвращает направление, основанное на выбранной оси
	public Vector3 ReturnVector (Axis refAxis, Vector3 target)
	{
		switch (refAxis)
		{
            case Axis.horizontal:
                target.y = 0;
                return target; 
			case Axis.vertical:
                target.x = 0;
                return target; 
            default:
                return target; 
		}
	}
}