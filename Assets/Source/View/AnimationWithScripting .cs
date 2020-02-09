using UnityEngine;
using System.Collections;
public class AnimationWithScripting : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public Sprite[] walk;
    public Sprite[] idle;
    public Sprite[] kick;
    void Start()
    {
        StartCoroutine(Idle());
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            StopAllCoroutines();
            StartCoroutine(Idle());
        }
        if (Input.GetKeyDown(KeyCode.K))
        {
            StopAllCoroutines();
            StartCoroutine(Kick());
        }
        if (Input.GetKeyDown(KeyCode.W))
      {
            StopAllCoroutines();
            StartCoroutine(Walk());
        }
    }
    IEnumerator Idle()
    {
        int i;
        i = 0;
        while (i < idle.Length)
        {
            spriteRenderer.sprite = idle[i];
            i++;
            yield return new WaitForSeconds(0.07f);
            yield return 0;
                
        }
        StartCoroutine(Idle());
    }
    IEnumerator Walk()
    {
        int i;
        i = 0;
        while (i < walk.Length)
        {
            spriteRenderer.sprite = walk[i];
            i++;
            yield return new WaitForSeconds(0.07f);
            yield return 0;
        }
        StartCoroutine(Walk());
    }

    IEnumerator Kick()
    {
        int i;
        i = 0;
        while (i < kick.Length)
        {
            spriteRenderer.sprite = kick[i];
            i++;
            yield return new WaitForSeconds(0.07f);
            yield return 0;

        }
        StartCoroutine(Kick());
    }
}