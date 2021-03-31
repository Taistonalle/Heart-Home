using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotIndemoTrigger : MonoBehaviour
{
    TextOnPickUp infoText; //Placeholder tapa tälle nyt
    PlayerManager pManager;
    public string notInDemoTriggeri = "Seems too dangerous to go there";
    void Start() {
        infoText = FindObjectOfType<TextOnPickUp>();
        pManager = FindObjectOfType<PlayerManager>();
    }

    void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.tag == "Player") {
            infoText.NewText(notInDemoTriggeri);
        }
    }
}
