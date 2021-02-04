using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[System.Serializable]
public struct LevelData {
    public string id;
    public GameObject prefab;
}
public class LevelManager : MonoBehaviour {
    //public Dictionary<string, int> dict;
    public List<LevelData> levels;
    public GameObject player;
    GameObject currentLevel = null;
    void Start() {

    }

    void Update() {

    }

    void UnloadCurrent() {
        Destroy(currentLevel);
        currentLevel = null;
    }

    public void UnloadLevel() {
        UnloadCurrent();
    }

    public void LoadLevel(string levelID, string spawnpointID) {
        if(currentLevel != null) {
            UnloadCurrent();
        }

        //Unload previous level
        GameObject prefab = null;
        foreach (var ld in levels) {
            if(ld.id == levelID) {
                prefab = ld.prefab;
                break;
            }
        }
        // var x = levels.Where(ld => ld.id == levelID).First();
        if(prefab == null) {
            Debug.LogError("Level missing!" + levelID);
        }

        var level = Instantiate(prefab);
        currentLevel = level;
        //Intialize level scripts

        var spawns = level.GetComponentsInChildren<PlayerSpawn>();
        GameObject waypoint = null;
        foreach (var sp in spawns) {
            if(sp.id == spawnpointID) {
                waypoint = sp.gameObject;
                break;
            }
        }
        if (waypoint == null) {
            Debug.LogError("Spawnpoint missing!" + spawnpointID);
        }
        player.transform.position = waypoint.transform.position;
    }
}
