using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LetterEnable : MonoBehaviour
{
    public SelectedUI ui;
    public GameObject letter;
    void Update()
    {
        if (ui.activated) {
            if(letter == null) {
                Debug.LogError("no Letter");
            } else {
                letter.SetActive(true);
                letter.GetComponentInChildren<ReturnButton>().containerUI = ui;
                print("Activated " + letter);
            }
        } else letter.SetActive(false);
    }
}
