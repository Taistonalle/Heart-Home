using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType { Food, Flower, Moondust, Ingredient, Weapon, Enemy };
public class ItemData  {
    public ItemType kind;
    public Sprite sprite;
    
    public ItemData(ItemDataScriptable data) {
        kind = data.item;
        sprite = data.sprite;
    }
}
