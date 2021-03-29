using System.Collections;
using System.Collections.Generic;
using UnityEngine;
    public enum Effect { Health, Stamina, Magic, Speed, Strength, Dash }

[CreateAssetMenu(fileName = "Recipe", menuName = "RecipeInventory", order = 1)]

public class Recipes : ScriptableObject
{

    public Effect effect;
    public ItemType mainIngridient;
    public ItemType neededIngredient;
    public float cookkingTime;
    public Sprite sprite;

}
