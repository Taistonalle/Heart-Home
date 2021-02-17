using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KitchenManager : MonoBehaviour {

    public GameObject kitchenPlaceholder; //Väliaikainen
    KitchenPhaseSystem ksp;
    private void Awake() {
        ksp = FindObjectOfType<KitchenPhaseSystem>();
    }


    public void ExitKitchen() {
        kitchenPlaceholder.SetActive(false);
        ksp.enabled = false;
    }

    public void EnterKitchen() {
        kitchenPlaceholder.SetActive(true);
        ksp.enabled = true;
    }

    void Start() {

    }

    void Update() {

    }
}
