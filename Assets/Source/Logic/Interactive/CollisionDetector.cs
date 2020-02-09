//
//
//

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class CollisionDetector : MonoBehaviour
{
#region Public Verible
    public bool pickupable = true;
    public Vector3 langth = Vector3.one;
#endregion
    [Space]
    public Screen flashScreen;
    #region System Verible
    public enum Screen 
    {
        noneEffect,
        positive,
        negative,
    }
    #endregion
    [Tooltip("Действие выполняется при обьекта с игроком")]
    public UnityEvent pickupEvent;
    [Tooltip("Действие выполняется при уничтожении обьекта")]
    public UnityEvent destroyEvent;

    #region Private Verible
    private BoxCollider collider;
    #endregion

    //------------------------------------------------------------------------------------
	// Use this for initialization
    private void Start() 
    {
        SetCollider();
	}
    //
    private void OnValidate() 
    {
       // SetCollider();
    }
    //
    private void SetCollider() 
    {
        collider = GetComponent<BoxCollider>();
        if (collider != null)
        {
            collider.isTrigger = true;
            collider.size = langth;
        }
        else 
            {
                collider = gameObject.AddComponent<BoxCollider>();
                collider.isTrigger = true;
                collider.size = langth;
            }
    }
//---------------------------------------2Д физика------------------------------------
    // Событие при входе в триггер
    private void OnTriggerEnter(Collider other)
    {
        if (pickupable)
        {
            pickupEvent.Invoke();
            if (flashScreen == Screen.positive)
            {
                if(UIFlashScreen.Instance != null)
                UIFlashScreen.Instance.TookPositiveScreen();
            }
            else if (flashScreen == Screen.negative)
            {
                if (UIFlashScreen.Instance != null)
                UIFlashScreen.Instance.TookNegativeScreen();
            }
        }
    }
//---------------------------------------Отладка--------------------------------------
    private void OnDestroy()
    {
        destroyEvent.Invoke();
    }
    public void DebugText(string txt)
    {
        Debug.Log(txt);
    }
    //
#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        Color c = new Color32(0, 191, 255, 80);  // blue
        Color c2 = new Color(0, 0.8f, 1f, 0.8f); // blue
        Gizmos.matrix = Matrix4x4.TRS(transform.position, transform.rotation, transform.lossyScale);
        Custom.Utility.NGizmos.DrawCube(Vector3.zero, langth, c);
        Custom.Utility.NGizmos.DrawWireCube(Vector3.zero, langth, c2);
    }
#endif
//-----------------------------------------API----------------------------------------
    //
    public void PickingStey(bool arg)
    {
        pickupable = arg;
    }
    //
    public void CreateEffect(ParticleSystem ps) 
    {
        Instantiate(ps, transform.position, transform.rotation);
    }
    public void GameLog(string str) 
    {
        if (UILog.Instance != null) 
        {
            UILog.Instance.AddMassege(str);
        }
    }
    public void PlaySoundInPlayer(AudioClip ac) 
    {
        FindObjectOfType<PlayerAudioController>().GetAudiosourse().PlayOneShot(ac);
    }
}
