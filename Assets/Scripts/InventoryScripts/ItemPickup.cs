using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ItemPickup : MonoBehaviour
{
    public Item Item;
    public GameObject itemObject;
    public Image loadingWheel;
    float holdTime = 0;
    public float maxHoldTime = 0.7f;

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
    private void ShowLoadingWheel()
    {
        holdTime += Time.deltaTime;

        if (loadingWheel != null)
            loadingWheel.fillAmount = holdTime / maxHoldTime;

        if (holdTime >= maxHoldTime)
        {
            Pickup();
        }
    }
    private void OnMouseOver()
    {
        if (Input.GetMouseButton(0))
        {
            ShowLoadingWheel();
        }
        else
        {
            holdTime = 0;

            if (loadingWheel != null)
                loadingWheel.fillAmount = 0;
        }
        if (Input.GetMouseButton(1))
        {
            if (Item.itemName.Contains("Cooked"))
            {
                GameManager.instance.player.AddHunger();
                Destroy(gameObject);
            }
            else if (Item.itemName.Contains("Health"))
            {
                GameManager.instance.player.AddHP();
                Destroy(gameObject);
            }
            else if (Item.itemName.Contains("age"))
            {
                GameManager.instance.player.StopTheAge();
                Destroy(gameObject);
            }
            else if (Item.itemName.Contains("Speed"))
            {
                GameManager.instance.player.AddSpeed();
                Destroy(gameObject);
            }
            else if (Item.itemName.Contains("Sword"))
            {
                GameManager.instance.player.AddDmg();
                Destroy(gameObject);
            }
            else if (Item.itemName.Contains("Armor"))
            {
                GameManager.instance.player.AddMaxHP();
                Destroy(gameObject);
            }
        }
    }
    private void OnMouseExit()
    {
        holdTime = 0;

        if (loadingWheel != null)
            loadingWheel.fillAmount = 0;
    }
}
