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
    /// Vol�no v Update; vol� metodu pro tvorbu a zpracov�n� craftable buttons
    /// </summary>
    public void TryCraft()
    {
        // Tady byl p�edt�m if - po�et craftable recept� > 0; nepot�ebujeme, o�et�en� se d�je v metod� CreateButtons
        craftingManager.CreateButtons(craftableRecipes);
    }

    /// <summary>
    /// Zji��uje, jak� recepty lze vytvo�it - jak� recepty maj� dost surovin na zemi
    /// </summary>
    private void CheckCraftableRecipes() 
    {
        // Vy�isti List recept�, aby se nep�id�valy stejn� recepty opakovan�
        craftableRecipes.Clear();

        // Ka�d� recept, kter� m� na zemi dostate�n� po�et surovin p�idej do listu craftable recept�
        foreach (Recipes recipe in recipes)
        {
            if (HasItemsOnGround(recipe.requiredItems))
                craftableRecipes.Add(recipe);
        }
    }

    /// <summary>
    /// Vytvo�� item
    /// </summary>
    /// <param name="recipe">Recept, ze kter�ho se m� vytvo�it item</param>
    public void CreateItem(Recipes recipe)
    {
        // Vytvo� vycraft�n� item 
        Instantiate(recipe.craftedItemPrefab, transform.position, Quaternion.identity);

        // Sma� pou�it� suroviny
        RemoveItems(recipe.requiredItems);
    }
    /// <summary>
    /// Sma� itemy
    /// </summary>
    /// <param name="itemNames">List n�zv� item�, kter� se maj� smazat</param>
    private void RemoveItems(List<string> itemNames)
    {
        // Pro ka�d� item zjisti, jestli je na zemi a pot� ho sma� a zni�
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
    /// Zji��uje, zda-li jsou suroviny na zemi
    /// </summary>
    /// <param name="neededItems"> List n�zv� item�, kter� se maj� na zemi naj�t </param>
    /// <returns> Boolean, zda-li recept m� v�echny pot�ebn� suroviny na zemi </returns>
    private bool HasItemsOnGround(List<string> neededItems)
    {
        // Slovn�k pot�ebn�ch surovin
        Dictionary<string, int> inRecipeItems = GetRequiredItemsCounts(neededItems);

        // Zjisti, jestli kolem sebe m� dost surovin
        foreach (string requiredItemName in neededItems)
        {
            int requiredItemCount = groundItems.FindAll(item => item.Item.itemName == requiredItemName).Count;
            // Ne, nem�
            if (requiredItemCount < inRecipeItems[requiredItemName])
                return false;
        }
        // Ano, m�
        return true;
    }

    /// <summary>
    /// Kontroluje a z�sk�v� itemy kolem hr��e
    /// </summary>
    private void CheckRadiusForItems()
    {
        // Vy�isti list, aby se itemy duplicitn� nep�id�valy
        groundItems.Clear();

        // V�echny objekty kolem hr��e
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 0.5f);

        // Do listu p�idej jen itemy - objekty s komponentou `ItemPickup`
        foreach (Collider2D collider in colliders)
        {
            ItemPickup itemPickup;
            if (collider.TryGetComponent(out itemPickup))
                if (!groundItems.Contains(itemPickup))
                    groundItems.Add(itemPickup);
        }
    }

    /// <summary>
    /// Vr�t� slovn�k pot�ebn�ch item� a jejich odpov�daj�c�m po�t�m
    /// </summary>
    /// <param name="itemNames"> List n�zv� item� </param>
    /// <returns> List p�eveden� do slovn�ku </returns>
    Dictionary<string, int> GetRequiredItemsCounts(List<string> itemNames) 
    {
        // Nov� slovn�k
        Dictionary<string, int> items = new();

        // P�idej do slovn�k� a p�ipo�ti 1
        foreach (string itemName in itemNames)
            if (items.ContainsKey(itemName))
                items[itemName]++;
            else
                items[itemName] = 1;

        // Vra� slovn�k
        return items;
    }

    void Update()
    {
        CheckRadiusForItems();
        CheckCraftableRecipes();
        TryCraft();
    }
}