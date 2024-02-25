using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CraftingButton : MonoBehaviour
{
    public Recipes craftedRecipes;
    public CraftingSystem craftingSystem;

    public Image Icon;
    public TMP_Text Name;
    /// <summary>
    /// P�i�ad� informace dan�mu tla��tku
    /// </summary>
    /// <param name="recipe"> Recept, kter� craft� </param>
    /// <param name="craftingSystem"> Reference na CraftingSystem</param>
    public void AssignedRecipe(Recipes recipe, CraftingSystem craftingSystem)
    {
        craftedRecipes = recipe;
        this.craftingSystem = craftingSystem;
        Icon.sprite = recipe.ItemIcon;
        Name.text = recipe.name;
    }

    /// <summary>
    /// Vytvo�� item
    /// </summary>
    public void CraftItem()
    {
        craftingSystem.CreateItem(craftedRecipes);
    }
}
