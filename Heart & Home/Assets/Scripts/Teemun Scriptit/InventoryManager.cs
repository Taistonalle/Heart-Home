using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour {

    public delegate void OnItemChange();
    public OnItemChange onItemChangeCallback;
    public Inventory kitchenInv = new Inventory();
    public FoodInventory personalInvFood = new FoodInventory();
    public Inventory personalInvIngredients = new Inventory();
    public Inventory kitchenInvIngredients = new Inventory();
    public FoodInventory kitchenInvFood = new FoodInventory();

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

                s += item.item + " ";
            }

            print(s);
        }
    }

    public void AddItemInInventory(Inventory inv, ItemDataScriptable item) {
        if (inv == kitchenInvIngredients) {
            kitchenInvIngredients.items.Add(item);
        } else if (inv == personalInvIngredients) {
            personalInvIngredients.items.Add(item);
        }

        if (onItemChangeCallback != null) {
            onItemChangeCallback.Invoke();
        }
    }

    public void RemovItemInInventory(Inventory inv, ItemDataScriptable item) {
        if (inv == kitchenInvIngredients) {
            kitchenInvIngredients.items.Remove(item);
        } else if (inv == personalInvIngredients) {
            personalInvIngredients.items.Remove(item);
        }

        if (onItemChangeCallback != null) {
            onItemChangeCallback.Invoke();
        }
    }
    public void AddItem(ItemDataScriptable item) {
        kitchenInv.items.Add(item);
        print(item.sprite);
        if (onItemChangeCallback != null) {
            onItemChangeCallback.Invoke();
        }


    }

    public void RemoveItem(ItemDataScriptable item) {
        kitchenInv.items.Remove(item);
        if (onItemChangeCallback != null) {
            onItemChangeCallback.Invoke();
        }
    }

    public void AddFoodInInventory(FoodInventory foodInventory, Recipes recipe) {
        if (foodInventory == personalInvFood) {
            personalInvFood.recipes.Add(recipe);
        } else if (foodInventory == kitchenInvFood) {
            kitchenInvFood.recipes.Add(recipe);
        } else Debug.LogError("no inventory found");

        if (onItemChangeCallback != null) {
            onItemChangeCallback.Invoke();
        }
    }

    public void RemoveFoodInInventory(FoodInventory foodInventory, Recipes recipe) {
        if (foodInventory == personalInvFood) {
            personalInvFood.recipes.Remove(recipe);
        } else if (foodInventory == kitchenInvFood) {
            personalInvFood.recipes.Remove(recipe);
        } else Debug.LogError("no inventory found");

        if (onItemChangeCallback != null) {
            onItemChangeCallback.Invoke();
        }
    }

    public void Save() {
        for(int i = 0; i < kitchenInv.items.Count; i++) {
            var name = kitchenInv.items[i].item.ToString();
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
