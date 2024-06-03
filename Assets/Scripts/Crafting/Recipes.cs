using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "New Recipe")]
public class Recipes : ScriptableObject
{
    public string recipeName;
    public int requiredAge;
    public List<string> requiredItems;
    public GameObject craftedItemPrefab;
    public Sprite ItemIcon;
    public bool RequiresAlchemistTable = false;
    public bool RequiresCampfire = false;
}