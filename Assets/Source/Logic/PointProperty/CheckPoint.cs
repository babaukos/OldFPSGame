using System.Collections;
using UnityEngine;

public class CheckPoint : MonoBehaviour 
{
    public int numberChackpoint;

    //--------------------------------------Отладка--------------------------------------	
 #if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        //var c1 = new Color(1, 0, 0, 1f);          // red
        //var c2 = new Color(1, 0.92f, 0.016f, 0.6f); // yelow

        //Custom.Utility.NGizmos.DrawCube(transform.position, new Vector3(0.2f, 0.2f, 0.2f), c1);
        //Custom.Utility.NGizmos.DrawCube(transform.position, new Vector3(1f, 1.9f, 1f), c2);
    }
#endif
    //----------------------------------------API-----------------------------------------
    // Конец игры с победой
    public void SavePlayerData()
    {
        if (GameManager.Instance != null)
        {
           // GameManager.Instance.SaveData();
        }
    }
    // Конец игры с проигришем
    public void LoadPlayerData()
    {
        if (GameManager.Instance != null)
        {
           // GameManager.Instance.LoadData();
        }
    }
}
