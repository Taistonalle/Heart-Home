using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoticeBoardUI : MonoBehaviour
{
    // Start is called before the first frame update
    public RunPhaseUI run;
    void OnEnable()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q)) {
            run.runningUI = false;
            print("Exiting noticeboardUI");
        }
    }
}
