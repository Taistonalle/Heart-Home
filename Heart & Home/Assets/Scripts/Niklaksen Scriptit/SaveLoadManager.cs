using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SavedState { Platforming, Kitchen, }
 class SaveState {
    public SaveState() { 
        namedStrings = new Dictionary<string, string>(); 
    }

    public SavedState currentState;
    public string currentWaypoint;
    public string currentLevelID;
    public Dictionary<string, string> namedStrings;
}

public class SaveLoadManager : MonoBehaviour
{
    LevelManager lvlMngr;
    Gamemanager gamemanager;
    SaveState state = new SaveState();
    InventoryManager inventoryManager;

    public bool ContainsString(string key) {
       return state.namedStrings.ContainsKey(key);
    }

    public void GetCurrentLvlAndWaypoint(out string levelID, out string waypoint) {
        levelID = state.currentLevelID;
        waypoint = state.currentWaypoint;
    }

    void Awake() {
        lvlMngr = FindObjectOfType<LevelManager>();
        gamemanager = FindObjectOfType<Gamemanager>();
        inventoryManager = FindObjectOfType<InventoryManager>();
    }

    public string GetString(string key) {
        return state.namedStrings[key];
    }

    public void SetString(string key, string data) {
        state.namedStrings.Add(key, data);
    }
    void Update() {
        if (Input.GetKeyDown(KeyCode.J)) {
            Save();
        }  else if (Input.GetKeyDown(KeyCode.H)) {
            Load();
        }
    }
    public void Save() {
        inventoryManager.Save();
        if (gamemanager.gameState == GameState.Platforming) {
            state.currentState = SavedState.Platforming;
            state.currentLevelID = lvlMngr.currentLevelID;
            state.currentWaypoint = lvlMngr.currentWaypoint;
        } else if (gamemanager.gameState == GameState.Kitchen) {
            state.currentState = SavedState.Kitchen;
        }

    }

    public void Load() {
        Load(state);
    }

    void Load(SaveState state) {
        inventoryManager.Load();
        if (state.currentState == SavedState.Platforming) {
            gamemanager.StartState(GameState.Platforming, true);
        } else if (state.currentState == SavedState.Kitchen) {
            gamemanager.StartState(GameState.Kitchen, true);
        }
    }
}
