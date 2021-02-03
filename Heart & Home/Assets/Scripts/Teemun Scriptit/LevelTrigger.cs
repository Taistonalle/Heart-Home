using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelTrigger : MonoBehaviour {
    public string levelID, spawnID;
    private void OnTriggerEnter2D(Collider2D collision) {
        FindObjectOfType<LevelManager>().LoadLevel(levelID, spawnID); // siisti tämä myöhemmin
    }

    void Update() {

    }
}
