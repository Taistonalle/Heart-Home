using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    InventoryManager inventoryManager;
    public ItemDataScriptable item;
    public GameObject InteractionClue;

    private void Awake() {
        inventoryManager = FindObjectOfType<InventoryManager>();
    }
    private void OnTriggerEnter2D(Collider2D collision) {
        InteractionClue.SetActive(true);
    }

    private void OnTriggerExit2D(Collider2D collision) {
        InteractionClue.SetActive(false);
    }

    private void OnTriggerStay2D(Collider2D collision) {
        if (Input.GetKeyDown(KeyCode.E)) {
            inventoryManager.AddItem(item);
            Destroy(gameObject);
        }
    }
}
