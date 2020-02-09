//
//
//

using System.Collections;
using UnityEngine.UI;
using UnityEngine;


public class UIDeathScreen : MonoBehaviour 
{
    public float speed;

    public Image deathScreenBg;
    public Text deathText;
    public Button[] buttons;

    private void OnEnable()
    {
        foreach (Button button in buttons)
        {
            button.GetComponentInChildren<Text>().canvasRenderer.SetAlpha(0.0f);
        }
        deathScreenBg.material.SetFloat("_Level", 1.0f);
        deathText.canvasRenderer.SetAlpha(0.0f);
        StartCoroutine(FadeScreen());
        StartCoroutine(FadeText());
    }

    private void OnDisable() 
    {
      
    }

    IEnumerator FadeText() 
    {
        while (deathScreenBg.material.GetFloat("_Level") > 0)
        {
            deathText.CrossFadeAlpha(1.0f, 1.0f, false);
            foreach (Button button in buttons)
            {
                button.GetComponentInChildren<Text>().CrossFadeAlpha(1.0f, 1.0f, false);
            }
            yield return null;
        }
    }

    IEnumerator FadeScreen()
    {
        float t = 1f;
        while (t > 0)
        {
            t -= Time.deltaTime * speed;
            deathScreenBg.material.SetFloat("_Level", t);
            yield return null;
        }
    }
}
