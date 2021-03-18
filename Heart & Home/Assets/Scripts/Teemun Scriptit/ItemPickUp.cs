using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickUp : MonoBehaviour {
    InventoryManager invManager;
    public ItemDataScriptable itemToAdd;
    //public GameObject placeHolderText;
    void Start() {
        invManager = FindObjectOfType<InventoryManager>();
    }

    void OnTriggerEnter2D(Collider2D collision) {
        //placeHolderText.SetActive(true);
    }

    void OnTriggerExit2D(Collider2D collision) {
        //placeHolderText.SetActive(false);
    }

    void OnTriggerStay2D(Collider2D collision) {
        if (Input.GetKeyDown(KeyCode.E)) {
            
            invManager.AddItemInInventory(invManager.personalInvIngredients, itemToAdd);
            gameObject.SetActive(false);
            print("Added " + itemToAdd.item);
        }
    }
}
