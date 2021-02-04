using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelTrigger : MonoBehaviour {
    LevelManager manager;
    public string levelID, spawnID;
    private void OnTriggerEnter2D(Collider2D collision) {
        //FindObjectOfType<LevelManager>().LoadLevel(levelID, spawnID); // siisti tämä myöhemmin
        manager.LoadLevel(levelID, spawnID);
    }

    void Start() {
       manager = FindObjectOfType<LevelManager>();
    }

    void Update() {

    }
}
