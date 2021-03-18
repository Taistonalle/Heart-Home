using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType { Food, Flower, Moondust, Wheat, Water, Honey, Milk, Egg, Strawberry, Blueberry, Banana, Fairydust, Sunshine, Mushroom, Moss, Butterflywings, Flour}

public class ItemData  {
    public ItemType kind;
    public Sprite sprite;

    
    public ItemData(ItemDataScriptable data) {
        kind = data.item;
        sprite = data.sprite;

    }
}
