using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour 
{
    public RotMethod rotMethod;
    public float speed = 4;

    public enum RotMethod 
    {
        circule,
        pingPong,
    }
	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update () 
    {
        switch(rotMethod)
        {
            case RotMethod.circule:
            transform.RotateAround(Vector3.up, speed * Time.deltaTime);	
            break;
            case RotMethod.pingPong:
            transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, Mathf.PingPong(Time.time * speed * 40, 90.0f), transform.localEulerAngles.z);
            break;
        }

	}
}
