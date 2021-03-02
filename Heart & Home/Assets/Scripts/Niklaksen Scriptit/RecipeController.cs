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

    void Update() {
        if (Input.GetKeyDown(KeyCode.O)) {
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
        Sprite mainImage = null;
        Sprite secImage = null;

        
        GetCorrectItems(mainIngredient, mainImage);
        GetCorrectItems(secIngredient, secImage);

        var newButton = Instantiate(recipeButton);
        newButton.transform.parent = scroller.transform;
        var newButtonScript = newButton.GetComponent<RecipeButtonScript>();
        newButtonScript.newDishImage = dishIcon;
        newButtonScript.newMainImage = mainImage;
        newButtonScript.newSecondImage = secImage;
        newButtonScript.cookTime = cookTime;
        newButtonScript.recipeEffect = effect;
        newButtonScript.newRecipeName = recipe.ToString();
        
        newButtonScript.CreateButton();
        recipeButtons.Add(newButton);


    }

    void GetCorrectItems(ItemType item, Sprite sprite) {
        for (int i = 0; i < ingContainer.itemDatas.Count; i++) {
            if (item != ingContainer.itemDatas[i].item) {
                continue;
            } else {
                sprite = ingContainer.itemDatas[i].sprite;
            }
        }
    }
}
