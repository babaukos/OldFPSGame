using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ObjectSwich : MonoBehaviour 
{
    public UnityEvent oneStateEvent;
    public UnityEvent twoStateEvent;

    private bool oneState = true;

	// Use this for initialization
	void Start () 
    {
		
	}
	
	// Update is called once per frame
	void Update () 
    {
        if (oneState) 
        {
            oneStateEvent.Invoke();
        }
        else 
        {
            twoStateEvent.Invoke();
        }
		
	}

//-------------------------------------------------Отладка---------------------------------------------	
#if UNITY_EDITOR
    void OnDrawGizmos()
    {
        float pos = 0.5f;
        Custom.Utility.NGizmos.DrawDottedLine(transform.position, transform.position + new Vector3(0, pos - 0.02f, 0), Color.magenta);
        Custom.Utility.NGizmos.DrawString("Swich", transform.position + new Vector3(0, pos, 0), 600, 600, Color.magenta, Color.black);
    }
#endif
    public void Swich() 
    {
        oneState = !oneState;
    }
}
