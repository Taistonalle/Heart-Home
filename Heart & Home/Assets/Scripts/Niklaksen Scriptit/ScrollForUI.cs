using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollForUI : MonoBehaviour
{
    public List<GameObject> inventorySlots = new List<GameObject>();
    public int rowLength;
    public int rowHight;
    int currentSlot;
    int maxSlots;
    int startingSlot = 0;
    


    void OnEnable()
    {
        if (inventorySlots.Count == 0) {
            for (int i = 0; i < transform.childCount; i++) {
                inventorySlots.Add(transform.GetChild(i).gameObject);
            }
        }
        maxSlots = rowLength * rowHight;
        currentSlot = startingSlot;
    }

    void OnDisable() {
        for (int i = 0; i < inventorySlots.Count; i++) {
            inventorySlots[i].GetComponent<SelectedUI>().selected = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.LeftShift)) {
        //    if (currentSide == InventorySide.Personal) {
        //        currentSide = InventorySide.Kitchen;
        //    } else if (currentSide == InventorySide.Kitchen) {
        //        currentSide = InventorySide.Personal;
        //    } else Debug.LogError("cant change sides");
        //}
        UIMovement();
        VisualizeSlot();
    }  

    void VisualizeSlot() {
        for (int i = 0; i < inventorySlots.Count; i++) {
            if (currentSlot != i) {
                inventorySlots[i].GetComponent<SelectedUI>().selected = false;
            } else if (currentSlot == i) {
                inventorySlots[i].GetComponent<SelectedUI>().selected = true;
            }
        }
    }

    void Scroll(int addOn) {
        if (currentSlot + addOn < 0) {
            int difference = currentSlot + addOn;
            currentSlot = maxSlots - difference;
        } else if (currentSlot + addOn > maxSlots) {
            int difference = (currentSlot + addOn) - maxSlots;
            currentSlot = startingSlot + difference;
        } else currentSlot += addOn;
    }

    void UIMovement() {
        if (Input.GetKeyDown(KeyCode.D)) {
            Scroll(1);
        } else if (Input.GetKeyDown(KeyCode.A)) {
            Scroll(-1);
        } else if (Input.GetKeyDown(KeyCode.S)) {
            Scroll(rowLength);
        } else if (Input.GetKeyDown(KeyCode.W)) {
            Scroll(-rowLength);
        } else if (Input.GetKeyDown(KeyCode.E)) {
            ActivateSlot();
        }
    }

    void ActivateSlot() {
        print("activated slot " + currentSlot);
        inventorySlots[currentSlot].GetComponent<SelectedUI>().activated = true;
    }
}
