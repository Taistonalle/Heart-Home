using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddItemToInvTest : MonoBehaviour {
    InventoryManager invManager;
    public ItemDataScriptable itemToAdd;
    void Start() {
        invManager = FindObjectOfType<InventoryManager>();
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.F)) { //F to pay respects
            var item = new ItemData(itemToAdd);
            invManager.kitchenInv.items.Add(itemToAdd);
        }
    }
}
