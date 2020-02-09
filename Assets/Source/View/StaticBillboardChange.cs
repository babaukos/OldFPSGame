using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticBillboardChange : MonoBehaviour 
{
    //public Dir dir;
    public Sprite frontSprite;
    public Sprite backSprite;
    public Sprite leftSprite;
    public Sprite rightSprite;

    //public AnimationClip[] anims;
    [SerializeField]
    bool isAnimated;

    Animator anim;
    public SpriteRenderer sr;

    float angle;

    public enum Dir 
    {
        dir2,
        dir4,
        dir8,
    }

    private void Awake()
    {
        if (anim == null)
        anim = GetComponent<Animator>();
        if (sr==null)
        sr = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        angle = GetAngle();

        //anim.Play(anims[index].name);
        if (angle >= 225.0f && angle <= 315.0f)     // тыльная проекция
        {
            //ChangeSprite(0);
            sr.sprite = backSprite;
        }
        else if (angle < 225.0f && angle > 135.0f) // левая проекция
        {
            //ChangeSprite(1);
            sr.sprite = rightSprite;
        }
        else if (angle <= 135.0f && angle >= 45.0f) // фронтальная проекция
        {    //ChangeSprite(2);
            sr.sprite = frontSprite;
        }
        else if ((angle < 45.0f && angle > 0.0f) || (angle > 315.0f && angle < 360.0f))// правая проекция
        {
            //ChangeSprite(3);
            sr.sprite = leftSprite;
        }
    }

    float GetAngle()
    {
        //Camera camera = Camera.main;
        Vector3 direction = Camera.main.transform.position - transform.position;
        float angleTemp = Mathf.Atan2(direction.z, direction.x) * Mathf.Rad2Deg;
        angleTemp += 180.0f;
        return angleTemp;
    }

}
