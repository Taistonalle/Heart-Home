using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum KitchenPhase { None, Fridge, AutoCauldron, ManuCauldron, NoticeBoard, Exit };
public class KitchenPhaseSystem : MonoBehaviour
{
    public delegate void KitchenPhaseChange(KitchenPhase phase);
    public KitchenPhaseChange phaseChange;
    public KitchenPhase currentPhase;
    public bool manualEnabled;
    public RunPhaseUI run;
    
    void OnEnable()
    {
        currentPhase = KitchenPhase.None;
        run.enabled = true;

    }

    private void OnDisable() {
        run.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(currentPhase == KitchenPhase.None) {
            if (Input.GetKeyDown(KeyCode.D)) {
                currentPhase = KitchenPhase.Exit;
                phaseChange(KitchenPhase.Exit);
            } else if (Input.GetKeyDown(KeyCode.A)) {
                currentPhase = KitchenPhase.Fridge;
                phaseChange(KitchenPhase.Fridge);
            }
        } else if (currentPhase == KitchenPhase.Fridge) {
            if (Input.GetKeyDown(KeyCode.D)) {
                currentPhase = KitchenPhase.Exit;
                phaseChange(KitchenPhase.Exit);
            } else if (Input.GetKeyDown(KeyCode.A)) {
                currentPhase = KitchenPhase.AutoCauldron;
                phaseChange(KitchenPhase.AutoCauldron);
            }
        } else if (currentPhase == KitchenPhase.AutoCauldron) {
            if (Input.GetKeyDown(KeyCode.D)) {
                currentPhase = KitchenPhase.Fridge;
                phaseChange(KitchenPhase.Fridge);
            } else if (Input.GetKeyDown(KeyCode.A)) {
                if (manualEnabled == true) {
                    currentPhase = KitchenPhase.ManuCauldron;
                    phaseChange(KitchenPhase.ManuCauldron);
                } else {
                    currentPhase = KitchenPhase.NoticeBoard;
                    phaseChange(KitchenPhase.NoticeBoard);
                }
            }
        } else if (currentPhase == KitchenPhase.ManuCauldron) {
            if (Input.GetKeyDown(KeyCode.D)) {
                currentPhase = KitchenPhase.AutoCauldron;
                phaseChange(KitchenPhase.AutoCauldron);
            } else if (Input.GetKeyDown(KeyCode.A)) {
                currentPhase = KitchenPhase.NoticeBoard;
                phaseChange(KitchenPhase.NoticeBoard);
            }
        } else if (currentPhase == KitchenPhase.NoticeBoard) {
            if (Input.GetKeyDown(KeyCode.D)) {
                if (manualEnabled == true) {
                    currentPhase = KitchenPhase.ManuCauldron;
                    phaseChange(KitchenPhase.ManuCauldron);
                } else {
                    currentPhase = KitchenPhase.AutoCauldron;
                    phaseChange(KitchenPhase.AutoCauldron);
                }
            } else if (Input.GetKeyDown(KeyCode.A)) {
                currentPhase = KitchenPhase.Exit;
                phaseChange(KitchenPhase.Exit);
            }
        } else if (currentPhase == KitchenPhase.Exit) {
            if (Input.GetKeyDown(KeyCode.D)) {
                currentPhase = KitchenPhase.NoticeBoard;
                phaseChange(KitchenPhase.NoticeBoard);
            } else if (Input.GetKeyDown(KeyCode.A)) {
                currentPhase = KitchenPhase.Fridge;
                phaseChange(KitchenPhase.Fridge);
            }
        }
    }
}
