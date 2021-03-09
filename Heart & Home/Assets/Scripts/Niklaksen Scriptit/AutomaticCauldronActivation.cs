using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutomaticCauldronActivation : MonoBehaviour
{
    RecipeButtonScript[] buttonScripts;
    int currentRecipe;

    void OnEnable() {
        buttonScripts = GetComponentsInChildren<RecipeButtonScript>();
        currentRecipe = 0;
    }

    private void Update() {
        if (buttonScripts.Length != 0) {
            for (int i = 0; i < buttonScripts.Length; i++) {
                if (buttonScripts[i] == buttonScripts[currentRecipe]) {
                    continue;
                } else buttonScripts[i].isHighlighted = false;
            }

            if ((currentRecipe >= -1) && (currentRecipe <= buttonScripts.Length)) {
                if (Input.GetKeyDown(KeyCode.S)) {
                    currentRecipe++;
                } else if (Input.GetKeyDown(KeyCode.W)) {
                    currentRecipe--;
                }
            } else currentRecipe = 0;
 
            buttonScripts[currentRecipe].isHighlighted = true;
        } else print("no recipes");
    }
}
