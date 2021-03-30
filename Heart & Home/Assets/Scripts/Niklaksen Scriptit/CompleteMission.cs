using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompleteMission : MonoBehaviour
{
    public SelectedUI ui;
    public LetterScript letterScript;
    InventoryManager inv;
    EffectEnable effectEnable;
    public ReturnButton returnButton;

    private void Awake() {
        inv = FindObjectOfType<InventoryManager>();
        effectEnable = FindObjectOfType<EffectEnable>();
    }
    private void Update() {
       if (ui.activated == true) {
            //check if inventory has dish
            var dish = letterScript.neededDish;
            if (inv.personalInvFood.recipes.Contains(dish)) {
                inv.personalInvFood.recipes.Remove(dish);
                effectEnable.EnableEffect(letterScript.reward);
                letterScript.completedMission = true;
                letterScript.Close();
            } else print("you dont have the needed dish");
       }
    }
}
