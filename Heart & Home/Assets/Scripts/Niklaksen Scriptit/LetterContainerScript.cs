using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LetterContainerScript : MonoBehaviour
{
    // Start is called before the first frame update
    public List<GameObject> incompleteLetters = new List<GameObject>();
    public List<GameObject> completedLetters = new List<GameObject>();
    public List<GameObject> letterContainers = new List<GameObject>();

    private void OnEnable() {
        foreach(var letter in letterContainers) {
            var lLetter = letter.GetComponent<LetterEnable>().letter;
            if (lLetter == null) {
                letter.GetComponent<LetterEnable>().letter = incompleteLetters[0];
                incompleteLetters.RemoveAt(0);
            } else {
                if(lLetter.GetComponent<LetterScript>().completedMission == false) {
                    continue;
                } else {
                    completedLetters.Add(lLetter);
                    letter.GetComponent<LetterEnable>().letter = incompleteLetters[0];
                    incompleteLetters.RemoveAt(0);
                }
            }
        }
    }
}
