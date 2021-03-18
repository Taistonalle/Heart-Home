using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KitchenTrigger : MonoBehaviour {
    Gamemanager gm;
    //public GameState gS;

    void OnTriggerEnter2D(Collider2D collision) {
        gm.StartState(GameState.Kitchen);
    }

    void Start() {
        gm = FindObjectOfType<Gamemanager>();
        //gS = gm.gameState;
    }
}
