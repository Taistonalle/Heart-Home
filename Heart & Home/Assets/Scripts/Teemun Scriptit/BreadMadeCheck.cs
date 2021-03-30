using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreadMadeCheck : MonoBehaviour {
    TextOnPickUp infoText; //Placeholder tapa t�lle nyt
    PlayerManager pManager;
    public string n�kym�t�nSein�Triggeri = "I'm too hungry to go on..";
    void Start() {
        infoText = FindObjectOfType<TextOnPickUp>();
        pManager = FindObjectOfType<PlayerManager>();
    }

    void Update() {
        if (pManager.breadMade) {
            gameObject.SetActive(false);
        }
    }

    void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.tag == "Player") {
            infoText.NewText(n�kym�t�nSein�Triggeri);
        }
    }
}
