using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReturnButton : MonoBehaviour
{
    public LetterScript letterScript;
    public SelectedUI uI;
    public SelectedUI containerUI;
    void Update()
    {
        if (uI.activated == true) {
            CloseLetter();
        }
    }

    public void CloseLetter() {
       
        letterScript.Close();
        letterScript.gameObject.SetActive(false);
        containerUI.activated = false;
    }
}
