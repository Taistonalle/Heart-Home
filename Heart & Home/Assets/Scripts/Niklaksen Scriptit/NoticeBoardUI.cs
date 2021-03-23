using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoticeBoardUI : MonoBehaviour
{
    // Start is called before the first frame update
    public RunPhaseUI run;
    public GameObject UI;
    void OnEnable()
    {
        UI.SetActive(true);
    }

    // Update is called once per frame
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q)) {
            UI.SetActive(false);
            run.runningUI = false;
            this.enabled = false;
            print("Exiting noticeboardUI");
        }
    }
}
