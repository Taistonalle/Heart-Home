using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngredientsVisualizer : MonoBehaviour
{
    InventoryManager inv;
    public GameObject itemParent;
    InventorySlot[] slots;


    //public GameObject inventoryUI;
    void OnEnable() {
        inv = FindObjectOfType<InventoryManager>();
        slots = itemParent.GetComponentsInChildren<InventorySlot>();
        UpdateUI();
    }

    

    // Update is called once per frame
    void Update() {
 
    }

    void UpdateUI() {
        Debug.Log("Updating cauldron inventory");

        for (int i = 0; i < slots.Length; i++) {
            if (i < inv.kitchenInvIngredients.items.Count) {
                slots[i].AddItem(inv.kitchenInvIngredients.items[i]);
                print(inv.kitchenInvIngredients.items[i].kind);
            } else {
                slots[i].ClearSlot();
            }
        }
    }
}
