using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoCauldronUI : MonoBehaviour
{
    public RecipeController controller;
    public RunPhaseUI run;
    // Start is called before the first frame update
    public GameObject ui;
    public IngredientsVisualizer viz;

    void OnEnable()
    {
        controller.enabled = true;
        ui.SetActive(true);
        viz.enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q)) {
            run.runningUI = false;
            controller.enabled = false;
            ui.SetActive(false);
            this.enabled = false;
            viz.enabled = false;
            print("Exiting Automatic CauldronUI");
        }
    }
    
}
