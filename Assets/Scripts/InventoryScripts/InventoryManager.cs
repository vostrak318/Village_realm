using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance;
    public List<Item> Items = new List<Item>();
    public List<GameObject> GameObjectsItems = new List<GameObject>();

    public Transform ItemContent;
    public GameObject InventoryItem;
    public GameObject Player;

    public Button RemoveItemButton;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        RemoveItemButton.onClick.AddListener(() => Remove(InventoryItem.GetComponent<Item>()));
    }

    public void Add(Item item, GameObject gameObjectItem)
    {
        Items.Add(item);
        GameObjectsItems.Add(gameObjectItem);
        ListItems();
    }

    public void Remove(Item item)
    {
        DropItem(item);
        Items.Remove(item);
        ListItems();
    }

    public void ListItems()
    {
        foreach (Transform child in ItemContent)
        Destroy(child.gameObject);

        foreach (var item in Items)
        {
            GameObject obj = Instantiate(InventoryItem, ItemContent);
            var itemName = obj.transform.Find("ItemName").GetComponent<TMP_Text>();
            var itemIcon = obj.transform.Find("ItemIcon").GetComponent<Image>();
            var removeButton = obj.transform.Find("RemoveItem").GetComponent<Button>();

            removeButton.onClick.AddListener(() => Remove(item));

            itemName.text = item.itemName;
            itemIcon.sprite = item.icon;
        }
    }
    public void DropItem(Item item)
    {
        Instantiate(item.itemPrefab, Player.transform.position, Quaternion.identity);
    }
}
