/////////////////////////////////////////////////////
//      Дает возможность переключать камеры        //
//          Словно на студии                       //
//                                                 //
/////////////////////////////////////////////////////

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("Camera/Manager/Camera Manager TV")]
public class CameraManagerTV : MonoBehaviour 
{
    [SerializeField]
    private int qurrentCamera;
    [SerializeField]
    private Camera[] camersPool;

    private KeyCode[] keyCodesAlph = 
    {
         KeyCode.Alpha1,
         KeyCode.Alpha2,
         KeyCode.Alpha3,
         KeyCode.Alpha4,
         KeyCode.Alpha5,
         KeyCode.Alpha6,
         KeyCode.Alpha7,
         KeyCode.Alpha8,
         KeyCode.Alpha9,
     };
    private KeyCode[] keyCodesKeyp = 
    {
         KeyCode.Keypad1,
         KeyCode.Keypad2,
         KeyCode.Keypad3,
         KeyCode.Keypad4,
         KeyCode.Keypad5,
         KeyCode.Keypad6,
         KeyCode.Keypad7,
         KeyCode.Keypad8,
         KeyCode.Keypad9,
     };
    public enum KeyType 
    {
      AlphaKey,
      KepadKey,
    }

	// Use this for initialization
	void Start ()
    {
        StatesCameras();
    }
    //
    void Inputs () 
    {
        if (Input.GetKeyDown(KeyCode.KeypadPlus)) 
        {
            if (qurrentCamera < camersPool.Length - 1)
            {
                qurrentCamera += 1;
            }
            else 
                {
                    qurrentCamera = 0;
                }
        }
        else
        if (Input.GetKeyDown(KeyCode.KeypadMinus))
        {
            if (qurrentCamera > 0)
            {
                qurrentCamera -= 1;
            }
            else
            {
                qurrentCamera = camersPool.Length - 1;
            }
        }

        for (int c = 0; c < camersPool.Length; c++)
        {
             for(int i = 0 ; i < keyCodesAlph.Length; i ++ )
             {
                 if (Input.GetKeyDown(keyCodesKeyp[i]))
                 {
                     int numberPressed = i + 1;
                     Debug.Log(numberPressed);
                     qurrentCamera = i;
                     return;
                 }
             }
         }
    }
	// Update is called once per frame
	void Update () 
    {
        Inputs();
        StatesCameras();
	}
    //
    void StatesCameras () 
    {
        qurrentCamera = Mathf.Clamp(qurrentCamera, 0, camersPool.Length - 1);
        for (int c = 0; c < camersPool.Length; c++)
        {
            if (c != qurrentCamera)
            {
                camersPool[c].gameObject.active = false;
            }
            else
            {
                camersPool[c].gameObject.active = true;
            }
        }
    }
    //
    public void SetActiveCamera(int arg) 
    {
        qurrentCamera = arg;
    }
}
