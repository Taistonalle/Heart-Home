using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum InventoryState { Food, Ingredients };
public class FridgeUI : MonoBehaviour
{
    public RunPhaseUI run;
    // Start is called before the first frame update
    public GameObject foodUI;
    public GameObject ingredientsUI;
    public InventoryState currentState;
    public GameObject fIndicator;
    public GameObject iIndicator;
    public GameObject fridge;


    void OnEnable()
    {
        currentState = InventoryState.Food;
        fridge.SetActive(true);
    }

    private void OnDisable() {
        fridge.SetActive(false);
    }
    // Update is called once per frame
    void Update()
    {
        CheckState();
        if (Input.GetKeyDown(KeyCode.Q)) {
            run.runningUI = false;
            this.enabled = false;
            print("Exiting FridgeUI");

        } else if (Input.GetKeyDown(KeyCode.Tab)) {
            if (currentState == InventoryState.Food) {
                currentState = InventoryState.Ingredients;
            } else if (currentState == InventoryState.Ingredients) {
                currentState = InventoryState.Food;
            } else Debug.LogError("changing state not working");
        }
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
