using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KitchenManager : MonoBehaviour {

    public GameObject kitchenPlaceholder; //Všliaikainen


    public void ExitKitchen() {
        kitchenPlaceholder.SetActive(false);
    }

    public void EnterKitchen() {
        kitchenPlaceholder.SetActive(true);
    }

    void Start() {

    }

    void Update() {

    }
}
