using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour {
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
}
