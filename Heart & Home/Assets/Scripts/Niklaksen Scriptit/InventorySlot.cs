using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    public Image icon;
    // Start is called before the first frame update
    public ItemDataScriptable item;
    public Recipes recipe;

    public void AddItem(ItemDataScriptable newItem) {
        item = newItem;

        icon.sprite = item.sprite;
        icon.enabled = true;
    }

    public void AddRecipe(Recipes recipe) {
        item = null;
        icon.sprite = recipe.sprite;
        icon.enabled = true;
    }


    public void ClearSlot() {
        item = null;
        icon.sprite = null;
        icon.enabled = false;
        recipe = null;
    }
}
