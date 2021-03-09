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
        if (inventoryManager.kitchenInvIngredients.items.Contains(new ItemData(mainIngredient))){
            hasMainIngredient = true;
            print("has mainingredient");
        } else if (inventoryManager.kitchenInvIngredients.items.Contains(new ItemData(secondIngredient))) {
            hasSecondIngredient = true;
            print("Has second ingredient");
        }
    }
}
