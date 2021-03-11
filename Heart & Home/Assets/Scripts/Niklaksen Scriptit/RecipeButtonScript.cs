using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RecipeButtonScript : MonoBehaviour
{
    // Start is called before the first frame update
    public Text recipeName;
    public Image mainImage;
    public Image secondImage;
    public Image dishImage;
    public float cookTime;
    public Effect recipeEffect;
    public bool isHighlighted;
    public ItemDataScriptable mainIngredient;
    public ItemDataScriptable secondIngredient;
    InventoryManager inventoryManager;
    bool hasMainIngredient;
    bool hasSecondIngredient;

    private void Awake() {
        inventoryManager = FindObjectOfType<InventoryManager>();
    }

    void Update() {
        if (isHighlighted) {
            if (Input.GetKeyDown(KeyCode.E)) {
                print("Checking for Ingredients");
                print(mainIngredient + " " + secondIngredient);
                CheckForIngredients();
                if (hasMainIngredient && hasSecondIngredient) {
                    CookDish();
                } else print("Not enough ingredients");
            }
        }
    }

    void CookDish() {
        //inventoryManager.AddItemToDishInventory()
        print("Cooked" + recipeName);
    }

    void CheckForIngredients() {
        var items = inventoryManager.kitchenInvIngredients.items;
        if (items.Exists(x => x.kind == mainIngredient.item)){
            hasMainIngredient = true;
            print("has mainingredient");
        }
        
        if (items.Exists(x => x.kind == secondIngredient.item)) {
            hasSecondIngredient = true;
            print("Has second ingredient");
        }
    }
}
