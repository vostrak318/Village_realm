using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class CraftingSystem : MonoBehaviour
{
    private List<ItemPickup> groundItems = new();
    public List<Recipes> recipes = new();
    private List<Recipes> craftableRecipes = new();

    public CraftingManager craftingManager;

    /// <summary>
    /// Voláno v Update; volá metodu pro tvorbu a zpracování craftable buttons
    /// </summary>
    public void TryCraft()
    {
        // Tady byl pøedtím if - poèet craftable receptù > 0; nepotøebujeme, ošetøení se dìje v metodì CreateButtons
        craftingManager.CreateButtons(craftableRecipes);
    }

    /// <summary>
    /// Zjišuje, jaké recepty lze vytvoøit - jaké recepty mají dost surovin na zemi
    /// </summary>
    private void CheckCraftableRecipes() 
    {
        // Vyèisti List receptù, aby se nepøidávaly stejné recepty opakovanì
        craftableRecipes.Clear();

        // Kadı recept, kterı má na zemi dostateènı poèet surovin pøidej do listu craftable receptù
        foreach (Recipes recipe in recipes)
        {
            if (HasItemsOnGround(recipe.requiredItems))
                craftableRecipes.Add(recipe);
        }
    }

    /// <summary>
    /// Vytvoøí item
    /// </summary>
    /// <param name="recipe">Recept, ze kterého se má vytvoøit item</param>
    public void CreateItem(Recipes recipe)
    {
        // Vytvoø vycraftìnı item 
        Instantiate(recipe.craftedItemPrefab, transform.position, Quaternion.identity);

        // Sma pouité suroviny
        RemoveItems(recipe.requiredItems);
    }
    /// <summary>
    /// Sma itemy
    /// </summary>
    /// <param name="itemNames">List názvù itemù, které se mají smazat</param>
    private void RemoveItems(List<string> itemNames)
    {
        // Pro kadı item zjisti, jestli je na zemi a poté ho sma a zniè
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
    /// <summary>
    /// Zjišuje, zda-li jsou suroviny na zemi
    /// </summary>
    /// <param name="neededItems"> List názvù itemù, které se mají na zemi najít </param>
    /// <returns> Boolean, zda-li recept má všechny potøebné suroviny na zemi </returns>
    private bool HasItemsOnGround(List<string> neededItems)
    {
        // Slovník potøebnıch surovin
        Dictionary<string, int> inRecipeItems = GetRequiredItemsCounts(neededItems);

        // Zjisti, jestli kolem sebe máš dost surovin
        foreach (string requiredItemName in neededItems)
        {
            int requiredItemCount = groundItems.FindAll(item => item.Item.itemName == requiredItemName).Count;
            // Ne, nemáš
            if (requiredItemCount < inRecipeItems[requiredItemName])
                return false;
        }
        // Ano, máš
        return true;
    }

    /// <summary>
    /// Kontroluje a získává itemy kolem hráèe
    /// </summary>
    private void CheckRadiusForItems()
    {
        // Vyèisti list, aby se itemy duplicitnì nepøidávaly
        groundItems.Clear();

        // Všechny objekty kolem hráèe
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 0.5f);

        // Do listu pøidej jen itemy - objekty s komponentou `ItemPickup`
        foreach (Collider2D collider in colliders)
        {
            ItemPickup itemPickup;
            if (collider.TryGetComponent(out itemPickup))
                if (!groundItems.Contains(itemPickup))
                    groundItems.Add(itemPickup);
        }
    }

    /// <summary>
    /// Vrátí slovník potøebnıch itemù a jejich odpovídajícím poètùm
    /// </summary>
    /// <param name="itemNames"> List názvù itemù </param>
    /// <returns> List pøevedenı do slovníku </returns>
    Dictionary<string, int> GetRequiredItemsCounts(List<string> itemNames) 
    {
        // Novı slovník
        Dictionary<string, int> items = new();

        // Pøidej do slovníkù a pøipoèti 1
        foreach (string itemName in itemNames)
            if (items.ContainsKey(itemName))
                items[itemName]++;
            else
                items[itemName] = 1;

        // Vra slovník
        return items;
    }

    void Update()
    {
        CheckRadiusForItems();
        CheckCraftableRecipes();
        TryCraft();
    }
}