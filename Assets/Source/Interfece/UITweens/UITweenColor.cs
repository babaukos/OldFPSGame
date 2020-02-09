// ver. 0.1
// 
// Michael Khmelevsky

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

[AddComponentMenu("UI/Tweens/TweenColor", order: 1)]
public class UITweenColor : MonoBehaviour
{
    [SerializeField]
    private bool play;
    [SerializeField]
    private bool playAwake;
    [SerializeField]
    private bool playOneshote;

    [Tooltip("Время анимации")]
    public float timeTransition = 1f;
    [Tooltip("Пропустить анимацию")]
    public bool skipTransition = false;
    [Tooltip("Первый цвет")]
    public Color oneColor = Color.white;
    [Tooltip("Первый цве")]
    public Color twoColor = Color.white;
    [Tooltip("Еффект анимации текста")]
    public TweenType tweenType;

    public Function function;

    private bool transitioning = false;

    private Image imageCompponet;
    private Text textComponent;
    private Color lerpedColor;
    private float ratio;

    public enum TweenType
    {
        blink,
        fading,
        manifestation,
    }
    public enum Function 
    {
        line,
        square,
        quadrat,
    }

    //
    private void Awake() 
    {
        imageCompponet = GetComponent<Image>();
        textComponent = GetComponent<Text>();
    }

	// Use this for initialization
    private void Start() 
    {
        imageCompponet.color = oneColor;
        //textComponent.color = oneColor;
        SetState();
	}
	
	// Update is called once per frame
    private void Update()
    {
        ColorLerp();
	}
    //
    private void SetState()
    {
        switch (tweenType)
        {
            case TweenType.blink:
                break;
            case TweenType.fading:
                StartCoroutine(ColorFading());
                break;
            case TweenType.manifestation:
                StartCoroutine(ColorManifestation());
                break;
        }
    }
    //
    private void ColorLerp()
    {
        ratio += Time.deltaTime * timeTransition;
       // ratio = Mathf.Clamp01(ratio);
        //imageCompponet.color = Color.Lerp(oneColor, twoColor, ratio);
        //oneColor = imageCompponet.color;
        //imageCompponet.color = Color.Lerp(imageCompponet.color, twoColor, Mathf.Sqrt(ratio)); // A cool effect
        //imageCompponet.color = Color.Lerp(imageCompponet.color, twoColor, ratio * ratio); // Another cool effect

        if (imageCompponet.color != twoColor)
        {
            switch (function)
            {
                case Function.line:
                    imageCompponet.color = Color.Lerp(imageCompponet.color, twoColor, ratio);
                    break;
                case Function.quadrat:
                    imageCompponet.color = Color.Lerp(imageCompponet.color, twoColor, Mathf.Sqrt(ratio));  // A cool effect
                    break;
                case Function.square:
                    imageCompponet.color = Color.Lerp(imageCompponet.color, twoColor, ratio * ratio);      // Another cool effect
                    break;
            }
        }
        else
            {
                imageCompponet.color = oneColor;
            }
    }

    //
    private IEnumerator ColorBlink()
    {
        
        //transitioning = true;
        //while (true)
        //{
        //    lerpedColor = Color.Lerp(oneColor, twoColor, Time.deltaTime * timeTransition);
        //    imageCompponet.color = lerpedColor;
        //    //yield return new WaitForSeconds(0.5f);
        //    //lerpedColor = Color.Lerp(twoColor, oneColor, Time.deltaTime * timeTransition);
        //    //imageCompponet.color = lerpedColor;
        //    //yield return new WaitForSeconds(0.5f);
        //    yield break;
        //}
        if (imageCompponet.color.ToString() != twoColor.ToString())
        {
            imageCompponet.color = twoColor;
        }
        else
            {
                imageCompponet.color = oneColor;
            }
        yield return new WaitForSecondsRealtime(timeTransition);
        //transitioning = false;
    }

    // Исчезновение текста
    private IEnumerator ColorFading()
    {
        if (transitioning) yield break;

        transitioning = true;
        Color color = imageCompponet.color;

        for (float i = 1; i > 0; i -= 0.01f)
        {
            if (skipTransition)
            {
                color.a = 0;
                imageCompponet.color = color;
                yield break;
            }
            color.a = i;
            imageCompponet.color = color;
            yield return new WaitForSecondsRealtime(timeTransition / 100);
        }
        transitioning = false;
    }

    // Появление текста
    private IEnumerator ColorManifestation()
    {
        if (transitioning) yield break;

        transitioning = true;
        Color color = imageCompponet.color;

        for (float i = 0; i < 1; i += 0.01f)
        {
            if (skipTransition)
            {
                color.a = 1;
                imageCompponet.color = color;
                yield break;
            }
            color.a = i;
            imageCompponet.color = color;
            yield return new WaitForSecondsRealtime(timeTransition / 100);
        }
        transitioning = false;
    }

    public void Play() 
    {
        play = true;
    }
}
