using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType { Food, Flower, Moondust };
public class ItemData  {
    public ItemType kind;
    
    public ItemData(ItemDataScriptable data) {
        kind = data.item;
    }
}
