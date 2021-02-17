using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunPhaseUI : MonoBehaviour
{
    // Start is called before the first frame update
    KitchenPhaseSystem ksp;
    FridgeUI fridgeUI;
    ExitUI exitUI;
    AutoCauldronUI autoCauldronUI;
    ManuCauldronUI manuCauldronUI;
    NoticeBoardUI noticeBoardUI;

    void Awake() {
        ksp = FindObjectOfType<KitchenPhaseSystem>();
    }
    void Start()
    {
        fridgeUI = FindObjectOfType<FridgeUI>();
        exitUI = FindObjectOfType<ExitUI>();
        autoCauldronUI = FindObjectOfType<AutoCauldronUI>();
        manuCauldronUI = FindObjectOfType<ManuCauldronUI>();
        noticeBoardUI = FindObjectOfType<NoticeBoardUI>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E)) {
            RunPhase(ksp.currentPhase);
        }
    }

    void RunPhase(KitchenPhase phase) {
        if (phase == KitchenPhase.None) {
            Debug.Log("phase NONE");
        } else if (phase == KitchenPhase.Exit) {
            exitUI.enabled = true;
        } else if (phase == KitchenPhase.Fridge) {
            fridgeUI.enabled = true;
        } else if (phase == KitchenPhase.ManuCauldron) {
            manuCauldronUI.enabled = true;
        } else if (phase == KitchenPhase.AutoCauldron) {
            autoCauldronUI.enabled = true;
        } else if (phase == KitchenPhase.NoticeBoard) {
            noticeBoardUI.enabled = true;
        } else Debug.Log("No Kitchenphase atm");
    }
}
