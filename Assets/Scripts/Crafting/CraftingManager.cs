using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class CraftingManager : MonoBehaviour
{
    public Transform recipesContainer;
    public GameObject buttonPrefab;
    public CraftingSystem craftingSystem;

    public void CreateButtons(List<Recipes> craftableRecipe) 
    {
        if (recipesContainer.gameObject.activeInHierarchy == false)
            recipesContainer.gameObject.SetActive(true);

        for (int i = 0; i < recipesContainer.childCount; i++)
        {
            // Pokud ten recept u� nem��eme craftit, sma� button
            if (!craftableRecipe.Contains(recipesContainer.GetChild(0).GetComponent<CraftingButton>().craftedRecipes))
                Destroy(recipesContainer.GetChild(0).gameObject);
        }
        foreach (Recipes recipe in craftableRecipe)
            // Pokud ten recept m��eme craftit, p�idej ho
            if (recipesContainer.childCount == 0 || !craftableRecipe.Contains(recipesContainer.GetChild(0).GetComponent<CraftingButton>().craftedRecipes))
                Instantiate(buttonPrefab, recipesContainer).GetComponent<CraftingButton>().AssignedRecipe(recipe, craftingSystem);
    }
}
