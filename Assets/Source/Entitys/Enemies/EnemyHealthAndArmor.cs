//
//
//

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class EnemyHealthAndArmor : MonoBehaviour
{
    [SerializeField]
    float health;
    public int maxHealth = 100;
    public bool autoRegen;
    [DrawIf("autoRegen", true)]
    public int stepRegen;
    [Space]
    [SerializeField]
    float armor;
    public int maxArmor = 100;

    public GameObject blood;

    public AudioClip hitScream;
    public AudioClip deathScream;

    public GameObject deadBody;

    public HPEvents events;

    AudioSource source;
    bool alive = true;
    [System.Serializable]
    public class HPEvents
    {
        public UnityEvent hitEvent;
        public UnityEvent lessHPEvent;
    }

    private void Start()
    {        
        source = GetComponent<AudioSource>();
    }

    private void Update()
    {
        armor = Mathf.Clamp(armor, 0, maxArmor);
        health = Mathf.Clamp(health, 0, maxHealth);

        if (autoRegen == true) 
        {
            if (health < maxHealth)
                health += stepRegen * Time.deltaTime;
        }
        if (health <= 0 && alive)
        {
            alive = false;
            Deadth();
        }
    }

    private void OnValidate() 
    {
        armor = Mathf.Clamp(armor, 0, maxArmor);
        health = Mathf.Clamp(health, 0, maxHealth);
    }

    private void Deadth() 
    {
        events.lessHPEvent.Invoke();
        source.PlayOneShot(deathScream);
        Instantiate(deadBody, transform.position, transform.rotation);

        Destroy(gameObject, 0.2f);
    }
    private void OnDestroy()
    {
        if (GameManager.Instance != null)
            GameManager.Instance.AddEnemyFrag(1);
    }
    //
    private void AddHealth(float value)
    {
        health += value;
    }
    private void AddArmor(float value)
    {
        armor += value;
    }
    // Повреждение
    public void Damage(float damage)
    {
        events.hitEvent.Invoke();
        source.PlayOneShot(hitScream);
        if(armor > 0 && armor >= damage)
        {
            armor -= damage;
        } else if (armor > 0 && armor < damage)
        {
            damage -= armor;
            armor = 0;
            health -= damage;
        } else
        {
            health -= damage;
        }
    }
    public void Damage(int damage, RaycastHit hit)
    {
        Damage(damage);
        Instantiate(blood, hit.point, Quaternion.FromToRotation(Vector3.forward, hit.normal));
    }
    public void DebugString(string str)
    {
        Debug.Log(str);
    }
}
