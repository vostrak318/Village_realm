using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class CraftingSystem : MonoBehaviour
{
    private List<ItemPickup> groundItems = new();
    public List<Recipes> recipes = new();
    public List<Recipes> unlockedRecipes = new();
    private List<Recipes> craftableRecipes = new();
    Player player;

    public CraftingManager craftingManager;

    public void TryCraft()
    {
        craftingManager.CreateButtons(craftableRecipes);
    }

    private void CheckCraftableRecipes() 
    {
        craftableRecipes.Clear();

        foreach (Recipes recipe in unlockedRecipes)
        {
            if (HasItemsOnGround(recipe.requiredItems))
                craftableRecipes.Add(recipe);
        }
    }

    public void CreateItem(Recipes recipe)
    {
        Instantiate(recipe.craftedItemPrefab, transform.position, Quaternion.identity);

        RemoveItems(recipe.requiredItems);
    }
    private void RemoveItems(List<string> itemNames)
    {
        foreach (string itemName in itemNames)
        {
            ItemPickup itemToRemove = groundItems.Find(item => item.Item.itemName == itemName);
            if (itemToRemove != null)
            {
                groundItems.Remove(itemToRemove);
                Destroy(itemToRemove.gameObject);
            }
        }
    }
    private bool HasItemsOnGround(List<string> neededItems)
    {
        Dictionary<string, int> inRecipeItems = GetRequiredItemsCounts(neededItems);

        foreach (string requiredItemName in neededItems)
        {
            int requiredItemCount = groundItems.FindAll(item => item.Item.itemName == requiredItemName).Count;
            if (requiredItemCount < inRecipeItems[requiredItemName])
                return false;
        }
        return true;
    }

    private void CheckRadiusForItems()
    {
        groundItems.Clear();

        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 0.5f);

        foreach (Collider2D collider in colliders)
        {
            ItemPickup itemPickup;
            if (collider.TryGetComponent(out itemPickup))
                if (!groundItems.Contains(itemPickup))
                    groundItems.Add(itemPickup);
        }
    }

    Dictionary<string, int> GetRequiredItemsCounts(List<string> itemNames) 
    {
        Dictionary<string, int> items = new();

        foreach (string itemName in itemNames)
            if (items.ContainsKey(itemName))
                items[itemName]++;
            else
                items[itemName] = 1;

        return items;
    }

    void CheckAge()
    {
        unlockedRecipes.Clear();
        foreach (Recipes recipe in recipes)
        {
            if (player.currentAge >= recipe.requiredAge && !unlockedRecipes.Contains(recipe))
            {
                unlockedRecipes.Add(recipe);
            }
        }
    }

    void Update()
    {
        CheckAge();
        CheckRadiusForItems();
        CheckCraftableRecipes();
        TryCraft();
    }
}