// ver. 0.2
// Простой триггер
// Michael Khmelevsky

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System.Reflection;

[SelectionBase]
public class Trigger : MonoBehaviour
{
    [Tooltip("В кокой среде используется")]
    public TypePhisic usingPhisic;

    protected RaycastHit hit3D;
    protected RaycastHit2D hit2D;

    protected CircleCollider2D trigger2D;
    protected SphereCollider trigger3D;

    [Header("   Параметры")]
    [Tooltip("Включен ли тригер")]
    public bool activated = true;
    [Tooltip("Повторяемость: [false]- отключится после срабатывания")]
    public bool repeatable = true;
    [Tooltip("Форма триггера")]
    public TriggerShape shape;
    [DrawIf("shape", TriggerShape.sphere)]
    [Tooltip("Радиус зоны триггера")]
    public float radiuse = .2f;
    [DrawIf("shape", TriggerShape.box)]
    [Tooltip("Размер зоны триггера")]
    public Vector3 langth = Vector3.one;

    [Space][Space]
    public string text;

    [Header("   Активаторы")]
    public string activationTag;
    public LayerMask activationLayer;

    [Header("   События")]
    public SimpleEvent simpleEvent;
    public TimerEvent timerEvent;
    public SecondaryEvent secondaryEvent;

    private float qurentTime = 0;

    BoxCollider2D triggerBox2D;
    CircleCollider2D triggerCircle2D;
    BoxCollider triggerBox3D;
    SphereCollider triggerCircle3D;

    [System.Serializable]
    public class TimerEvent
    {
        public bool useTimer;
        public float timer;
        public UnityEvent timerEvent;
    }
    [System.Serializable]
    public class SimpleEvent
    {
        public UnityEvent enterEvent;
        public UnityEvent steyEvent;
        public UnityEvent exitEvent;
    }
    [System.Serializable]
    public class SecondaryEvent
    {
        public UnityEvent onDisableEvent;
        public UnityEvent onEnableEvent;
        public UnityEvent onDestroyEvent;
    }
    [System.Serializable]
    public class GizmoOptions
    {
        public bool drawGizmos = true;
        public Color color;
        public float alpha;
    }

    public enum TriggerShape
    {
        sphere,
        box,
    }

    public enum TypePhisic
    {
        phis2D,
        phis3D,
    }

//------------------------------------------------------------------------------------
    //
    private void Start() 
    {
        transform.localScale = Vector3.one;
        switch (usingPhisic)
        {
            case TypePhisic.phis2D:
                switch (shape)
                {
                    case TriggerShape.sphere:
                        trigger2D = (CircleCollider2D)gameObject.AddComponent(typeof(CircleCollider2D));
                        trigger2D.radius = radiuse;
                        trigger2D.isTrigger = true;
                    break;
                    case TriggerShape.box:
                    triggerBox2D = (BoxCollider2D)gameObject.AddComponent(typeof(BoxCollider2D));
                    triggerBox2D.size = langth;
                        triggerBox2D.isTrigger = true;
                    break;
                }
            break;
            case TypePhisic.phis3D:
                switch (shape)
                {
                    case TriggerShape.sphere:
                        triggerCircle3D = (SphereCollider)gameObject.AddComponent(typeof(SphereCollider));
                        triggerCircle3D.radius = radiuse;
                        triggerCircle3D.isTrigger = true;
                    break;
                    case TriggerShape.box:
                        triggerBox3D = (BoxCollider)gameObject.AddComponent(typeof(BoxCollider));
                        triggerBox3D.size = langth;
                        triggerBox3D.isTrigger = true;
                    break;
                }
            break;
        }
    }
    //
    private void Update() 
    {
        if (activated == true && timerEvent.useTimer) 
        {
            if (TimerOut())
            {
                timerEvent.timerEvent.Invoke();
                qurentTime = 0;
            }
        }
    }
    //
    private void OnValidate()
    {
        if (radiuse < 0) radiuse = 0;
        transform.localScale = Vector3.one;

    }
    //
    private void OnEnable()
    {
        secondaryEvent.onEnableEvent.Invoke();
    }
    //
    private void OnDisable()
    {
        secondaryEvent.onDisableEvent.Invoke();
    }
    //
    private void OnDestroy()
    {
        secondaryEvent.onDestroyEvent.Invoke();
    }

//---------------------------------------2Д физика------------------------------------
    // Событие при входе в триггер
    void OnTriggerEnter2D(Collider2D other)
    {
        if (activated && (other.gameObject.tag == activationTag || ((1<<other.gameObject.layer) & activationLayer) != 0))
        {
            simpleEvent.enterEvent.Invoke();
            if(!repeatable)
                activated = !activated;
        }
    }
    // Событие при нахождение в триггере
    void OnTriggerStay2D(Collider2D other)
    {
        if (activated && (other.gameObject.tag == activationTag || ((1 << other.gameObject.layer) & activationLayer) != 0))
        {
            simpleEvent.steyEvent.Invoke();
            if (!repeatable)
                activated = !activated;
        }
    }
    // Событие при выходе из триггера
    void OnTriggerExit2D (Collider2D other)
    {
        if (activated && (other.gameObject.tag == activationTag || ((1 << other.gameObject.layer) & activationLayer) != 0))
        {
            simpleEvent.exitEvent.Invoke();
            if(!repeatable)
                activated = !activated;
        }
    }

//---------------------------------------3Д физика------------------------------------
    // Событие при входе в триггер
    void OnTriggerEnter(Collider other)
   {
       if (activated && (other.gameObject.tag == activationTag || ((1 << other.gameObject.layer) & activationLayer) != 0))
       {
           simpleEvent.enterEvent.Invoke();
           if (!repeatable)
               activated = !activated;
       }
   }
    // Событие при нахождение в триггере
    void OnTriggerStay(Collider other)
   {
       if (activated && (other.gameObject.tag == activationTag || ((1 << other.gameObject.layer) & activationLayer) != 0))
       {
           simpleEvent.steyEvent.Invoke();
           if (!repeatable)
               activated = !activated;
       }
   }
    // Событие при выходе из триггера
    void OnTriggerExit(Collider other)
   {
       if (activated && (other.gameObject.tag == activationTag || ((1 << other.gameObject.layer) & activationLayer) != 0))
       {
           simpleEvent.exitEvent.Invoke();
           if (!repeatable)
               activated = !activated;
       }
   }

//--------------------------------------Отладка--------------------------------------	
#if UNITY_EDITOR
    private void OnDrawGizmos() 
    {

       if (Application.isEditor && !Application.isPlaying)
       Custom.Utility.NGizmos.DrawString(text, transform.position, Color.red, Color.black);

       // Рисуем зависимости 
       DrawHelperLines(simpleEvent.enterEvent);
       DrawHelperLines(simpleEvent.steyEvent);
       DrawHelperLines(simpleEvent.exitEvent);

       Color c = new Color(0, 1, 0, 0.2f);  // green
       Color c2 = new Color(0, 1, 0, 0.7f); // green
       switch (shape)
       {
           case TriggerShape.sphere:
               Gizmos.matrix = Matrix4x4.TRS(transform.position, transform.rotation, transform.lossyScale);
               Custom.Utility.NGizmos.DrawSphere(Vector3.zero, radiuse, c);
               Custom.Utility.NGizmos.DrawWireSphere(Vector3.zero, radiuse, c2);

               break;
           case TriggerShape.box:
               Gizmos.matrix = Matrix4x4.TRS(transform.position, transform.rotation, transform.lossyScale);
               Custom.Utility.NGizmos.DrawCube(Vector3.zero, langth, c);
               Custom.Utility.NGizmos.DrawWireCube(Vector3.zero, langth, c2);
               break;
       }
    }
    private void DrawHelperLines(UnityEvent unityEvent)
    {
        Color colorDep = Color.white;
        for (int i = 0; i < unityEvent.GetPersistentEventCount(); i++)
        {
            GameObject go = unityEvent.GetPersistentTarget(i) as GameObject; // Получить целевой компонент слушателя по индексу индекса.
            string method = unityEvent.GetPersistentMethodName(i);           // Получить имя целевого метода слушателя на индекс индекс

            if (go != null)
            {
                Custom.Utility.NGizmos.DrawDottedLine(transform.position, go.transform.position, colorDep);
                Custom.Utility.NGizmos.DrawCube(transform.position, Vector3.one * 0.08f, colorDep);
                Custom.Utility.NGizmos.DrawCube(go.transform.position, Vector3.one * 0.08f, colorDep);
            }
        }
    }
#endif

    private bool TimerOut()
    {
        if (qurentTime < timerEvent.timer)
        {       
            qurentTime += Time.deltaTime;
            return false;
        }
        else
            {
                return true;
            }
    }
//--------------------------------------------------------------------------------------
    public void TriggerActive(bool arg)
    {
        activated = arg;
    }

    public void DestroySelf() 
    {
        DestroyOther(gameObject);
    }
    public void DestroySelfTime(float time)
    {
        Destroy(gameObject, time);
    }
    public void DestroyOther(GameObject gObject) 
    {
        Destroy(gObject);
    }
    public void TriggerDebugLog(string str)
    {
        Debug.Log(str);
    }

}
