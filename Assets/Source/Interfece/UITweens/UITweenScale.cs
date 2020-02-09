// ver. 0.1
// 
// Michael Khmelevsky

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

[AddComponentMenu("UI/Tweens/TweenScale", order: 1)]
public class UITweenScale : MonoBehaviour
{
    public bool playAwake;
    public TweenPlay tweenPlay;
    public float timeTransition = 2;

    public Tweens tweenType;

    [DrawIf("tweenType", Tweens.CURVE, DisablingType.Draw)]
    public float start;
    [DrawIf("tweenType", Tweens.CURVE, DisablingType.Draw)]
    public float end;
    [DrawIf("tweenType", Tweens.CURVE)]
    public AnimationCurve curve;

    public enum Tweens
    {
        LINEAR,
        EASE_IN,
        CURVE
    }
    public enum TweenPlay
    {
        oneShot,
        loop,
        pingPong,
    }

    private float scale;
    private float startTime;
    private int count;
    private bool play;

    // Use this for initialization
    private void Awake()
    {
        if (playAwake)
            Play();
    }

    private void OnEnable() 
    {
        Play();
    }
    private void OnDisable() 
    {
        ResetValue();
    }

    void InittValue()
    {
        if (count < 1)
        {
            //startTime = Time.time;
            scale = start;
            //count += 1;
        }
    }
    void ResetValue() 
    {
        startTime = 0;
        scale = 0;
        count = 0;
    }

    // Update is called once per frame
    private void Update()
    {
        if (play)
        {
            switch (tweenType)
            {
                case Tweens.LINEAR:
                    scale = Linear(DeltaTime());
                    break;

                case Tweens.EASE_IN:
                    scale = EaseIn(DeltaTime());
                    break;

                case Tweens.CURVE:
                    scale = Curve(DeltaTime());
                    break;

                default:
                    scale = 0;
                    break;
            }

            transform.localScale = new Vector3(scale, scale, transform.localScale.z);

            if (tweenPlay == TweenPlay.loop)
            {
                Loop();
            }
            else if (tweenPlay == TweenPlay.pingPong)
            {
                PingPong();
            }
        }
    }

    //
    private float DeltaTime()
    {
        float timeDelta = Time.time - startTime;

        if (timeDelta < timeTransition)
        {
            return timeDelta / timeTransition;
        }
        else
        {
            return 1;
        }
    }

    //--------------------------------------
    private void Stoping()
    {
        if (DeltaTime() == 1)
        {
            count += 1;
            play = false;
        }
    }
    private void Loop()
    {
        if (DeltaTime() == 1)
        {
            transform.localScale = new Vector3(start, start, transform.localScale.z);
            startTime = Time.time;
        }
    }
    private void PingPong()
    {
        if (DeltaTime() == 1)
        {
            float temp = end;
            end = start;
            start = temp;
            startTime = Time.time;
        }
    }

    //---------------------------------------
    private float Linear(float delta)
    {
        return Mathf.Lerp(start, end, delta);
    }
    private float EaseIn(float delta)
    {
        return Mathf.Lerp(start, end, delta * delta);
    }
    private float Curve(float delta)
    {
        return (end - start) * curve.Evaluate(delta) + start;
    }

    //----------------------------------------
    public void Play()
    {
        InittValue();
        play = true;
    }
    public void Stop()
    {
        transform.localScale = new Vector3(start, start, transform.localScale.z);
        startTime = Time.time;
        play = false;
    }
}