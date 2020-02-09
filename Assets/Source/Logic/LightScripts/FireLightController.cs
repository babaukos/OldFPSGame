using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SelectionBase]
public class FireLightController : MonoBehaviour 
{
    [SerializeField]
    private bool ignite;
    [SerializeField]
    private float timer;

    [Space]
    public ViewEffect viewEffect;
    public SoundEffect soundEffect;

    [System.Serializable]
    public class ViewEffect
    {
        public ParticleSystem particles;
        public Light fireLight;

        public float minInt = 3f;
        public float maxInt = 5f;
    }
    [System.Serializable]
    public class SoundEffect
    {
        public AudioSource source;
        public AudioClip fireSound;

        public float min;
        public float max;
    }

	// Use this for initialization
    void Start()
    {
        soundEffect.source.loop = true;
        soundEffect.source.clip = soundEffect.fireSound;

        soundEffect.source.volume = Random.Range(soundEffect.min, soundEffect.max);
        soundEffect.source.pitch = Random.Range(soundEffect.min, soundEffect.max);
    }

    void Update() 
    {
        if (ignite) 
        {
            if (soundEffect.source.isPlaying != true)
            soundEffect.source.Play();

            viewEffect.fireLight.gameObject.SetActive(true);
            viewEffect.particles.Play();

            StartCoroutine(Lighting());
            //StartCoroutine(Flickering());
        }
        else 
        {
            viewEffect.particles.Stop();
            viewEffect.fireLight.gameObject.SetActive(false);
            soundEffect.source.Stop();
        }
    }
    // мерцание интенсивностью
    IEnumerator Lighting() 
    {
        float lightInt;

        viewEffect.fireLight.intensity = Random.Range(viewEffect.minInt, viewEffect.maxInt);
        yield return new WaitForSeconds(timer);
    }
    //мерцание обьектом
    IEnumerator Flickering()
    {
        float timer;
        viewEffect.fireLight.gameObject.SetActive(true);
        timer = Random.Range(0.1f, 1);
        yield return new WaitForSeconds(timer);
        viewEffect.fireLight.gameObject.SetActive(false);
        timer = Random.Range(0.1f, 1);
        yield return new WaitForSeconds(timer);
    }
    //
    public void Ignite(bool arg) 
    {
        ignite = arg;
    }
}
