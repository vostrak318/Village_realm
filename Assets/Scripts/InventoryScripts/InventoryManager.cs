using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class Inventory
{
    public List<Item> Items;
}
public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance;
    public Inventory inventory = new Inventory();

    public Transform ItemContent;
    public GameObject InventoryItem;
    public GameObject Player;

    public Button RemoveItemButton;

    
    public void SaveInventory()
    {
        string json = JsonUtility.ToJson(inventory);
        PlayerPrefs.SetString("inventory", json);
        PlayerPrefs.Save();
    }

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        RemoveItemButton.onClick.AddListener(() => Remove(InventoryItem.GetComponent<Item>()));
        LoadInventory();
    }

    public void Add(Item item, GameObject gameObjectItem)
    {
        inventory.Items.Add(item);
        ListItems();
    }

    public void Remove(Item item)
    {
        DropItem(item);
        inventory.Items.Remove(item);
        ListItems();
    }

    public void ListItems()
    {
        foreach (Transform child in ItemContent)
        Destroy(child.gameObject);

        foreach (var item in inventory.Items)
        {
            GameObject obj = Instantiate(InventoryItem, ItemContent);
            var itemName = obj.transform.Find("ItemName").GetComponent<TMP_Text>();
            var itemIcon = obj.transform.Find("ItemIcon").GetComponent<Image>();
            var removeButton = obj.transform.Find("RemoveItem").GetComponent<Button>();

            removeButton.onClick.AddListener(() => Remove(item));
            obj.GetComponent<Button>().onClick.AddListener(() => Build(item));

            if (itemName != null && itemIcon != null && removeButton != null)
            {
                itemName.text = item.itemName;
                itemIcon.sprite = item.icon;
            }
            else
                Debug.LogError("Mrdko, nìco ti chybí!");
        }
    }
    public void DropItem(Item item)
    {
        Instantiate(item.itemPrefab, Player.transform.position, Quaternion.identity);
    }

    public void LoadInventory()
    {
        string json = PlayerPrefs.GetString("inventory");
        if (!string.IsNullOrEmpty(json))
        {
            inventory = JsonUtility.FromJson<Inventory>(json);
            Debug.Log("Done Loading");
        }
    }

    public void Build(Item item)
    {
        if (item.isBuilding)
        {
            BuildingManager.Instance.ChangeBuildingMode(true);
            BuildingManager.Instance.SetBuilding(item.itemPrefab);
            //Remove(item);
            UIManager.Instance.CloseInventory();
        }
    }
}
