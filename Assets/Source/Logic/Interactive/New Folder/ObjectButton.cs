using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ObjectButton : MonoBehaviour 
{
    public UnityEvent pressedEvent;

    private bool pressed;

	// Use this for initialization
	void Start () 
    {
		
	}
	
	// Update is called once per frame
	void Update () 
    {
        if (pressed)
        {
            pressedEvent.Invoke();
            pressed = false;
        }
	}
    //-------------------------------------------------Отладка---------------------------------------------	
#if UNITY_EDITOR
    void OnDrawGizmos()
    {
        float pos = 0.5f;
        Custom.Utility.NGizmos.DrawDottedLine(transform.position, transform.position + new Vector3(0, pos - 0.02f, 0), Color.white);
        Custom.Utility.NGizmos.DrawString("Button", transform.position + new Vector3(0, pos, 0), 600, 600, Color.red, Color.black);
    }
#endif

    public void Pressed() 
    {
        pressed = true;
    }
}
