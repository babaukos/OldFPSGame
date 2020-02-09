using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(AudioSource))]
public class Shotgun : Weapons 
{
    public Sprite idleShootgun;
    public Sprite shotShootgun;
    public Light lightEffect;

    public GameObject shell;

    public float shootgunDamage;
    public float shootgunRange;

    public float recoil = 30;
    public float relodeTime = 2;

    public AudioClip shotSound;
    public AudioClip reloadSound;
    public AudioClip emptyGunSound;

    public int pellets = 4;
    public int ammoAmount;
    public int ammoClipSize;

    int ammoLeft;
    int ammoClipLeft;

    bool isShot;
    bool isReloading;

    AudioSource source;
    SpriteRenderer render;

    void Awake()
    {
        render = GetComponent<SpriteRenderer>();
        source = GetComponent<AudioSource>();
        ammoLeft = ammoAmount;
        ammoClipLeft = ammoClipSize;
        lightEffect.gameObject.SetActive(false);
    }

    void Update()
    {
        if (UIStatsScreen.Instance != null)
            UIStatsScreen.Instance.AmmunitionText.text = ammoClipLeft + "/" + ammoLeft;

        if (Input.GetButtonDown("Fire1") && isReloading == false)
            isShot = true;
        if (Input.GetKeyDown(KeyCode.R) && isReloading == false)
        {
            Reload();
        }
    }

    void FixedUpdate()
    {
        RaycastHit hit;
        if (isShot == true && ammoClipLeft > 0 && isReloading == false)
        {
            isShot = false;
            UIDynamicCrosshair.spread += UIDynamicCrosshair.PISTOL_SHOOTING_SPREAD;
            ammoClipLeft--;
            source.PlayOneShot(shotSound);
            StartCoroutine("Shot");
            for (int b = 0; b < pellets; b++)
            {
                Vector2 bulletOffset = Random.insideUnitCircle * UIDynamicCrosshair.spread * 2;
                Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2 + bulletOffset.x, Screen.height / 2 + bulletOffset.y, 0));
                if (Physics.Raycast(ray, out hit, shootgunRange, -5, QueryTriggerInteraction.Ignore))
                {
                    Debug.Log("Попали в коллайдер " + hit.collider.gameObject.name);
                    //hit.collider.gameObject.SendMessage("pistolHit", pistolDamage, SendMessageOptions.DontRequireReceiver);
                    EnemyHealthAndArmor enemy = hit.collider.GetComponent<EnemyHealthAndArmor>();
                    if (enemy != null)
                        enemy.Damage((int)shootgunDamage, hit);

                    ObjectProperty envir = hit.collider.GetComponent<ObjectProperty>();
                    if (envir != null)
                        envir.Hit((int)shootgunDamage, hit);
                }
            }
        }
        else if (isShot == true && ammoClipLeft <= 0 && isReloading == false)
        {
            isShot = false;
            Reload();
        }
    }
    
    void Reload()
    {
        int bulletsToReload = ammoClipSize - ammoClipLeft;
        if (ammoLeft >= bulletsToReload)
        {
            StartCoroutine("ReloadWeapon");
            ammoLeft -= bulletsToReload;
            ammoClipLeft = ammoClipSize;
        }
        else if (ammoLeft < bulletsToReload && ammoLeft > 0)
        {
            StartCoroutine("ReloadWeapon");
            ammoClipLeft += ammoLeft;
            ammoLeft = 0;
        }
        else if (ammoLeft <= 0)
        {
            source.PlayOneShot(emptyGunSound);
        }
    }

    IEnumerator ReloadWeapon()
    {
        isReloading = true;
        source.PlayOneShot(reloadSound);
        yield return new WaitForSeconds(relodeTime);
        isReloading = false;
    }
    IEnumerator Shot()
    {
        //Camera.main.GetComponent<CameraShake>().Shake(0.09f, 0.03f);
        GameObject.FindObjectOfType<CameraShake>().Shake(0.09f, recoil/1000);

        Instantiate(shell, transform.position, transform.rotation).GetComponent<Rigidbody>().AddForce(-transform.right * Random.Range(47,50));


        lightEffect.gameObject.SetActive(true);
        render.sprite = shotShootgun;
        yield return new WaitForSeconds(0.2f);
        render.sprite = idleShootgun;
        lightEffect.gameObject.SetActive(false);
    }

    public override int GetAmmoAmount()
    {
        return ammoAmount;
    }
    public override void SetAmmoAmount(int val)
    {
        ammoAmount = val;
    }

    public override int GetAmmoClipSize()
    {
        return ammoClipSize;
    }
    public override void SetAmmoClipSize(int val)
    {
        ammoClipSize = val;
    }
}
