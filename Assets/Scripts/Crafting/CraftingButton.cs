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
    /// Pøiøadí informace danému tlaèítku
    /// </summary>
    /// <param name="recipe"> Recept, který craftí </param>
    /// <param name="craftingSystem"> Reference na CraftingSystem</param>
    public void AssignedRecipe(Recipes recipe, CraftingSystem craftingSystem)
    {
        craftedRecipes = recipe;
        this.craftingSystem = craftingSystem;
        Icon.sprite = recipe.ItemIcon;
        Name.text = recipe.name;
    }

    /// <summary>
    /// Vytvoøí item
    /// </summary>
    public void CraftItem()
    {
        craftingSystem.CreateItem(craftedRecipes);
    }
}
