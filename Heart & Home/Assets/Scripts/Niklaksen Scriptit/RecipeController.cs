using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class RecipeController : MonoBehaviour
{
    // Start is called before the first frame update
    public RecipeContainer recContainer;
    public IngredientContainer ingContainer;
    public GameObject recipeButton;
    public List<GameObject> recipeButtons = new List<GameObject>();
    public GameObject scroller;


    void OnEnable() {
        if (recipeButtons.Count == 0) {
            for (int i = 0; i < recContainer.recipes.Count; i++) {
                CreateRecipeButton(recContainer.recipes[i]);
            }
        }
    }

    void CreateRecipeButton(Recipes recipe) {
        var mainIngredient = recipe.mainIngridient;
        var secIngredient = recipe.neededIngredient;
        var cookTime = recipe.cookkingTime;
        var effect = recipe.effect;
        Sprite dishIcon = recipe.sprite;
        
        var newButton = Instantiate(recipeButton);
        newButton.transform.parent = scroller.transform;
        var newButtonScript = newButton.GetComponent<RecipeButtonScript>();

        for (int i = 0; i < ingContainer.itemDatas.Count; i++) {
            if (mainIngredient != ingContainer.itemDatas[i].item || secIngredient != ingContainer.itemDatas[i].item) {
                continue;
            } else if (mainIngredient == ingContainer.itemDatas[i].item) {
                newButtonScript.mainIngredient = ingContainer.itemDatas[i];
                newButtonScript.mainImage.sprite = ingContainer.itemDatas[i].sprite;

            } else if (secIngredient == ingContainer.itemDatas[i].item) {
                newButtonScript.secondIngredient = ingContainer.itemDatas[i];
                newButtonScript.secondImage.sprite = ingContainer.itemDatas[i].sprite;
            }
        }
        print(mainIngredient + " " + secIngredient);
        newButtonScript.dishImage.sprite = dishIcon;
        newButtonScript.cookTime = cookTime;
        newButtonScript.recipeEffect = effect;
        newButtonScript.recipeName.text = recipe.ToString();
        recipeButtons.Add(newButton);
    }
}
