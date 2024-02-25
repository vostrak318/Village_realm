using System.Collections.Generic;
using UnityEngine;

public class CraftingManager : MonoBehaviour
{
    public Transform recipesContainer;
    public GameObject buttonPrefab;
    public CraftingSystem craftingSystem;

    /// <summary>
    /// Vytvoøí buttony pro craftable itemy
    /// </summary>
    /// <param name="craftableRecipe"> List receptù, které lze vycraftit </param>
    public void CreateButtons(List<Recipes> craftableRecipes) 
    {
        // Zapni nabídku, pokud ještì zaplá není
        if (recipesContainer.gameObject.activeInHierarchy == false)
            recipesContainer.gameObject.SetActive(true);

        // Pro kadého potomka - button v containeru
        for (int i = recipesContainer.childCount - 1; i > -1; i--)
        {
            // Pokud ten recept u nemùeme craftit, sma button
            if (!craftableRecipes.Contains(recipesContainer.GetChild(i).GetComponent<CraftingButton>().craftedRecipes))
                Destroy(recipesContainer.GetChild(i).gameObject);
        }

        // Pro kadı craftable recept
        foreach (Recipes recipe in craftableRecipes)
            // Pokud ten recept mùeme craftit nebo ještì ádnı button neexistuje, pøidej ho
            if (!IsRecipeInstantiated(recipe))
                Instantiate(buttonPrefab, recipesContainer).GetComponent<CraftingButton>().AssignedRecipe(recipe, craftingSystem);
    }

    /// <summary>
    /// Zjišuje, jestli je recept u zobrazen nebo ne
    /// </summary>
    /// <param name="recipe"> Recept, kterı se hledá </param>
    /// <returns> Boolean, zda-li je ji recept zobrazenı nebo ne </returns>
    bool IsRecipeInstantiated(Recipes recipe)
    {
        for (int i = 0; i < recipesContainer.childCount; i++)
        {
            // U existuje
            if (recipesContainer.GetChild(i).GetComponent<CraftingButton>().craftedRecipes == recipe)
                return true;
        }
        // Ještì neexistuje
        return false;
    }
}
