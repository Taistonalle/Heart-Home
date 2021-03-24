using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersonalInventoryUI : MonoBehaviour
{
    public GameObject inventoryUI;
    public GameObject foodUI;
    public GameObject ingredientsUI;
    public InventoryState currentState;
    public GameObject fIndicator;
    public GameObject iIndicator;

    void Awake() {
        currentState = InventoryState.Food;

    }

    

    // Update is called once per frame
    void Update() {
        CheckState();
        if (Input.GetButtonDown("Inventory")) {
            inventoryUI.SetActive(!inventoryUI.activeSelf);   
        }

        if (inventoryUI.activeSelf){
            if (Input.GetKeyDown(KeyCode.Tab)) {
                if (currentState == InventoryState.Food) {
                    currentState = InventoryState.Ingredients;
                } else if (currentState == InventoryState.Ingredients) {
                    currentState = InventoryState.Food;
                } else Debug.LogError("changing state not working");
            }
        }
        
    }

    void UpdateUI() {
        Debug.Log("Updating inventory");
        foodUI.GetComponent<PersonalFoodUI>().UpdateUI();
        ingredientsUI.GetComponent<PersonalIngredientsUI>().UpdateUI();
        
    }

    void CheckState() {
        if (currentState == InventoryState.Food) {
            foodUI.SetActive(true);
            ingredientsUI.SetActive(false);
            fIndicator.GetComponent<SelectedUI>().selected = true;
            iIndicator.GetComponent<SelectedUI>().selected = false;
        } else if (currentState == InventoryState.Ingredients) {
            ingredientsUI.SetActive(true);
            foodUI.SetActive(false);
            fIndicator.GetComponent<SelectedUI>().selected = false;
            iIndicator.GetComponent<SelectedUI>().selected = true;
        } else Debug.LogError("checkstate not working");
    }
}
