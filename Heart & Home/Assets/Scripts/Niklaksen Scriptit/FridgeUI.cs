using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FridgeUI : MonoBehaviour
{
    public RunPhaseUI run;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q)) {
            run.runningUI = false;
            print("Exiting FridgeUI");
        }
    }
}
