//
//
//

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIFlashScreen : MonoBehaviour
{
    public Color positive = Color.green;
    public Color negative = Color.red;

    Image flashScreen;

    private static UIFlashScreen _instance;
    public static UIFlashScreen Instance
    {
        get { return _instance; }
    }

    private void Start()
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

        flashScreen = GetComponent<Image>();
        Color c = flashScreen.color;
        c.a = 0;
        flashScreen.color = c;
    }

    private void Update()
    {
        if (flashScreen.color.a > 0)
        {
            Color invisible = new Color(flashScreen.color.r, flashScreen.color.g, flashScreen.color.b, 0);
            flashScreen.color = Color.Lerp(flashScreen.color, invisible, 5 * Time.deltaTime);
        }
    }

    public void TookNegativeScreen()
    {
        flashScreen.color = negative; // new Color(1, 0, 0, 0.8f);
    }
    public void TookPositiveScreen()
    {
        flashScreen.color = positive; // new Color(1, 0, 0, 0.8f);
    }
}