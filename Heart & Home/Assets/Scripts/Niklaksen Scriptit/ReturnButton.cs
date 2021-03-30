using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReturnButton : MonoBehaviour
{
    public LetterScript letterScript;
    public SelectedUI uI;
    void Update()
    {
        if (uI.activated == true) {
            letterScript.Close();
        }
    }
}
