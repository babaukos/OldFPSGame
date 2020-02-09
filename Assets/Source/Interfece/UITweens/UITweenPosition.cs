//
//
//

using UnityEngine;
using System.Collections;

[AddComponentMenu("UI/Tweens/TweenPosition", order: 1)]
public class UITweenPosition : MonoBehaviour 
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

    private float move;
    private float startTime;
    private int count;
    private bool play;

	// Use this for initialization
    private void Start()
    {
        if (playAwake)
            play = true;
	}

    private void SetStartValue()
    {
        if (count < 1)
        {
            startTime = Time.time;
            start = transform.position.y;
            end = start + end;
            count += 1;
        }
    }
	// Update is called once per frame
    private void Update()
    {
        if (playAwake)
        {
            SetStartValue();
        }
        if (play)
        {
            switch (tweenType)
            {
                case Tweens.LINEAR:
                    move = Linear(DeltaTime());
                    break;

                case Tweens.EASE_IN:
                    move = EaseIn(DeltaTime());
                    break;

                case Tweens.CURVE:
                    move = Curve(DeltaTime());
                    break;

                default:
                    move = 0;
                    break;
            }

            transform.position = new Vector3(transform.position.x, move, transform.position.z);

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
    private void Stop()
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
            transform.position = new Vector3(transform.position.x, start, transform.position.z);
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
        SetStartValue();
        play = true;
    }
}