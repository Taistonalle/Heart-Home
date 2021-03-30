using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoitceBoardLetterControll : MonoBehaviour
{
    public List<GameObject> containers = new List<GameObject>();
    int startingSlot = 0;
    int maxSlots = 3;
    int currentSlot;


    private void OnEnable() {
        if (containers.Count == 0) {
            for (int i = 0; i < transform.childCount; i++) {
                containers.Add(transform.GetChild(i).gameObject);
            }
        }
    }
    private void Update() {
        VisualizeSlot();
        UIMovement();
    }
    void OnDisable() {
        for (int i = 0; i < containers.Count; i++) {
            containers[i].GetComponent<SelectedUI>().selected = false;
        }
    }

    void VisualizeSlot() {
        for (int i = 0; i < containers.Count; i++) {
            if (currentSlot != i) {
                containers[i].GetComponent<SelectedUI>().selected = false;
            } else if (currentSlot == i) {
                containers[i].GetComponent<SelectedUI>().selected = true;
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
            Scroll(2);
        } else if (Input.GetKeyDown(KeyCode.W)) {
            Scroll(-2);
        } else if (Input.GetKeyDown(KeyCode.E)) {
            ActivateSlot();
        }
    }

    void ActivateSlot() {
        containers[currentSlot].GetComponent<LetterEnable>().ShowLetter();
        print("Showing letter" + currentSlot);
        this.enabled = false;
    }
}
