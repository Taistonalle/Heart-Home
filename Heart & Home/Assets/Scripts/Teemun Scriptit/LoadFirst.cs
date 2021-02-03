using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadFirst : MonoBehaviour {
    public string levelID, spawnID;
    void Start() {
        FindObjectOfType<LevelManager>().LoadLevel(levelID, spawnID);
    }

    void Update() {

    }
}
