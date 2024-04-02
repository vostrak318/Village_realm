using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    public Item Item;
    public GameObject itemObject;

    void Pickup()
    {
        if (InventoryManager.Instance.inventory.Items.Count < 12)
        {
            InventoryManager.Instance.Add(Item, itemObject);
            Destroy(gameObject);
        }
        else
        {
            Debug.Log("plnej inv");
        }
    }

    private void OnMouseDown()
    {
        Pickup();
    }
}
