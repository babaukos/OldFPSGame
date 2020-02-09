using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class Portal : MonoBehaviour 
{
    [Tooltip("Включен ли тригер")]
    public bool activated = true;
    public string activationTag = "Player";
    public Vector3 size = new Vector3(0.5f, 1, 0.5f);

    public Portal outputPortal;

    public UnityEvent EnterExitEvent;

    private BoxCollider trigger;
    private Portal inputPortal;
    private bool blocked;
    private float coolDown = 2f;
    private float timer = 0;

	// Use this for initialization
	void Start () 
    {
        trigger = (BoxCollider)gameObject.AddComponent(typeof(BoxCollider));
        trigger.size = size;
        trigger.isTrigger = true;
        inputPortal = this;
	}
	// Update is called once per frame
	void Update () 
    {
        PortalCoolDown(blocked);
	}
    //---------------------------------------2Д физика------------------------------------
    // Событие при входе в триггер
    void OnTriggerEnter(Collider other)
    {
        if (activated && other.gameObject.tag == activationTag
        && outputPortal != null && blocked == false)
        {
            //other.GetComponent<Rigidbody>().velocity = new Vector2(0, 0);
            outputPortal.blocked = true;
            EnterExitEvent.Invoke();
            WarpJump(other.transform);
        }
    }
    void OnTriggerExit(Collider other)
    {
        //if (activated && other.gameObject.tag == activationTag)
        //{
        //    blocked = true;
        //}
    }
    //
    void PortalCoolDown(bool arg) 
    {
        if (arg)
        {
            if (timer < coolDown)
            {
                blocked = true;
                timer += Time.deltaTime;
            }
            else
            {
                blocked = false;
                timer = 0;
            }
        }
    }
    //--------------------------------------Отладка--------------------------------------	
#if UNITY_EDITOR
    void OnDrawGizmos()
    {
        inputPortal = GetComponent<Portal>();
        Gizmos.matrix = Matrix4x4.TRS(transform.position, transform.rotation, transform.lossyScale);
        if (activated)
        {
            if (outputPortal == null)
            {
                var cA = new Color(0, 0, 1, 0.4f); // синий
                Custom.Utility.NGizmos.DrawCube(Vector3.zero, size, cA);
                Custom.Utility.NGizmos.DrawWireCube(Vector3.zero, size, cA);
            }
            else
            {
                var cB = new Color(242, 0, 1, 0.4f); // красный
                Custom.Utility.NGizmos.DrawCube(Vector3.zero, size, cB);
                Custom.Utility.NGizmos.DrawWireCube(Vector3.zero, size, cB);
            }
        }
        Custom.Utility.NGizmos.DrawRay(Vector3.zero, Vector3.forward, Color.white);
        Custom.Utility.NGizmos.DrawCircle(transform.position, 0.5f, transform.up,Color.white);
    }
    void OnDrawGizmosSelected()
    {
        if (outputPortal != null)
        {
            Vector3 dir = outputPortal.transform.position - transform.position;
            Custom.Utility.NGizmos.DrawArrow(transform.position, dir, Color.white, 0.5f);
        }
    }
#endif
    //
    void WarpJump(Transform to) 
    {
        to.gameObject.SetActive(false);
        to.transform.position = outputPortal.transform.position;
        to.transform.localRotation = outputPortal.transform.localRotation;
        outputPortal.EnterExitEvent.Invoke();
        to.gameObject.SetActive(true);
    }
}
