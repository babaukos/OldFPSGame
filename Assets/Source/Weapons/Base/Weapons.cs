using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapons : MonoBehaviour 
{
    public int id;

    public virtual int GetAmmoAmount()
    {
        return 0;
    }
    public virtual void SetAmmoAmount(int val)
    {
       
    }

    public virtual int GetAmmoClipSize()
    {
        return 0;
    }
    public virtual void SetAmmoClipSize(int val)
    {

    }
}
