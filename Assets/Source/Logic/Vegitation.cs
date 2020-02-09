using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SelectionBase]
public class Vegitation : MonoBehaviour 
{
    public Growth growth;

    [DrawIf("growth", Growth.Dinamic)]
    public float growRate;
    [DrawIf("growth", Growth.Dinamic)]
    public float maxSize;
    [DrawIf("growth", Growth.Dinamic)]
    public float size;

    public enum Growth 
    {
        Static,
        Dinamic,
    }

	// Use this for initialization
    private void Start() 
    {

	}
    //
    private void Update() 
    {
        if (growth == Growth.Dinamic)
        Grow();
    }
    //
    private void Grow() 
    {
        if (size < maxSize)
        {
            transform.localScale = Vector3.one * size;
            size += growRate * Time.deltaTime;
        }
    }

    //-------------------------------------------------Отладка---------------------------------------------
#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        Custom.Utility.NGizmos.DrawRay(transform.position, transform.forward * 1.2f, Color.cyan);
        Custom.Utility.NGizmos.DrawCircle(transform.position, 0.7f, transform.up, Color.cyan);
        Custom.Utility.NGizmos.DrawSphere(transform.position, 0.1f, Color.green);
    }
#endif
}
