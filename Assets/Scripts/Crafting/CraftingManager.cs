using System.Collections.Generic;
using UnityEngine;

public class CraftingManager : MonoBehaviour
{
    public Transform recipesContainer;
    public GameObject buttonPrefab;
    public CraftingSystem craftingSystem;

    /// <summary>
    /// Vytvo�� buttony pro craftable itemy
    /// </summary>
    /// <param name="craftableRecipe"> List recept�, kter� lze vycraftit </param>
    public void CreateButtons(List<Recipes> craftableRecipes) 
    {
        // Zapni nab�dku, pokud je�t� zapl� nen�
        if (recipesContainer.gameObject.activeInHierarchy == false)
            recipesContainer.gameObject.SetActive(true);

        // Pro ka�d�ho potomka - button v containeru
        for (int i = recipesContainer.childCount - 1; i > -1; i--)
        {
            // Pokud ten recept u� nem��eme craftit, sma� button
            if (!craftableRecipes.Contains(recipesContainer.GetChild(i).GetComponent<CraftingButton>().craftedRecipes))
                Destroy(recipesContainer.GetChild(i).gameObject);
        }

        // Pro ka�d� craftable recept
        foreach (Recipes recipe in craftableRecipes)
            // Pokud ten recept m��eme craftit nebo je�t� ��dn� button neexistuje, p�idej ho
            if (!IsRecipeInstantiated(recipe))
                Instantiate(buttonPrefab, recipesContainer).GetComponent<CraftingButton>().AssignedRecipe(recipe, craftingSystem);
    }

    /// <summary>
    /// Zji��uje, jestli je recept u� zobrazen nebo ne
    /// </summary>
    /// <param name="recipe"> Recept, kter� se hled� </param>
    /// <returns> Boolean, zda-li je ji� recept zobrazen� nebo ne </returns>
    bool IsRecipeInstantiated(Recipes recipe)
    {
        for (int i = 0; i < recipesContainer.childCount; i++)
        {
            // U� existuje
            if (recipesContainer.GetChild(i).GetComponent<CraftingButton>().craftedRecipes == recipe)
                return true;
        }
        // Je�t� neexistuje
        return false;
    }
}
