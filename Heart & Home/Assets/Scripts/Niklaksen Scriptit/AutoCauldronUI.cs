using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoCauldronUI : MonoBehaviour
{
    public RunPhaseUI run;
    // Start is called before the first frame update
    public RecipeContainer recipes;


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q)) {
            run.runningUI = false;
            print("Exiting Automatic CauldronUI");
        }
    }

    void MakeRecipeButton(Recipes recipe) {
 
    }
}
