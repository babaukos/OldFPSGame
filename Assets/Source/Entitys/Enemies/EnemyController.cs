using UnityEngine;
using System.Collections;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    [Space]
    public string name = "Jon Doe";
    public BrainAI brain;
    [Space]
    public float moveSpeed = 4;
    public float angularSpeed = 50;
    public float acceleration = 2;
    private float mva = 20;
    [Space]
    public bool canMelee;
    [DrawIf("canMelee", true)]
    public float meleeRate = 2;
    [DrawIf("canMelee", true)]
    public float meleeDamage = 1;
    [DrawIf("canMelee", true)]
    public float meleDist = 2;
 
    public bool canShoot;
    [DrawIf("canShoot", true)]
    public float shootDamage;
    [DrawIf("canShoot", true)]
    public float shootRate;
    [DrawIf("canShoot", true)]
    public Transform weaponPos;
    [DrawIf("canShoot", true)]
    public GameObject projectile;

    private float nextFire;
    private float nextHit;
    private float timerFind;
    private NavMeshAgent agent;


    [System.Serializable]
    public class BrainAI 
    {
        public Visor visor;
        public State state;
        public WayContainer way;
        public int targWayPoint = 0;
        public float perseverance = 12;
    }

    public enum State
    {
        Idle = 0,    // покой
        Patrol = 1,  // патрулирование
        Pursuit = 2, // преследование
        Attack = 3,  // атака
        Find = 4,    // поиск
    }
    public enum FireMode 
    {
    
    }
    public enum MoveMode 
    {
    
    }

    private void Start() 
    {
        if (agent == null)
            agent = GetComponent<NavMeshAgent>();
        if (agent != null) 
        {
            agent.speed = moveSpeed;
            agent.angularSpeed = angularSpeed;
            agent.acceleration = acceleration;
        }
    }

    private void Update()
    {
        UnitState();
    }

    // дальняя атака
    private void ShootAttack() 
    {
        if (canShoot == true && Time.time > nextFire)
        {
            nextFire = Time.time + shootRate;
            Instantiate(projectile, weaponPos.position, weaponPos.rotation);
        }
    }
    // ближняя атака
    private void MeleeAttack() 
    {
        RaycastHit rh;
        if (canMelee == true && Time.time > nextHit)
        {
            nextHit = Time.time + meleeRate;
            if (Physics.Raycast(transform.position + new Vector3(0, 1, 0), transform.forward, out rh, meleDist))
            {
                // rh.transform.GetComponent<PlayerHealth>().EnemyHit(meleeDamage);
                rh.transform.SendMessage("Damage", meleeDamage);
            }
        }
    }

    //-------------------------------------------------Отладка---------------------------------------------
#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        Custom.Utility.NGizmos.DrawRay(transform.position, transform.forward * 1.2f, Color.white);
        Custom.Utility.NGizmos.DrawCircle(transform.position, 0.7f, transform.up, Color.white);
        Custom.Utility.NGizmos.DrawPoint(weaponPos.position, 0.09f, Color.red, Custom.Utility.NGizmos.PointTipe.crist);
        Custom.Utility.NGizmos.DrawWireCube(transform.position + new Vector3(0, 1, 0), new Vector3(0.7f, 2, 0.7f), Color.white);
    }
#endif
//-----------------------------------------------------------------------
    public void StateChange(State s) 
    {
        brain.state = s;
    }

    //
    void UnitState()
    {
        switch (brain.state)
        {
            case State.Patrol:
                StatePatrol();
                break;
            case State.Pursuit:
                StatePursuit();
                break;
            case State.Attack:
                StateAttack();
                break;
            case State.Find:
                StateFind();
                break;
        }
    }

    // патрулируем
    void StatePatrol() 
    {
        if (brain.way != null) 
        {
            brain.targWayPoint = brain.way.GetTargetPoint(transform.position, brain.targWayPoint);
            agent.SetDestination(brain.way.waypoints[brain.targWayPoint].position);
        }
        else 
        {
            brain.state = State.Find;
        }
        if (brain.visor.visibleTarget.Count != 0) 
        {
            brain.state = State.Pursuit;
        }
    }

    // преследуем
    void StatePursuit() 
    {
        if (brain.visor.visibleTarget.Count != 0)
        {
            agent.SetDestination(brain.visor.visibleTarget[0].position);
            brain.state = State.Attack;
        }
        else 
        {
            brain.state = State.Find;
        }
    }

    // атакуем
    void StateAttack() 
    {
        if (brain.visor.visibleTarget.Count == 0)
        {
            brain.state = State.Find;
        }
        else
        {
            Vector3 tar = brain.visor.visibleTarget[0].position;
            tar.y = 0;
            transform.LookAt(tar);
            if (Vector3.Distance(transform.position, brain.visor.visibleTarget[0].position) > meleDist)
            {
                ShootAttack();
            }
            else
            {
                MeleeAttack();
            }
        }
    }

    // осматриваемся
    void StateFind() 
    {
        if (brain.visor.visibleTarget.Count == 0)
        {
            timerFind += Time.deltaTime;
            if (timerFind > brain.perseverance)
            {
                timerFind = brain.perseverance;
                if (brain.way != null) 
                {
                    brain.state = State.Patrol;
                }
                else 
                {
                    brain.state = State.Find;
                }
            }
            transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, Mathf.PingPong(Time.time * mva, 180.0f), transform.rotation.eulerAngles.z);
        }
        else 
        {
            brain.state = State.Attack;
        }
    }
}