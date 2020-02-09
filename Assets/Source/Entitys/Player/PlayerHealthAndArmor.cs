//
//
//

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class PlayerHealthAndArmor : MonoBehaviour
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

    public AudioClip hitScream;
    public AudioClip deathScream;

    public GameObject deadBody;

    public HPEvents events;

    AudioSource source;
    bool isGameOver = false;
    [System.Serializable]
    public class HPEvents
    {
        public UnityEvent hitEvent;
        public UnityEvent lessHPEvent;
    }

    void Start()
    {        
        source = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (UIStatsScreen.Instance != null)
            UIStatsScreen.Instance.ArmorText.text = Mathf.Clamp(armor, 0, maxArmor).ToString();
        if (UIStatsScreen.Instance != null)
            UIStatsScreen.Instance.HealthText.text = Mathf.Clamp(health, 0, maxHealth).ToString();

        if (autoRegen == true)
        {
            if (health < maxHealth)
                health += stepRegen * Time.deltaTime;
        }

        if(health <= 0 && !isGameOver)
        {
            isGameOver = true;
            Deadth();
        }
    }

    private void Deadth() 
    {
        events.lessHPEvent.Invoke();
        source.PlayOneShot(deathScream);
        Instantiate(deadBody, transform.position + new Vector3(0, 1.2f, 0), transform.rotation);

        if (UIStatsScreen.Instance != null)
            GameManager.Instance.GameOver();

        Destroy(gameObject, 1.0f);
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
    //
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

        if (UIFlashScreen.Instance != null)
            UIFlashScreen.Instance.TookNegativeScreen();
    }
    public void Damage(int damage, RaycastHit hit)
    {
        Damage(damage);
        //Instantiate(blood, hit.point, Quaternion.FromToRotation(Vector3.forward, hit.normal));
    }
    public void DebugString(string str) 
    {
        Debug.Log(str);
    }
}
