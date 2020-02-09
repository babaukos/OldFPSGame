using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteViewState : MonoBehaviour
{
    public bool automaticReturn;
    public int qurentSprite;
    public Sprite[] spritse;
    
    private SpriteRenderer sr;


	// Use this for initialization
	void Start () 
    {
		if (sr == null)
            sr = GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void Update () 
    {
        sr.sprite = spritse[qurentSprite];
	}
    void LateUpdate() 
    {
        if (automaticReturn)
        {
            SpriteBack();
        }
    }
    public void Sprite(int arg)
    {
        qurentSprite = arg;
    }
    public void OnPostRender()
    {
        if (qurentSprite < spritse.Length)
        {
            qurentSprite ++;
        }
    }
    public void SpriteBack()
    {
        if (qurentSprite > 0)
        {
            qurentSprite--;
        }
    }
}
