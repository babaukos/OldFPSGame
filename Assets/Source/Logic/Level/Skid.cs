// ver. 0.1
// Перемещает обьект горизонтально или вертикально, с заданой скоростью
// Michael Khmelevsky

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skid : MonoBehaviour
{
#region Public Varible
    [Tooltip("Вкл./Выкл. движение")]
    [SerializeField]
    private bool moveThis;
    [SerializeField]
    private float moveSpeed;
    [Tooltip("Движение: [true]- вправо(вниз); [false]- влево(вверх)")]
    [SerializeField]
    private bool moveInverse;
    [Tooltip("Ограничители (по модулю)")]
    public Limit moveLimit;
    [SerializeField]
    private MoveType moveType;
    public bool inheritObject;
#endregion

#region Private Varible
    private Vector3 startPos;
    private Vector3 endPos;
#endregion

#region System Varible
    public enum MoveType 
    {
        pingPong,
        once,
    }
    public enum MovingDir
    {
      horizontal,
      vertical,
    }
    [System.Serializable]
    public class Limit 
    {
        public Vector3 max;
        public Vector3 min;
    }
#endregion


#region Standard Methods
    // Выполняется при инициализации
    private void Start() 
    {
        // запоминаем первое положение
        startPos = transform.localPosition;
	}
	// Выполняется каждый кадр
	private void Update ()
    {
        if (moveInverse)
        {
            endPos = startPos + moveLimit.min;
        }
        else
        {
            endPos = startPos + moveLimit.max;
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
                moveThis = false;
                moveInverse = !moveInverse;
            }
            else if (moveType == MoveType.pingPong)
            {
                moveInverse = !moveInverse;
            }
        }

        transform.localPosition = Vector3.MoveTowards(transform.localPosition, endPos, moveSpeed * Time.deltaTime);
    }
    //
    void OnCollisionEnter2D(Collision2D col)
    {
        if (inheritObject)
        {
            col.transform.parent = transform;
        }
    }
    //
    void OnCollisionExit2D(Collision2D col)
    {
        if (inheritObject)
        {
            col.transform.parent = null;
        }
    }
#endregion

    //-------------------------------------------------Отладка---------------------------------------------	
#if UNITY_EDITOR
    void OnDrawGizmos()
    {
        float scaleGizmoPoint = 0.2f;
        if (Application.isEditor && !Application.isPlaying)
            startPos = transform.position;

        Vector3 MinLim = startPos + moveLimit.min;
        Vector3 MaxLim = startPos + moveLimit.max;

        Custom.Utility.NGizmos.DrawLine(startPos, MinLim, Color.cyan);
        Custom.Utility.NGizmos.DrawLine(startPos, MaxLim, Color.cyan);

        Custom.Utility.NGizmos.DrawPoint(MinLim, scaleGizmoPoint, Color.white, Custom.Utility.NGizmos.PointTipe.crist);
        Custom.Utility.NGizmos.DrawPoint(MaxLim, scaleGizmoPoint, Color.white, Custom.Utility.NGizmos.PointTipe.crist);
    }
#endif
//--------------------------------------API----------------------------------------------------	
    //
    public void Move(bool arg) 
    {
        moveThis = arg;
    }
    public void MoveInvese(bool arg)
    {
        moveInverse = arg;
    }
}
