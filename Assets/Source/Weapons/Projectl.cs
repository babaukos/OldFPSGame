//
//
//

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectl : MonoBehaviour 
{
    public float speed;
    public float damage;
    public AudioClip sound;

    private AudioSource audSour;

	// Use this for initialization
	void Start () 
    {
        audSour = GetComponent<AudioSource>();
        if (audSour==null)
        audSour = gameObject.AddComponent<AudioSource>();
        audSour.volume = 0.4f;
        audSour.PlayOneShot(sound);
	}
	
	// Update is called once per frame
	void Update () 
    {
        //transform.position += transform.forward * speed * Time.deltaTime;
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
        Destroy(gameObject, 10);
	}

    //
    void OnCollisionEnter(Collision collision) 
    {
        PlayerHealthAndArmor pha = collision.transform.GetComponent<PlayerHealthAndArmor>();
        if (pha != null) 
        {
            collision.transform.SendMessage("Damage", damage);
        }
        Destroy(gameObject);
    }
    //-------------------------------------------------Отладка---------------------------------------------	
#if UNITY_EDITOR
    void OnDrawGizmos()
    {
       Custom.Utility.NGizmos.DrawArrow(transform.position, transform.forward * 0.3f, Color.red, 0.3f);
    }
#endif
}
