using System.Collections;
using UnityEngine;

public class EndPoint : MonoBehaviour 
{
     
    //-------------------------------------------------Отладка---------------------------------------------
#if UNITY_EDITOR
    private void  OnDrawGizmos()
    {
        var c1 = new Color(0, 0, 1, 1f);          // blue
        Custom.Utility.NGizmos.DrawCube(transform.position, new Vector3(0.6f, 2.0f, 0.6f), c1);
    }
#endif
    // Конец игры с победой
    public void GameEnd() 
    {
        if (GameManager.Instance != null) 
        {
            GameManager.Instance.GameEnd();
        }
        else 
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#endif         
        }
    }
    // Конец игры с проигришем
    public void GameOver() 
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.GameOver();
        }
        else
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#endif
        }

    }
}
