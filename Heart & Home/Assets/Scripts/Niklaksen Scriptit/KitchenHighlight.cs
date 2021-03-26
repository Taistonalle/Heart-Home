using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KitchenHighlight : MonoBehaviour
{
    KitchenPhaseSystem ksp;
    public GameObject fridge;
    public GameObject exit;
    public GameObject manuCauldron;
    public GameObject autoCauldron;
    public GameObject noticeBoard;
    Vector3 fridgeOrigScale;
    Vector3 exitOrigScale;
    Vector3 manuOrigScale;
    Vector3 autoOrigScale;
    Vector3 notOrigScale;
    // Start is called before the first frame update
    void Awake()
    {
        ksp = FindObjectOfType<KitchenPhaseSystem>();
        ksp.phaseChange += Highlight;
    }
    private void Start() {
        fridgeOrigScale = fridge.transform.localScale;
        exitOrigScale = exit.transform.localScale;
        manuOrigScale = manuCauldron.transform.localScale;
        autoOrigScale = autoCauldron.transform.localScale;
        notOrigScale = noticeBoard.transform.localScale;
    }
    void ReturnToScale() {
       if (fridge.transform.localScale != fridgeOrigScale) {
            fridge.transform.localScale = fridgeOrigScale;
        } else if (exit.transform.localScale != exitOrigScale) {
            exit.transform.localScale = exitOrigScale;
        } else if (manuCauldron.transform.localScale != manuOrigScale) {
            manuCauldron.transform.localScale = manuOrigScale;
        } else if (autoCauldron.transform.localScale != autoOrigScale) {
            autoCauldron.transform.localScale = autoOrigScale;
        } else if (noticeBoard.transform.localScale != notOrigScale) {
            noticeBoard.transform.localScale = notOrigScale;
        }
    }
    void Highlight(KitchenPhase phase) {
        ReturnToScale();
        if (phase == KitchenPhase.Fridge) {
            fridge.transform.localScale += new Vector3(0.1f, 0.1f, 0f);
        } else if (phase == KitchenPhase.Exit) {
            exit.transform.localScale += new Vector3(0.1f, 0.1f, 0f);
        } else if (phase == KitchenPhase.AutoCauldron) {
            autoCauldron.transform.localScale += new Vector3(0.1f, 0.1f, 0f);
        } else if (phase == KitchenPhase.ManuCauldron) {
            manuCauldron.transform.localScale += new Vector3(0.1f, 0.1f, 0f);
        } else if (phase == KitchenPhase.NoticeBoard) {
            noticeBoard.transform.localScale += new Vector3(0.1f, 0.1f, 0f);
        } else {
            ReturnToScale();
        }
    }
}
