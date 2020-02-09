//!*
//
//

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class Visor : MonoBehaviour
{
    public bool active = true;
    [Tooltip("Угол обзора")]
	public int fovAngle = 90;
    [Tooltip("Дальность обзора")]
	public float fovMaxDistance = 15;
    [Tooltip("Маска реагирования обьектов")]
	public LayerMask detectMask;
	public List<RaycastHit> hits = new List<RaycastHit>();
    public List<Transform> visibleTarget = new List<Transform>();
 
//-----------------------------------------------------------------------------------------------------
    //
	void Start() 
	{

	}
    //
    //
    void FixedUpdate()
    {
        visibleTarget.Clear();
    }
    //
    public void LateUpdate()
    {
        FindObjectInSector();
    }
    //
    void OnValidate()
    {
        if (fovAngle < 0) 
            fovAngle = 0;
    }
//------------------------------------- Индексируемый расчет------------------------------------
    //
    //IEnumerator FindTarget(float delay)
    //{
    //    while (true)
    //    {
    //        yield return new WaitForSeconds(delay);
    //        FindObjectInSector();
    //    }
    //}
    void FindObjectInSector() 
    {
        if (active)
        {
            Collider[] targetInView = Physics.OverlapSphere(transform.position, fovMaxDistance, detectMask);
            for (int i = 0; i < targetInView.Length; i++)
            {
                Transform target = targetInView[i].transform;
                var heading = target.position - transform.position;
                var distance = heading.magnitude;
                var direction = heading / distance;

                if (Vector3.Angle(transform.forward, direction) < fovAngle / 2)
                {
                    if (Reycating(transform.position, direction, fovMaxDistance))
                    {
                        if (visibleTarget.Contains(target) == false)
                        visibleTarget.Add(target);
                        Debug.Log("вижу:" + target);
                    }
                }
                if (Vector3.Angle(-transform.forward, direction) < (360 - fovAngle) / 2)
                {
                    if (Reycating(transform.position, direction, fovMaxDistance / 2))
                    {
                        if (visibleTarget.Contains(target) == false)
                        visibleTarget.Add(target);
                        Debug.Log("слишу:" + target);
                    }
                }
            }
        }
    }
    // Рейкаст 
    bool Reycating(Vector3 pos, Vector3 dir, float dist)
    {
        RaycastHit hit, hit2;
        Physics.Raycast(pos, dir, out hit, dist, detectMask);
        Physics.Raycast(pos, dir, out hit2, dist);
        if ((hit.transform != null && hit2.transform != null) && (hit.transform.name == hit2.transform.name))
        {
            return true;
        }
        else
            {
                return false;
            }
    }

    //-------------------------------------------------Отладка---------------------------------------------
#if UNITY_EDITOR 
    void OnDrawGizmos()
    {
        if (active)
        {
            // Подсветка видимых обьектов
            for (int i = 0; i < visibleTarget.Count; i++)
            {
                Custom.Utility.NGizmos.DrawLine(transform.position, visibleTarget[i].position, Color.red);
                Custom.Utility.NGizmos.DrawPoint(visibleTarget[i].position, 0.3f, Color.red, Custom.Utility.NGizmos.PointTipe.all);
            }
            // ВИДИМОСТЬ
            Color c2 = new Color(0, 1, 0, 0.03f);
            Custom.Utility.NGizmos.DrawSolidArc(transform.position, Vector3.up, transform.forward, fovAngle / 2, fovMaxDistance, c2); // ЛЕВІЙ СЕГМЕНТ
            Custom.Utility.NGizmos.DrawSolidArc(transform.position, Vector3.up, transform.forward, -fovAngle / 2, fovMaxDistance, c2); // ПРАВІЙ СЕГМЕНТ
            // СЛИШИМОСТЬ
            Color c = new Color(1, 0, 0, 0.03f);
            Custom.Utility.NGizmos.DrawSolidArc(transform.position, Vector3.up, -transform.forward, -180 + fovAngle / 2, fovMaxDistance / 2, c);// ЛЕВІЙ СЕГМЕНТ
            Custom.Utility.NGizmos.DrawSolidArc(transform.position, Vector3.up, -transform.forward, 180 - fovAngle / 2, fovMaxDistance / 2, c);// ПРАВІЙ СЕГМЕНТ
        }
    }
#endif
}
