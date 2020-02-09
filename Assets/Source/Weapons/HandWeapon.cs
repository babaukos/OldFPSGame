using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class HandWeapon : MonoBehaviour 
{
    public Sprite idleSprite;
    public Sprite attackSprite;
    public float range;
    public float damage;
    public float recoil = 30;

    public AudioClip attackSound;

    bool attack = false;
    AudioSource source;
    SpriteRenderer render;

	// Use this for initialization
	void Start ()
    {
        render = GetComponent<SpriteRenderer>();
        source = GetComponent<AudioSource>();
	}
    //
    void Update()
    {
        if (UIStatsScreen.Instance != null)
            UIStatsScreen.Instance.AmmunitionText.text = "∞";

        if (Input.GetButtonDown("Fire1"))
            attack = true;
    }
    //
    void FixedUpdate()
    {
        Vector2 bulletOffset = Random.insideUnitCircle * UIDynamicCrosshair.spread;
        Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2 + bulletOffset.x, Screen.height / 2 + bulletOffset.y, 0));
        RaycastHit hit;
        if (attack == true)
        {
            attack = false;
            source.PlayOneShot(attackSound);
            StartCoroutine("Attack");
            if (Physics.Raycast(ray, out hit, range, -5, QueryTriggerInteraction.Ignore))
            {
                Debug.Log("Попали в коллайдер " + hit.collider.gameObject.name);
                //hit.collider.gameObject.SendMessage("pistolHit", pistolDamage, SendMessageOptions.DontRequireReceiver);
                EnemyHealthAndArmor enemy = hit.collider.GetComponent<EnemyHealthAndArmor>();
                if (enemy != null)
                    enemy.Damage((int)damage, hit);

                ObjectProperty envir = hit.collider.GetComponent<ObjectProperty>();
                if (envir != null)
                    envir.Hit((int)damage, hit);
            }
        }
    }

    IEnumerator Attack()
    {
        //Camera.main.GetComponent<CameraShake>().Shake(0.09f, 0.008f);
        GameObject.FindObjectOfType<CameraShake>().Shake(0.09f, recoil/1000);

        render.sprite = attackSprite;
        yield return new WaitForSeconds(0.1f);
        render.sprite = idleSprite;
    }
}
