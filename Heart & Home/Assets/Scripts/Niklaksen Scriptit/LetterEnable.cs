using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LetterEnable : MonoBehaviour
{
    public SelectedUI ui;
    public GameObject letter;
    public NoitceBoardLetterControll noitce;
    
    

    public void ShowLetter() {
        if (letter == null) {
            Debug.LogError("No letter");
        } else {
            //letter.SetActive(true);
            letter.SetActive(true);
            letter.GetComponent<LetterScript>().noitce = noitce;
            print(letter);
        }
    }
}
