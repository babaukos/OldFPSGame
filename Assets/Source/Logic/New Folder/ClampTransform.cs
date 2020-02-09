// ver. 0.1
// Позволяет ограничивать перемещение обьекта
// Michael Khmelevsky

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class ClampTransform : MonoBehaviour 
{
    [Space]
    [Tooltip("Исползовать ограничение по полю")]
    public ClampArea clampArea;
    public bool areaClamp;


	// Update is called once per frame
	void Update () 
    {
        if (clampArea != null)
        {
            if (areaClamp)
            transform.position = clampArea.GetTruePos(transform.position); 
        }
	}

    //
    public void SetAreae(ClampArea areaTrack)
    {
        clampArea = areaTrack;
    }
}
