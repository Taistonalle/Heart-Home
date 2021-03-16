using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum InventorySide { Personal, Kitchen};
public class SwitchInventory : MonoBehaviour
{
    public GameObject personalInventory;
    public GameObject kitchenInventory;
    InventorySide currentSide;

    private void OnEnable() {
        currentSide = InventorySide.Personal;
    }

    private void Update() {
        SwitchSides();
        if (Input.GetKeyDown(KeyCode.LeftShift)) {
            if (currentSide == InventorySide.Personal) {
                currentSide = InventorySide.Kitchen;
            } else if (currentSide == InventorySide.Kitchen) {
                currentSide = InventorySide.Personal;
            } else Debug.LogError("InventorySide misfunction");
        }
    }

    void SwitchSides() {
        if (currentSide == InventorySide.Personal) {
            personalInventory.GetComponent<ScrollForUI>().enabled = true;
            kitchenInventory.GetComponent<ScrollForUI>().enabled = false;
        } else if (currentSide == InventorySide.Kitchen) {
            personalInventory.GetComponent<ScrollForUI>().enabled = false;
            kitchenInventory.GetComponent<ScrollForUI>().enabled = true;
        }
    }
}
