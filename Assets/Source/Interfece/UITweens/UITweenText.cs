// ver. 0.1
// 
// Michael Khmelevsky

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[AddComponentMenu("UI/Tweens/TweenText", order: 1)]
[RequireComponent(typeof(Text))]
public class UITweenText : MonoBehaviour 
{
    [Tooltip("Время анимации")]
    public float timeTransition = 1f;
    [Tooltip("Пропустить анимацию")]
    public bool skipTransition = false;
    [Tooltip("Еффект анимации текста")]
    public TweenType tweenType;

    private bool transitioning = false;
    private Text textCompponet;

    private string inputString;
    private string outputString;
    public enum TweenType
    {
        typewriter,
        fading,
        manifestation,
    }


    //
    private void SetState()
    {
        inputString = textCompponet.text;
        switch (tweenType)
        {
            case TweenType.typewriter:
                textCompponet.text = "";
                StartCoroutine(TextPrint(inputString, timeTransition));
                break;
            case TweenType.fading:
                outputString = inputString;
                StartCoroutine(ColorFading());
                break;
            case TweenType.manifestation:
                outputString = inputString;
                StartCoroutine(ColorManifestation());
                break;
        }
    }

    //
    private void Awake() 
    {
       textCompponet = GetComponent<Text>();
    }

	// Use this for initialization
    private void Start() 
    {
        SetState();
	}
	
	// Update is called once per frame
    private void Update()
    {
        textCompponet.text = outputString;
	}

    //
    private void OnDisable()
    {
        
    }

    //
    private void OnEnable()
    {
        SetState();
    }

    // Вывод текста побуквенно
    private IEnumerator TextPrint(string input, float delay)
    {
        if (transitioning) yield break;

        transitioning = true;
        for (int i = 0; i <= input.Length; i++)
        {
            if (skipTransition)
            {
                outputString = input;
                yield break;
            }
            outputString = input.Substring(0, i);
            // yield return new WaitForSeconds(delay);
            yield return new WaitForSecondsRealtime(delay/100);
        }
        transitioning = false;
    }

    // Исчезновение текста
    private IEnumerator ColorFading()
    {
        if (transitioning) yield break;

        transitioning = true;
        Color color = textCompponet.color;

        for (float i = 1; i > 0; i -= 0.01f)
        {
            if (skipTransition)
            {
                color.a = 0;
                textCompponet.color = color;
                yield break;
            }
            color.a = i;
            textCompponet.color = color;
            yield return new WaitForSecondsRealtime(timeTransition / 100);
        }
        transitioning = false;
    }

    // Появление текста
    private IEnumerator ColorManifestation()
    {
        if (transitioning) yield break;

        transitioning = true;
        Color color = textCompponet.color;

        for (float i = 0; i < 1; i += 0.01f)
        {
            if (skipTransition)
            {
                color.a = 1;
                textCompponet.color = color;
                yield break;
            }
            color.a = i;
            textCompponet.color = color;
            yield return new WaitForSecondsRealtime(timeTransition / 100);
        }
        transitioning = false;
    }

    public void Play()
    {

    }
}
