using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CraftingSystem : MonoBehaviour
{
    private List<ItemPickup> groundItems = new();
    public List<Recipes> recipes = new();

    public void TryCraft()
    {
        foreach (Recipes recipe in recipes)
        {
            if (HasItemsOnGround(recipe.requiredItems))
            {
                CreateItem(recipe);
                RemoveItems(recipe.requiredItems);
            }
        }
    }
    private void CreateItem(Recipes recipe)
    {
        Vector2 spawnPosition1 = new Vector2(transform.position.x, transform.position.y);
        Instantiate(recipe.craftedItemPrefab, spawnPosition1, Quaternion.identity);
        Debug.Log("Vytvoøen item: " + recipe.recipeName);
    }
    private void RemoveItems(List<string> itemNames)
    {
        foreach (string itemName in itemNames)
        {
            ItemPickup itemToRemove = groundItems.Find(item => item.Item.itemName == itemName);
            Destroy(itemToRemove.gameObject);
            groundItems.Remove(itemToRemove);
        }
    }
    private bool HasItemsOnGround(List<string> neededItems)
    {
        Dictionary<string, int> inRecipeItems = GetRequiredItemsCounts(neededItems);
        foreach (string requiredItemName in neededItems)
        {
            int requiredItemCount = groundItems.FindAll(item => item.Item.itemName == requiredItemName).Count;
            if (requiredItemCount < inRecipeItems[requiredItemName])
            {
                return false;
            }
        }
        return true;
    }
    private void CheckRadiusForItems()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 0.5f);

        foreach (Collider2D collider in colliders)
        {
            ItemPickup itemPickup;
            if (collider.TryGetComponent<ItemPickup>(out itemPickup))
            {
                if (!groundItems.Contains(itemPickup))
                {
                    groundItems.Add(itemPickup);
                }
            }
        }
    }
    Dictionary<string, int> GetRequiredItemsCounts(List<string> itemNames) 
    {
        Dictionary<string, int> items = new();
        foreach (string itemName in itemNames) 
        {
            if (items.ContainsKey(itemName))
            {
                items[itemName]++;
            }
            else
            {
                items[itemName] = 1;
            }
        }
        return items;
    }    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            TryCraft();
        }
        CheckRadiusForItems();
    }

}