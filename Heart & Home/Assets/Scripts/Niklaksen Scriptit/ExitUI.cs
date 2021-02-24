using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitUI : MonoBehaviour
{
    public RunPhaseUI run;
    public bool exiting;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q)) {
            run.runningUI = false;
            print("Exiting ExitUI");
        } else if (Input.GetKeyDown(KeyCode.E)) {
            exiting = true;
        }
    }
}
