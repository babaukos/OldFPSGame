//
//
//

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dor : MonoBehaviour 
{
    public int ID;
    [Space]
    public bool open;
    public bool locked;
    [Space]
    public float speed;
    public float pathLength;
    public MoutionType moutionType;

    public AudioClip soundOpen;
    public AudioClip soundClose;
    public AudioClip soundLocked;


    public enum MoutionType
    {
        turn,
        sliding,
    }
    public enum MoutionVector
    {
        vertical,
        horizontal,
    }

    private bool moution;
    private Vector3 startPos;
    private Vector3 startRot;
    private AudioSource audSourc;

	// Use this for initialization
	void Start ()
    {
        GetComponent();
	}

    void GetComponent() 
    {
        startPos = transform.localPosition;
        startRot = transform.localEulerAngles;
        if(audSourc == null)
           audSourc = GetComponent<AudioSource>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (open && !locked) 
        {
            if (moutionType == MoutionType.turn)
            {
                transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, Mathf.LerpAngle(transform.localEulerAngles.y, startRot.y + pathLength, Time.deltaTime * speed), transform.localEulerAngles.z);
            }
            else 
            {
                transform.localPosition = new Vector3(Mathf.LerpAngle(transform.localPosition.x, startPos.x - pathLength / 100, Time.deltaTime * speed), transform.localPosition.y, transform.localPosition.z);
            }
            if (moution) 
            {
                if (soundOpen != null)
                audSourc.PlayOneShot(soundOpen);
            }
            moution = false;
        }
        else
        if (!open && !locked)
        {
            if (moutionType == MoutionType.turn)
            {
                transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, Mathf.LerpAngle(transform.localEulerAngles.y, startRot.y, Time.deltaTime * speed), transform.localEulerAngles.z);
            }
            else
            {
                transform.localPosition = new Vector3(Mathf.LerpAngle(transform.localPosition.x, startPos.x, Time.deltaTime * speed), transform.localPosition.y, transform.localPosition.z);
            }
            if (moution)
            {
                if (soundClose != null)
                audSourc.PlayOneShot(soundClose);
            }
            moution = false;
        }
        else
            {
                open = false;
                if (moution)
                {
                    if (soundLocked != null)
                    audSourc.PlayOneShot(soundLocked);
                }
                moution = false;
            }
	}
    //-------------------------------------------------Отладка---------------------------------------------	
#if UNITY_EDITOR
    void OnDrawGizmos() 
    {
        Color colPatch = new Color(0,255,255, 0.5f);
        switch(moutionType)
        {
            case MoutionType.turn:
                Custom.Utility.NGizmos.DrawSphere(transform.position, 0.04f, Color.yellow);
                Custom.Utility.NGizmos.DrawSolidArc(transform.position, Vector3.up, transform.right, pathLength, 0.5f, Color.cyan);
                break;
            case MoutionType.sliding:
                Custom.Utility.NGizmos.DrawArrow(transform.position, -transform.right * Mathf.Abs(pathLength / 100), Color.cyan, 0.3f);
                Custom.Utility.NGizmos.DrawSphere(transform.position, 0.04f, Color.yellow);
                break;
        }
    }
#endif
    //
    public void Open(bool arg) 
    {
        open = arg;
        moution = true;
    }
    public void Swipe()
    {
        open = !open;
        moution = true;
    }
    public void Locked(bool arg) 
    {
        locked = arg;
    }
    public void UnLocked()
    {
        locked = false;
    }
}
