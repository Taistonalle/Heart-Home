using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour {

    public delegate void OnItemChange();
    public OnItemChange onItemChangeCallback;
    public Inventory kitchenInv = new Inventory();
    public Inventory personalInvFood = new Inventory();
    public Inventory personalInvIngredients = new Inventory();
    public Inventory kitchenInvIngredients = new Inventory();
    public Inventory kitchenInvFood = new Inventory();

    SaveLoadManager saveLoad;

    Dictionary<string, ItemDataScriptable> itemTypes = new Dictionary<string, ItemDataScriptable>();


    void Awake() {
        LoadItemTypes();
        saveLoad = FindObjectOfType <SaveLoadManager>();
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.I)) {
            var s = "";
            foreach (var item in kitchenInv.items) {
                //Enum.GetName(typeof(ItemType), item.kind)); 

                s += item.kind + " ";
            }

            print(s);
        }
    }

    public void AddItemInInventory(Inventory inv, ItemDataScriptable item) {
        if (inv == kitchenInvIngredients) {
            kitchenInvIngredients.items.Add(new ItemData(item));
        } else if (inv == kitchenInvFood) {
            kitchenInvFood.items.Add(new ItemData(item));
        } else if (inv == personalInvFood) {
            personalInvFood.items.Add(new ItemData(item));
        } else if (inv == personalInvIngredients) {
            personalInvIngredients.items.Add(new ItemData(item));
        }

        if (onItemChangeCallback != null) {
            onItemChangeCallback.Invoke();
        }
    }

    public void RemovItemInInventory(Inventory inv, ItemDataScriptable item) {
        if (inv == kitchenInvIngredients) {
            kitchenInvIngredients.items.Remove(new ItemData(item));
        } else if (inv == kitchenInvFood) {
            kitchenInvFood.items.Remove(new ItemData(item));
        } else if (inv == personalInvFood) {
            personalInvFood.items.Remove(new ItemData(item));
        } else if (inv == personalInvIngredients) {
            personalInvIngredients.items.Remove(new ItemData(item));
        }

        if (onItemChangeCallback != null) {
            onItemChangeCallback.Invoke();
        }
    }
    public void AddItem(ItemDataScriptable item) {
        kitchenInv.items.Add(new ItemData(item));
        print(item.sprite);
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
    public void Save() {
        for(int i = 0; i < kitchenInv.items.Count; i++) {
            var name = kitchenInv.items[i].kind.ToString();
            var key = "kitcheninv" + i;
            saveLoad.SetString(key, name);
        }
    }

    

    public void Load() {
        int i = 0;
        kitchenInv.items.Clear();
        while (true) {
            var id = "kitcheninv" + i;
            if (!saveLoad.ContainsString(id)) break;
            var name = saveLoad.GetString(id);
            AddItem(itemTypes[name]);
            i++;
        }
    }

    void LoadItemTypes() {
        var items = Resources.LoadAll<ItemDataScriptable>("Items");
        foreach (var item in items) {
            //print(item.item.ToString());
            itemTypes.Add(item.item.ToString(), item);
        }
    }
}
