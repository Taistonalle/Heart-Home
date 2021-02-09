using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour {

    public delegate void OnItemChange();
    public OnItemChange onItemChangeCallback;
    public Inventory kitchenInv = new Inventory();
    void Start() {

    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.I)) {
            var s = "";
            foreach(var item in kitchenInv.items) {
                //Enum.GetName(typeof(ItemType), item.kind)); 
            
            s += item.kind + " ";
            }

            print(s);
        }
    }

    public void AddItem(ItemDataScriptable item) {
        kitchenInv.items.Add(new ItemData(item));
        if (onItemChangeCallback != null) {
            onItemChangeCallback.Invoke();
        }
    }

    public void RemoveItem(ItemDataScriptable item) {
        kitchenInv.items.Remove(new ItemData(item));
        if (onItemChangeCallback != null) {
            onItemChangeCallback.Invoke();
        }
    }
}
