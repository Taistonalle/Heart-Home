using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    public Image icon;
    // Start is called before the first frame update
    public ItemData item;

    public void AddItem(ItemData newItem) {
        item = newItem;

        icon.sprite = item.sprite;
        icon.enabled = true;
    }

    public void ClearSlot() {
        item = null;
        icon.sprite = null;
        icon.enabled = false;
    }
}
