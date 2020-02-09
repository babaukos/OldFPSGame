using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecretPoint : MonoBehaviour 
{

	// Use this for initialization
	void Start () 
    {
		
	}
	
    public void SecretFound()
    {
        if (GameManager.Instance != null)
            GameManager.Instance.statistic.secrets += 1;
    }

    public void GameLog(string str)
    {
        if (UILog.Instance != null)
            UILog.Instance.AddMassege(str);
    }
}
