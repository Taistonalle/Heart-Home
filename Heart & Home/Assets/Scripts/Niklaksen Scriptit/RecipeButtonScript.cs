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
    public string newRecipeName;
    public Sprite newMainImage;
    public Sprite newSecondImage;
    public Sprite newDishImage;
    public float cookTime;
    public Effect recipeEffect;
    public bool isHighlighted;
    InventoryManager inventoryManager;

    private void Awake() {
        inventoryManager = FindObjectOfType<InventoryManager>();
    }

    public void CreateButton() {

        mainImage.sprite = newMainImage;
        secondImage.sprite = newSecondImage;
        dishImage.sprite = newDishImage;
        print("created" + newRecipeName);
    }

    void Update() {
        if (isHighlighted) {
            if (Input.GetKeyDown(KeyCode.E)) {
                CookDish();
            }
        }
    }

    void CookDish() {
        //inventoryManager.AddItemToDishInventory()
        print("Cooked" + newRecipeName);
    }
}
