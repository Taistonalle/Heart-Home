using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    // Start is called before the first frame update
    InventoryManager inv;
    public GameObject itemParent;
    InventorySlot[] slots;
    public GameObject inventoryUI;
    void Awake()
    {
        inv = FindObjectOfType<InventoryManager>();
        inv.onItemChangeCallback += UpdateUI;

    }

    void Start() {
        slots = itemParent.GetComponentsInChildren<InventorySlot>();

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Inventory")) {
            inventoryUI.SetActive(!inventoryUI.activeSelf);
        }
    }

    void UpdateUI() {
        Debug.Log("Updating inventory");

        for (int i = 0; i < slots.Length; i++) {
            if (i < inv.kitchenInv.items.Count) {
                slots[i].AddItem(inv.kitchenInv.items[i]);
            } else {
                slots[i].ClearSlot();
            }
        }
    }
}
