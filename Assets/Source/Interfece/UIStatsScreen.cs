using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIStatsScreen : MonoBehaviour 
{
    public Text HealthText;
    public Text ArmorText;
    public Text AmmunitionText;

    private static UIStatsScreen _instance;
    public static UIStatsScreen Instance
    {
        get { return _instance; }
    }

    //
    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
        else if (_instance != this)
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(this);
    }

	// Use this for initialization
    private void Start() 
    {
		
	}
	
	// Update is called once per frame
	void Update () 
    {
		
	}
}
