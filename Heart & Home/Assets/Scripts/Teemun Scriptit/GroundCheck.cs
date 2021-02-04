using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour {
    PlayerController player;
    void Start() {
        player = FindObjectOfType<PlayerController>();
    }

    void OnTriggerEnter2D(Collider2D collision) {
        player.grounded = true;
    }

    void OnTriggerExit2D(Collider2D collision) {
        player.grounded = false;
    }
}
