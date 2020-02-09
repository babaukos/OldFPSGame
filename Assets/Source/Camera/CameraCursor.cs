using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraCursor : MonoBehaviour
{
     public bool lockCursore;
     public bool showCursor;

	// Use this for initialization
	void Start () 
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        Screen.lockCursor = lockCursore;
        Cursor.visible = showCursor;
        //Cursor.lockState = CursorLockMode.Locked;
	}
}
