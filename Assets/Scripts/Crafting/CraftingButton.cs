using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftingButton : MonoBehaviour
{
    public Recipes craftedRecipes;
    public CraftingSystem craftingSystem;
    public void AssignedRecipe(Recipes recipe, CraftingSystem craftingSystem)
    {
        craftedRecipes = recipe;
        this.craftingSystem = craftingSystem;
    }
    public void CraftItem()
    {
        craftingSystem.CreateItem(craftedRecipes);
    }
}
