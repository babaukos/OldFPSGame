using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class LampLightController : MonoBehaviour 
{
    [SerializeField]
    private bool lightining;
    [SerializeField]
    private float delay;
    [SerializeField]
    private float flickeringTime;
    [Space]
    public EffectLight effectLight;
    public ViewSprite modelLight;

    [System.Serializable]
    public class EffectLight
    {
        public Light light;

        public float minInt = 3f;
        public float maxInt = 5f;

        public SpriteRenderer volumeLight;

        public float minColInt = 0.5f;
        public float maxColInt = 1f;
    }
    [System.Serializable]
    public class ViewSprite
    {
        public SpriteRenderer lampLightSprite;

        public Sprite onSprite;
        public Sprite offSprite;
    }

    private float timer;

    // Use this for initialization
    void Start()
    {
        //StartCoroutine(FlickeringLight());
    }
    // Update is called once per frame
    void Update()
    {
        if (lightining) 
        {
            if (timer < delay) 
            {
                timer += Time.deltaTime;
            }
            else
            {
                modelLight.lampLightSprite.sprite = modelLight.onSprite;
                effectLight.light.gameObject.SetActive(true);
                if(effectLight.volumeLight != null)
                effectLight.volumeLight.gameObject.SetActive(true);
            }
            StartCoroutine(Flickering());
        }
        else 
        {
            modelLight.lampLightSprite.sprite = modelLight.offSprite;
            effectLight.light.gameObject.SetActive(false);
            if (effectLight.volumeLight != null)
            effectLight.volumeLight.gameObject.SetActive(false);
            timer = 0;
        }
    }
    //
    IEnumerator Flickering()
    {
        if (flickeringTime > 0)
        {
            yield return new WaitForSeconds(flickeringTime);
            if (effectLight.light != null)
            {
                effectLight.light.intensity = Random.Range(effectLight.minInt, effectLight.maxInt);
            }
            if (effectLight.volumeLight != null)
            {
                Color col = effectLight.volumeLight.color;
                col.a = Random.Range(effectLight.minColInt, effectLight.maxColInt);
                effectLight.volumeLight.color = col;
            }
            yield return new WaitForSeconds(flickeringTime);
        }
    }
    //-------------------------------------------------Отладка---------------------------------------------
#if UNITY_EDITOR
    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.matrix = Matrix4x4.TRS(transform.position, transform.rotation, Vector3.one); 
        Custom.Utility.NGizmos.DrawVisor(Vector3.zero, 45f, 1f, 0.01f, 1);
    }
#endif
    //-----------------------------------------------------------------------------------------------------
    public void Lightining(bool arg) 
    {
        lightining = arg;
    }
}
