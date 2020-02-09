using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

// Require a character controller to be attached to the same game object
[AddComponentMenu("PersonController/ SecondPersonController")]
[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(NavMeshAgent))]
public class SecondPersonController : MonoBehaviour
{
    public LayerMask walkingMask;
    public bool isControllable = true;
    public float walkSpeed = 0.5f;
    public float runSpeed = 0.4f;
    public float turnSpeed = 0.4f;

    public List<Vector3> wayPoints;

    private float qurentSpeed;
    private bool startTimerClick;
    private float timerKlick = 0.18f;
    private float tk;

    NavMeshAgent agent;
    CharacterController controller;
	// Use this for initialization
	void Start () 
    {
        agent = GetComponent<NavMeshAgent>();
        controller = GetComponent<CharacterController>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        GetTarget();
        // Calculate actual motion
        CharacterController controller = GetComponent<CharacterController>();
        Debug.DrawRay(transform.position, transform.forward * 1, Color.green);
        if (wayPoints.Count > 0)
        {
            var dir = wayPoints[0] - transform.position;
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(dir), turnSpeed * Time.deltaTime);
            controller.Move(transform.forward * qurentSpeed * Time.deltaTime);
            //agent.destination = wayPoints[0];

            if (Vector3.Distance(transform.position, wayPoints[0]) < 1)
            {
                wayPoints.RemoveAt(0);
            }
            Debug.DrawLine(transform.position, wayPoints[0], Color.red);
            if (wayPoints.Count > 1)
            {
                for(int wp = 0; wp < wayPoints.Count; wp++)
                {
                    Debug.DrawLine(wayPoints[wp], wayPoints[wp+1], Color.blue);
                }
            }
        }
        else 
           {
               qurentSpeed = 0;
           }
        if (startTimerClick)
        {
            tk += Time.deltaTime;
            if (tk > timerKlick)
            {
                tk = 0;
                startTimerClick = false;
                qurentSpeed = walkSpeed;
            }
        }
	}

    void GetTarget()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if ((Input.GetMouseButtonDown(0)))
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                // задать маршрут следования
                if (Physics.Raycast(ray, out hit, walkingMask))
                {
                    wayPoints.Add(hit.point);
                }
            }
            else
                {   // задать точку следования
                    if (Physics.Raycast(ray, out hit, walkingMask))
                    {
                        wayPoints.Clear();
                        wayPoints.Add(hit.point);
                    }
                }
            if (!startTimerClick)             // если счетчик отключен, включаем его
            {
                tk = 0;
                startTimerClick = true;
            }
            else                             // если счетчик включен, то
            {
                startTimerClick = false;
                if (tk < timerKlick)
                {
                    tk = 0;
                    // двойной клик
                    qurentSpeed = runSpeed;
                }
            }
        }
    }
}
