#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Custom.Utility;

public class Ruler : MonoBehaviour 
{
    public Metric rulerMetric;

    public int rulerLength = 5;
    public Color text = Color.white;
    public Color ruler = Color.green;
    public Color dividersA = Color.blue;
    public Color dividersB = Color.cyan;
    public Color dividersC = Color.yellow;


    public List<Marker> markers = new List<Marker>();

    public enum Metric 
    {
        meters = 1,
        centimeters = 10,
        milimeters = 100,
    }
    [System.Serializable]
    public class Marker 
    {
        public float position;
        public float length = 1;    
    }


    //
    void OnValidate() 
    {
        gameObject.name = "RulerTool";
        for (int m = 0; m < markers.Count; m++)
        {
            if (markers[m].position > rulerLength)
            {
                markers[m].position = rulerLength;
            }
            if (markers[m].position < 0)
            {
                markers[m].position = 0;
            }
        }
    }
    //
    void OnDrawGizmos() 
    {
        NGizmos.DrawLine(transform.position, transform.position + transform.up * rulerLength, ruler);
        NGizmos.DrawSphere(transform.position, 0.05f, Color.red);
        DivisionA();
        DivisionB();
        DivisionC();
        DivisionMarker();
    }
    //mm
    void DivisionA()
    {
        float ln = 0.03f;
        float step = 0.1f;
        for (int l = 0; l <= rulerLength * 10; l++)
        {
            Vector3 pos = transform.position + transform.up * l * step;
            NGizmos.DrawRay(pos, transform.right * ln, dividersA);
            NGizmos.DrawRay(pos, transform.right * -ln, dividersA);
        }
    }
    //div
    void DivisionB()
    {
        float ln = 0.07f;
        float step = 0.5f;
        for (int l = 0; l <= rulerLength * 2; l++)
        {
            Vector3 pos = transform.position + transform.up * l * step;
            NGizmos.DrawRay(pos, transform.right * ln, dividersB);
            NGizmos.DrawRay(pos, transform.right * -ln, dividersB);
        }
    }
    //sm
    void DivisionC()
    {
        float ln = 0.14f;
        float step = 1f;
        for (int l = 0; l <= rulerLength; l++)
        {
            Vector3 pos = transform.position + transform.up * l * step;
            if (l == 0)
            {
                NGizmos.DrawString(transform.eulerAngles.z.ToString("0.0") + " °", pos - transform.right * 0.1f, text);
            }
            NGizmos.DrawRay(pos, transform.right * ln, dividersC);
            NGizmos.DrawRay(pos, transform.right * -ln, dividersC);
            NGizmos.DrawString(l.ToString() + "m", pos + transform.right * 0.1f, text);
        }
    }
    //
    void DivisionMarker() 
    {
        for (int m = 0; m < markers.Count; m++)
        {
            Vector3 pos = transform.position + transform.up * markers[m].position;
            NGizmos.DrawRay(pos, transform.right * markers[m].length, Color.red);
            NGizmos.DrawRay(pos, transform.right * -0.05f, Color.red);
        }
    }
}
#endif