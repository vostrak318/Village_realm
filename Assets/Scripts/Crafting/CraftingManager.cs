using System;
using System.Collections.Generic;
using UnityEngine;

public class CraftingManager : MonoBehaviour
{
    public Transform recipesContainer;
    public GameObject buttonPrefab;
    public CraftingSystem craftingSystem;

    private static CraftingManager instance;
    public static CraftingManager Instance;
    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);

        Instance = instance;
    }

    public void CreateButtons(List<Recipes> craftableRecipes) 
    {
        if (recipesContainer.gameObject.activeInHierarchy == false)
            recipesContainer.gameObject.SetActive(true);

        for (int i = recipesContainer.childCount - 1; i > -1; i--)
        {
            if (!craftableRecipes.Contains(recipesContainer.GetChild(i).GetComponent<CraftingButton>().craftedRecipes))
                Destroy(recipesContainer.GetChild(i).gameObject);
        }

        foreach (Recipes recipe in craftableRecipes)
            if (!IsRecipeInstantiated(recipe))
                Instantiate(buttonPrefab, recipesContainer).GetComponent<CraftingButton>().AssignedRecipe(recipe, craftingSystem);
    }

    bool IsRecipeInstantiated(Recipes recipe)
    {
        for (int i = 0; i < recipesContainer.childCount; i++)
        {
            if (recipesContainer.GetChild(i).GetComponent<CraftingButton>().craftedRecipes == recipe)
                return true;
        }
        return false;
    }
}
