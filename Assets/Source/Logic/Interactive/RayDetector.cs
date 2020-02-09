//
//
// Michael Khmelevsky

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[SelectionBase]
public class RayDetector : MonoBehaviour
{
#region Public Verible
    [Tooltip("вкл./выкл. интерактивность")]
    public bool active;
    public Vector3 langth = Vector3.one;
    [Space]
    [SerializeField]
    private UnityEvent selction;
    [SerializeField]
    private UnityEvent deselection;
    [SerializeField]
    private UnityEvent ection;

#endregion

#region Private Verible
    private bool status;
    private BoxCollider collider;
#endregion

#region System Verible
    public enum Mode
    {
        button,
        swich,
    }
#endregion


    // Use this for initialization
    private void Start()
    {
        SetCollider();
	}
	
	// Update is called once per frame
    private void Update() 
    {
        //switch (mode)
        //{
        //    case Mode.button:
        //        break;
        //    case Mode.swich:
        //        break;
        //}
	}

    //
    private void Using()
    {
        if (active)
            ection.Invoke();
    }
    private void Selection()
    {
        //if (active)
        selction.Invoke();
    }
    private void Deselection()
    {
        //if (active)
        deselection.Invoke();
    }

    //
    private void OnValidate()
    {
        // SetCollider();
    }

    //
    private void SetCollider()
    {
        collider = GetComponent<BoxCollider>();
        if (collider != null)
        {
            collider.isTrigger = true;
            collider.size = langth;
        }
        else
        {
            collider = gameObject.AddComponent<BoxCollider>();
            collider.isTrigger = true;
            collider.size = langth;
        }
    }
    //---------------------------------------Отладка--------------------------------------
#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        Color c1 = new Color (1, 0, 1, 0.2f); // magenta
        Color c2 = new Color(1, 0, 1, 0.7f);  // magenta 
        Gizmos.matrix = Matrix4x4.TRS(transform.position, transform.rotation, transform.lossyScale);
        Custom.Utility.NGizmos.DrawCube(Vector3.zero, langth, c1);
        Custom.Utility.NGizmos.DrawWireCube(Vector3.zero, langth, c2);
    }
#endif
    //--------------------------------------------API-------------------------------------------
    //
    public void SetActive(bool arg) 
    {
        active = arg;
    }
    public void DebugLog(string str)
    {
        Debug.Log(str);
    }
}
