///////////////////////////////////////////////////////////////////////////////
//                            v 0.0.1                                        //
//       Скрипт интерактивной прозвонки обьектов на наличиее                 //
// действий и спец скриптов действия, используется рейкас из активной камері //
//     Вешать его нужно на родительский обьект одной или двух камер          //
//                                                                           //
///////////////////////////////////////////////////////////////////////////////

using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CameraInteractivity : MonoBehaviour 
{
    [Tooltip("Игнорирование колладеров с маской:")]
    public LayerMask layerMask;
    [Tooltip("Длинна луча")]
    public float rayLength = 1.0f;                    //
    [Header(" Использование курсора")]
    public bool useCursor;
    [Tooltip(" Литерал из Input")]
    public string showCursorKey;
    [Tooltip(" Литерал из Input")]
    public string interactiveKey;


    private RaycastHit hit;                           //
    private Vector3 rayPos;                           //
	private Ray ray;                                  //
    private RayDetector intObj;                      //

    //--------------Нажатие кнопок-------------------
    private void Inputs() 
    {
        if (interactiveKey != "")
        {
            if (Input.GetButtonDown(interactiveKey))
            {
                if (intObj != null)
                    intObj.SendMessage("Using");
             }
        }
        if (showCursorKey != "") 
        {
            if (Input.GetButtonDown(showCursorKey)) 
            { 
                useCursor = !useCursor;
            }
        }
    }
    //------Выполняется при инициализации------------
    private void Start()
	{

	}
    //-----Виполняется при каждом кадре--------------
    private void Update() 
	{
        Inputs();
        if (!useCursor)
        {
            Screen.lockCursor = true;
            Cursor.visible = false;
            rayPos = new Vector3(Screen.width / 2, Screen.height / 2, 0);
        }
        else
        {
            Screen.lockCursor = false;
            Cursor.visible = true;
            rayPos = Input.mousePosition;
        }
        ReyCasting();
	}
    //---------------Пускаем луч---------------------
    private void ReyCasting() 
    {
        ray = Camera.main.ScreenPointToRay(rayPos);                         // рекаст из центра активной камеры
        if (Physics.Raycast(ray, out hit, rayLength, layerMask))
        {
            if (intObj == null) 
            {
                intObj = hit.collider.GetComponent<RayDetector>();
            }
            else 
            {
                //intObj.Selection();
                intObj.SendMessage("Selection");
            }
        }
        else 
        {
            if (intObj != null)
            {
                //intObj.Deselection();
                intObj.SendMessage("Deselection");
            }
            intObj = null;
        }
    }
    //-------------------------------------------------Отладка---------------------------------------------
#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        if (hit.point != Vector3.zero)
        {
            Custom.Utility.NGizmos.DrawRay(ray.origin, ray.direction * rayLength, Color.yellow);
            Custom.Utility.NGizmos.DrawPoint(hit.point, 0.09f, Color.yellow, Custom.Utility.NGizmos.PointTipe.crist);
        }
    }
#endif
 }
