using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelTrigger : MonoBehaviour {
    PlayerController pController;
    LevelManager manager;
    FadeController fadeController;
    public string levelID, spawnID;
    private void OnTriggerEnter2D(Collider2D collision) {
        //FindObjectOfType<LevelManager>().LoadLevel(levelID, spawnID); // siisti tämä myöhemmin
        fadeController.StartCoroutine("FadeUp");
        //manager.LoadLevel(levelID, spawnID);
    }
    private void OnTriggerStay2D(Collider2D collision) {
        pController.playerRB.velocity = new Vector2(0f, pController.playerRB.velocity.y);
        if (fadeController.fade.a >= 1f) manager.LoadLevel(levelID, spawnID);
    }

    void Start() {
        manager = FindObjectOfType<LevelManager>();
        fadeController = FindObjectOfType<FadeController>();
        pController = FindObjectOfType<PlayerController>();
    }
}
