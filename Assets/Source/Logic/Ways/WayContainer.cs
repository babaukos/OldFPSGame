// ver. 0.1
//
// Michael Khmelevsky

using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class WayContainer : MonoBehaviour 
{
    [Tooltip("Характер маршрута")]
    public TypeWay typeWay;
    [Tooltip("Маршрутные точки")]
    public List <Transform> waypoints = new List<Transform>();
    private bool forward = true;
    public enum TypeWay
    {
        wance,
        pingPong,
        circule,
    }
//-----------------------------------------------------------------------------------
    //
    void OnValidate() 
    {
        gameObject.name = "WayContainer";
    }
#if UNITY_EDITOR
    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        for (int p = 0; p < waypoints.Count; p++)
        {
            if (p > 0)
            {
                Vector3 dir = (waypoints[p].position - waypoints[p - 1].position);
                Custom.Utility.NGizmos.DrawArrow(waypoints[p - 1].position, dir, 0.25f); 
            }
         }
        if (typeWay == TypeWay.circule && waypoints.Count > 1)
        {
            Vector3 dir = (waypoints[0].position - waypoints[waypoints.Count - 1].position);
            Custom.Utility.NGizmos.DrawArrow(waypoints[waypoints.Count - 1].position, dir, 0.25f); 
        }
    }
    void OnDrawGizmosSelected()
    {

    }
#endif
//----------------------------------------API----------------------------------------
    public int GetTargetPoint(Vector3 position, int point)
    {
        float tolerance = 0.2f;
        float dist = Vector3.Distance(position, waypoints[point].position);
        switch (typeWay)
        {
            case TypeWay.wance:
                if (point < waypoints.Count - 1)
                {
                    if (dist <= tolerance)
                        point++;
                }
                return point;
            break;
            case TypeWay.circule:
                if (point < waypoints.Count - 1)
                {
                    if (dist <= tolerance)
                        point++;
                }
                else 
                {
                    if (dist <= tolerance)
                    point = 0;
                }
                return point;
            break;
            case TypeWay.pingPong:
                if (forward == true )
                {     
                    if (point < waypoints.Count - 1)
                    {
                        if (dist <= tolerance)
                            point++;
                    }
                    else 
                    {
                        forward = false;
                    }
                }
                else 
                {
                    if (point > 0)
                    {
                        if (dist <= tolerance)
                            point--;
                    }
                    else 
                    {
                        forward = true;
                    }
                }
                return point;
            break;
        }
        return point;
    }
//----------------------------------------API----------------------------------------
#if UNITY_EDITOR
    public void AddPoint()
    {
        WayPoint wpc;
        Transform wpt;
        wpt = new GameObject("point_" + waypoints.Count).transform;
        wpc = wpt.gameObject.AddComponent<WayPoint>();
        wpc.number = waypoints.Count + 1;
        wpt.transform.position = transform.position;
        wpt.transform.parent = transform;
        waypoints.Add(wpt);
    }
    public void RemovePoint()
    {
        if (waypoints.Count != 0)
        {
            UnityEditor.EditorApplication.delayCall += () =>
            {
                var tempArray = new GameObject[transform.childCount];
                DestroyImmediate(transform.GetChild(tempArray.Length - 1).gameObject);
            };
            waypoints.RemoveAt(waypoints.Count - 1);
        }
    }
#endif
}
