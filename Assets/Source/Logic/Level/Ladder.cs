//
//
//

using UnityEngine;
using System.Collections;

[RequireComponent(typeof(BoxCollider))]
public class Ladder : MonoBehaviour 
{
    public float speed;

    private BoxCollider collider;
    private GameObject thisObject;
    private GameObject playerObject;


	// Use this for initialization
	void Start () 
    {
        thisObject = gameObject;
	}
	// Update is called once per frame
	void Update () 
    {
        if (playerObject != null)
        {
            playerObject.transform.Translate(transform.up * Input.GetAxis("Vertical") * Time.deltaTime * speed);
        }
	}
    void OnValidate() 
    {
        if (collider == null)
        {
            collider = GetComponent<BoxCollider>();
            collider.isTrigger = true;
        }
    }
    //
    void OnTriggerEnter(Collider col) 
    {
        playerObject = col.gameObject;
        playerObject.transform.parent = col.transform;
        playerObject.GetComponent<FirstPersonController>().enabled = false;
    }
    //
    void OnTriggerExit(Collider col) 
    {
        playerObject.GetComponent<FirstPersonController>().enabled = true; 
        playerObject.transform.parent = null;
        playerObject = null;
    }
}
