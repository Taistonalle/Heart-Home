using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LetterScript : MonoBehaviour
{
    // Start is called before the first frame update
    public bool completedMission;
    public Effect reward;
    public Recipes neededDish;
    int letterSwitch = 0;
    public SelectedUI returnUI;
    public SelectedUI acceptUI;
    public NoitceBoardLetterControll noitce;


    private void Update() {
        Visualize();
        LetterMovement();
    }

    void Visualize() {
        if (letterSwitch == 0) {
            returnUI.selected = true;
            acceptUI.selected = false;
        } else if (letterSwitch == 1) {
            returnUI.selected = false;
            acceptUI.selected = true;
        }
    }
    public void Close() {
        returnUI.activated = false;
        acceptUI.activated = false;
        noitce.enabled = true;
        gameObject.SetActive(false);
    }
    void LetterMovement() {
        if (Input.GetKeyDown(KeyCode.D)) {
            if (letterSwitch == 0) {
                letterSwitch = 1;
            } else if (letterSwitch == 1) {
                letterSwitch = 0;
            }

            
        } else if (Input.GetKeyDown(KeyCode.A)) {
            if (letterSwitch == 0) {
                letterSwitch = 1;
            } else if (letterSwitch == 1) {
                letterSwitch = 0;
            }

            
        } else if (Input.GetKeyDown(KeyCode.E)) {
            if (letterSwitch == 0) {
                returnUI.activated = true;
            } else if (letterSwitch == 1) {
                acceptUI.activated = true;
            }
            
        }
    }
}
