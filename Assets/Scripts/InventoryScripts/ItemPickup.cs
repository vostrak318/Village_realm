using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemPickup : MonoBehaviour
{
    public Item Item;
    public GameObject itemObject;
    public Image loadingWheel;
    private bool isHoldingClick = false;

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
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            isHoldingClick = true;
            StartCoroutine(ShowLoadingWheel());
        }
        else if (Input.GetMouseButtonUp(0))
        {
            isHoldingClick = false;
            loadingWheel.fillAmount = 0;
        }
    }
    private IEnumerator ShowLoadingWheel()
    {
        float holdTime = 0;

        while (isHoldingClick && holdTime < 1.5f)
        {
            holdTime += Time.deltaTime;
            loadingWheel.fillAmount = holdTime / 1.5f;
            yield return null;
        }

        if (holdTime >= 1.5f)
        {
            Pickup();
        }
    }
}
