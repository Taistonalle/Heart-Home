using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Recipe", menuName = "RecipeInventory", order = 1)]
public class Recipes : ScriptableObject
{
    public ItemType mainIngridient;
    public ItemType neededIngredient;
}
