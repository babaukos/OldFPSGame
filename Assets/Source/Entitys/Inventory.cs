using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour 
{
    public GameObject[] inventoryItems;
    private GameObject[] itemsPul;
    private Transform InvContainer;

	// Use this for initialization
	void Start () 
    {
        itemsPul = new GameObject[inventoryItems.Length];
        if (InvContainer == null) 
        {
            InvContainer = transform.Find("InvContainer");
            if (InvContainer == null) 
            {
                InvContainer = new GameObject("InvContainer").transform;
                InvContainer.parent = transform;
                InvContainer.position = transform.position;

 
                Compare();
            }
            else 
            {
                Compare();
            }
        }
        else 
        {
            Compare();
        }
	}
	// Update is called once per frame
	void Update () 
    {
		
	}
    //
    void Compare() 
    {
        for (int i = 0; i < inventoryItems.Length; i++)
        {
            if (inventoryItems[i] != itemsPul[i])
            {
                ItemAddPull(Instantiate(inventoryItems[i], InvContainer), i);
            }
        }
    }
    //
    //void ItemAdd(GameObject it) 
    //{
    //    for (int i = 0; i < items.Length; i++)
    //    {
    //        if (items[i] == null)
    //        {
    //            ItemAddPull(it, i);
    //        }
    //    }
    //}
    void ItemAddPull(GameObject it, int pos)
    {
        itemsPul[pos] = it;
        itemsPul[pos].transform.parent = InvContainer;
        itemsPul[pos].transform.position = InvContainer.position;
        itemsPul[pos].SetActive(false);
    }
    //
    public void ItemPickUp() 
    {
    
    }
    // Выкинуть предмет под номером
    public void ItemDrop(int id)
    {
        if (itemsPul[id] != null && inventoryItems[id] != null)
        {
            inventoryItems[id].transform.position = transform.position + transform.up * 1.2f;
            itemsPul[id].transform.position = transform.position + transform.up * 1.2f;

            inventoryItems[id].transform.parent = null;
            itemsPul[id].transform.parent = null;

            inventoryItems[id].SetActive(true);
            itemsPul[id].SetActive(true);
            //itemsPul[id].gameObject.SendMessage("AddForce", transform.up * 0.3f);

            inventoryItems[id] = null;
            itemsPul[id] = null;

        }
    }
    // Выкинуть все предметы
    public void ItemsDropAll() 
    {
        for (int i = 0; i < itemsPul.Length; i++)
        {
            ItemDrop(i);
        }
    }

}
