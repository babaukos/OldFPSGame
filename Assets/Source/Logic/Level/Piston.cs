// ver. 0.1
// 
// Michael Khmelevsky

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Piston : MonoBehaviour
{
#region Public Varible
    [Tooltip("Вкл./Выкл. движение")]
    public bool moveThis;
    [Tooltip("Расстояние от начальной точки до конечной (с учетом поворота обьекта)")]
    public float moveDistance;
    [Tooltip("Скорость от начальной точки до конечной")]
    public float throwingSpeed = 10;
    [Tooltip("Скорость от конечной точки до начальной")]
    public float returnSpeed = 4;
    [Tooltip("Движение: [false]- к концу; [true]- в начало")]
    private bool moveInverse = true;
    public MoveType moveType;
#endregion

#region Private Varible
    private Vector3 endPos;
    private Vector3 startPos;

    private int workingCycle = 1;
    private int currentCycle = 0;
#endregion

#region System Varible
    public enum MoveType
    {
        once,
        pingPong,
    }
#endregion


#region Standard Methods
	// Use this for initialization
	void Start () 
    {
        startPos = transform.localPosition;
	}
	// Update is called once per frame
	void Update () 
    {
        if (!moveInverse)
        {
            endPos = Vector3.MoveTowards(transform.localPosition, startPos, returnSpeed * Time.deltaTime);
        }
        else
        {
            Vector3 target = startPos + (transform.up * moveDistance);
            endPos = Vector3.MoveTowards(transform.localPosition, target, throwingSpeed * Time.deltaTime);
        }
        if (moveThis)
        {
            Moving();
        }
     }
    // Движение
    private void Moving()
    {
        float divider = 10;
        float tolerance = 0.02f;

        if (Vector3.Distance(transform.localPosition, endPos) < tolerance)
        {
            if (moveType == MoveType.once)
            {
                if (moveInverse == false)
                    moveThis = false;
            }
            moveInverse = !moveInverse;
        }

        transform.localPosition = endPos;
    }
#endregion

    //-------------------------------------------------Отладка---------------------------------------------
#if UNITY_EDITOR
    void OnDrawGizmos()
    {
        float scaleGizmoPoint = 0.2f;
        if (Application.isEditor && !Application.isPlaying)
            startPos = transform.position;

        Vector3 dir = transform.up * moveDistance;
        Vector3 endPoint = dir + startPos;
        // moveLine
        Custom.Utility.NGizmos.DrawLine(startPos, endPoint, Color.cyan);
        // startPoint
        Custom.Utility.NGizmos.DrawPoint(startPos, scaleGizmoPoint, Color.white, Custom.Utility.NGizmos.PointTipe.crist);
        // endPoint
        Custom.Utility.NGizmos.DrawPoint(endPoint, scaleGizmoPoint, Color.white, Custom.Utility.NGizmos.PointTipe.crist);
    }
#endif
#region ----------------------------------API-----------------------------------------
    //
    public void Move(bool arg)
    {
        moveThis = arg;
    }
    public void MoveInvese(bool arg) 
    {
        moveInverse = arg;
    }
#endregion
}
