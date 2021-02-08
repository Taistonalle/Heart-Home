using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickUp : MonoBehaviour {
    InventoryManager invManager;
    public ItemDataScriptable itemToAdd;
    void Start() {
        invManager = FindObjectOfType<InventoryManager>();
    }

    void OnTriggerEnter2D(Collider2D collision) {
        var item = new ItemData(itemToAdd);
        invManager.kitchenInv.items.Add(item);
        gameObject.SetActive(false);
        print("Added " + item.kind);
    }
}
