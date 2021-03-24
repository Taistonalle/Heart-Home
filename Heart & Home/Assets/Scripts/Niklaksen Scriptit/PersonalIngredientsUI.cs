using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersonalIngredientsUI : MonoBehaviour
{
    public InventoryManager inv;
    public GameObject itemParent;
    public List<InventorySlot> inventorySlots = new List<InventorySlot>();
    public ScrollForUI scroll;

    private void OnEnable() {
        if (inventorySlots.Count == 0) {
            for (int i = 0; i < itemParent.transform.childCount; i++) {
                inventorySlots.Add(itemParent.transform.GetChild(i).GetComponent<InventorySlot>());
            }
        }
        scroll.enabled = true;
        UpdateUI();
    }
    private void OnDisable() {
        scroll.enabled = false;
    }


    public void UpdateUI() {
        for (int i = 0; i < inventorySlots.Count; i++) {
            if (i < inv.personalInvIngredients.items.Count) {
                inventorySlots[i].AddItem(inv.personalInvIngredients.items[i]);
            } else {
                inventorySlots[i].ClearSlot();
            }
        }
    }
}
