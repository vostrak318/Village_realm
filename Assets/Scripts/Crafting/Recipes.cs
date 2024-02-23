using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "New Recipe")]
public class Recipes : ScriptableObject
{
    public string recipeName;
    public List<string> requiredItems;
    public GameObject craftedItemPrefab;
}
