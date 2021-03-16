using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersonalInventoryAdd : MonoBehaviour
{
    public GameObject inventoryParent;

    void OnEnable() {
        inventoryParent.GetComponent<ScrollForUI>().enabled = true;
    }

    private void OnDisable() {
        inventoryParent.GetComponent<ScrollForUI>().enabled = false;
    }

}
