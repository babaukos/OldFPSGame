// ver. 0.1
//
// Michael Khmelevsky

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Timer : MonoBehaviour
{
    [SerializeField]
    [Tooltip("Включен ли тригер")]
    private bool activated = true;
    [SerializeField]
    [Tooltip("Разовый / цыкличный будильник")]
    private TimerType timerType;

    [SerializeField]
    [Tooltip("Время срабатывания [c]")]
    private float alarmTime;
    private float qurentTime;

    //public UnityEvent startTimeEvent;
    public UnityEvent endTimeEvent;

    private int attempt = 1;
    public enum TimerType 
    {
      once,
      repeat,
    }

//------------------------------------------------------------------------------------
    // Use this for initialization
	void Start () 
    {
		
	}
	// Update is called once per frame
	void Update () 
    {
        if (activated)
        { 
          if (qurentTime < alarmTime)
          {
              qurentTime += Time.deltaTime;
          }
          else  
            {
                if (timerType == TimerType.repeat)
                {
                    endTimeEvent.Invoke();
                    qurentTime = 0;
                }
                else 
                    {
                        if (attempt > 0)
                        {
                          endTimeEvent.Invoke();
                          attempt -= 1;
                        }
                    }
            }
        }
        else 
            {
                qurentTime = 0;
                attempt = 1;
            }
	}
    //
    private void OnValidate()
    {
        if (alarmTime < 0) 
        {
            alarmTime = 0;
        }
        if (qurentTime < 0) 
        {
            qurentTime = 0;
        }
    }
//--------------------------------------Отладка--------------------------------------	
#if UNITY_EDITOR
    void OnDrawGizmos () 
    {
        Custom.Utility.NGizmos.DrawCube(transform.position, new Vector3(0.1f, 0.1f, 0.0f), new Color(1, 1, 1, 0.5f));
        Custom.Utility.NGizmos.DrawString("[" + qurentTime.ToString("0.0") + "/" + alarmTime.ToString() + "]", transform.position + new Vector3(0, 0.04f, 0), Color.magenta, Color.black);
        Custom.Utility.NGizmos.DrawString(gameObject.name, transform.position - new Vector3(0, 0.15f, 0), Color.red, Color.black);
    }
#endif
//---------------------------------------API-----------------------------------------	
    //
    public void DestroySelf()
    {
        DestroyOther(gameObject);
    }
    public void DestroyOther(GameObject gObject)
    {
        Destroy(gObject);
    }
    public void TimerStatus(bool argument) 
    {
        activated = argument;
    }
    public void TimerDebug()
    {
        Debug.Log("Timer" + gameObject.name + "[" + qurentTime.ToString() + "/" + alarmTime.ToString() + "]");
    }
}
