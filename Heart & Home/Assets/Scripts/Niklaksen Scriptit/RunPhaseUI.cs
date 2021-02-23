using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunPhaseUI : MonoBehaviour
{
    // Start is called before the first frame update
    KitchenPhaseSystem ksp;
    public FridgeUI fridgeUI;
    public ExitUI exitUI;
    public AutoCauldronUI autoCauldronUI;
    public ManuCauldronUI manuCauldronUI;
    public NoticeBoardUI noticeBoardUI;
    public bool runningUI;

    void Awake() {
        ksp = FindObjectOfType<KitchenPhaseSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        if (runningUI == false) {
            if (Input.GetKeyDown(KeyCode.E)) {
                RunPhase(ksp.currentPhase);
                runningUI = true;
            }
        }

    }

    void RunPhase(KitchenPhase phase) {
        if (phase == KitchenPhase.None) {
            Debug.Log("phase NONE");
        } else if (phase == KitchenPhase.Exit) {
            exitUI.enabled = true;
            print("Running Exit");
        } else if (phase == KitchenPhase.Fridge) {
            fridgeUI.enabled = true;
            print("Running Fridge");
        } else if (phase == KitchenPhase.ManuCauldron) {
            manuCauldronUI.enabled = true;
            print("Running Manual Cauldron");
        } else if (phase == KitchenPhase.AutoCauldron) {
            autoCauldronUI.enabled = true;
            print("Running AutomaticCauldron");
        } else if (phase == KitchenPhase.NoticeBoard) {
            noticeBoardUI.enabled = true;
            print("Running noticeboard");
        } else Debug.Log("No Kitchenphase atm");
    }
}
