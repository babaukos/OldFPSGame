//
//
//

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Granate : MonoBehaviour
{
#region public var
    public bool init;
    [Space]
    [Tooltip("Задержка взрывателя [c]")]
    public float ignitionTime;

    public Type type;

    [ConditionalHide("type", "fragGranate")]
    [Tooltip("Дистанция разлета осколков")]
    public float fragmentRange;
    [Tooltip("Дистанция распостронения волны")]
    public float blastWaveRange;
    [Space]
    public float force;
    public int fragmentValue = 1;
    public float damage = 20;

    //public GameObject partsPref;
    [Space]
    public GameObject explosionPref;
    public AudioSource explosionSource;

    public enum Type 
    {
        fragGranate,
        smockGranate,
    }

#endregion
#region private var
    private float qurTimer;

#endregion


    // Use this for initialization
	void Start () 
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (init) 
        {
            if (qurTimer < ignitionTime)
            {
                qurTimer += Time.deltaTime;
            }
            else 
                {
                    qurTimer = 0;
                    if (type == Type.fragGranate) 
                    {
                        Explosion();
                    }
                    else 
                    {
                        Smokin();
                    }
                }
        }
	}
    //
    void Smokin() 
    {
        ParticleSystem ps;
        ps = explosionPref.GetComponent<ParticleSystem>();
        ps.Play();
    }
    //
    void Explosion() 
    {
        // Fragment Effect
        //if (partsPref != null)
        //{
            Projectl[] fragment = new Projectl[fragmentValue];
            for (int fr = 0; fr < fragmentValue; fr++)
            {
                var dir = new Vector3(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f));
                var direction = Random.insideUnitCircle.normalized;
                Vector3 randomDirection = new Vector3(Random.value, Random.value, Random.value);

                //if (Physics.Raycast(ray, out hit, shootgunRange, -5, QueryTriggerInteraction.Ignore))
                //{
                //    Debug.Log("Попали в коллайдер " + hit.collider.gameObject.name);
                //    //hit.collider.gameObject.SendMessage("pistolHit", pistolDamage, SendMessageOptions.DontRequireReceiver);
                //    Enemy enemy = hit.collider.GetComponent<Enemy>();
                //    if (enemy != null)
                //        enemy.PistolHit((int)shootgunDamage, hit);

                //    EnvirState envir = hit.collider.GetComponent<EnvirState>();
                //    if (envir != null)
                //        envir.PistolHit((int)shootgunDamage, hit);
                //}


                
                //fragment[fr] = Instantiate(partsPref, transform.position, transform.rotation).GetComponent<Projectl>();
                //fragment[fr].dir = dir;
                //fragment[fr].speed = Random.Range(5, 15);
            }
        //}
        // Explosion Effect
        if (explosionPref != null)
        {
            GameObject explosion = Instantiate(explosionPref, transform.position, transform.rotation) as GameObject;
        }
        //
        if (explosionSource != null)
        {
           explosionSource.Play();
        }

        // Projectl eeffect
        //RaycastHit rh;
        //for (int r = 0; r < fragmentValue; r++)
        //{
        //    var dir = new Vector3(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f));
        //    if (Physics.Raycast(transform.position, dir * fragmentRange, out rh)) 
        //    {
            
            
        //    }
        //}
        // BlastWave Effect
        //Collider[] col = Physics.OverlapSphere(transform.position, blastWaveRange);
        //foreach(Collider hit in col)
        //{
        //    Rigidbody rb = hit.GetComponent<Rigidbody>();
        //    if (rb != null)
        //    {
        //        rb.AddExplosionForce(force, transform.position, blastWaveRange);
        //    }
        //}

       Destroy(gameObject);
    }

    //-------------------------------------------------Отладка---------------------------------------------
#if UNITY_EDITOR
    private void  OnDrawGizmos()
    {
        var c1 = new Color(1, 0, 0, 0.2f);          // red
        var c2 = new Color(1, 0.92f, 0.016f, 0.2f); // yelow

        Custom.Utility.NGizmos.DrawSphere(transform.position, fragmentRange, c1);
        Custom.Utility.NGizmos.DrawSphere(transform.position, blastWaveRange, c2);
    }
#endif
}
