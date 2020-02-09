// ver. 0.1
//
// Michael Khmelevsky

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [Tooltip("Включить спавн")]
    public bool activated = true;
    [Tooltip("Радиус спавна, если 0 то спавн в точке")]
    [SerializeField]
    private float radiuse;
    [Tooltip("Спавнящийся обьект")]
    [SerializeField]
    private GameObject spawnObject;
    [SerializeField]
    [Tooltip("Сколько оьектов спавнить всего")]
    private int numberObjects = 1;
    [SerializeField]
    [Tooltip("Время спавна")]
    private float timer = 0;
    public Option forceOptions;

    private float timerCount;
    private int countObject;
    private List<GameObject> spawnObjectPool = new List<GameObject>();

    [System.Serializable]
    public class Option 
    {
        public bool useForce;
        public float force = 10f;
        public ForceMode forceMode;
    }
    //[System.Serializable]
    //public class Option
    //{
    //    public bool useForce;
    //    public float force = 10f;
    //    public ForceMode2D forceMode;
    //}

//------------------------------------------------------------------------------------
	// Use this for initialization
	private void Start () 
    {
		
	}
	// Update is called once per frame
    private void Update()
    {
        if (activated)
        {
            if (countObject < numberObjects)
            {
                if (Timer() == true)
                {
                    Spawn(new Vector3(Random.Range(transform.position.x - radiuse / 2, transform.position.x + radiuse / 2), transform.position.y, Random.Range(transform.position.z - radiuse / 2, transform.position.z + radiuse / 2)));
                    countObject++;
                }

            }
            else
                {
                    activated = false;
                    countObject = 0;
                }
        }
    }
    //
    private void OnValidate()
    {
        if (timer < 0) timer = 0;
        if (radiuse < 0) radiuse = 0;
        if (numberObjects < 1) numberObjects = 1;
    }
    //
    private bool Timer() 
    {
        if (timerCount >= timer)
        {
            timerCount = 0;
            return true;
        }
        else 
        {
            timerCount += Time.deltaTime;
            return false;
        }
    }
//--------------------------------------Отладка---------------------------------------
#if UNITY_EDITOR
    void OnDrawGizmos() 
    {
        if (forceOptions.useForce)
        {
            Custom.Utility.NGizmos.DrawArrow(transform.position, transform.forward * forceOptions.force / 10f, Color.red, 0.1f);
        }
        Custom.Utility.NGizmos.DrawSphere(transform.position, 0.04f, Color.yellow);
        Custom.Utility.NGizmos.DrawSphere(transform.position, radiuse, new Color(1 , 0, 0, 0.4f));
    }
    void OnDrawGizmosSelected()
    {
        Custom.Utility.NGizmos.DrawCircle(transform.position, 0.06f, transform.forward, Color.white);
    }
#endif
    //---------------------------------------API------------------------------------------
    // Спавним обьект
    public GameObject Spawn(Vector3 pos) 
    {
        if (spawnObject != null)
        {
            Vector2 dir;
            Rigidbody rb;
            GameObject spObj;

            dir = -transform.right;
            spObj = (GameObject)Instantiate(spawnObject, pos, transform.rotation);
            spawnObjectPool.Add(spObj);
            if (forceOptions.useForce) 
            {
                rb = spObj.GetComponent<Rigidbody>();
                if (rb != null) 
                {
                    rb.AddForce(dir * forceOptions.force, forceOptions.forceMode);
                }
                else 
                {
                    rb = spObj.AddComponent<Rigidbody>();
                    rb.AddForce(dir * forceOptions.force, forceOptions.forceMode);
                }
            }
            return spObj;
        }
        else    
            {
                return null;
            }
    }

    public void SpawnActive(bool arg)
    {
        activated = arg;
    }
}
