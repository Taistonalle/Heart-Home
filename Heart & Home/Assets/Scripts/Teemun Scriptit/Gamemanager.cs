using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum GameState { Menu, Pause, Platforming, Kitchen };
public class Gamemanager : MonoBehaviour {
    public GameState gameState { get; private set; }
    LevelManager levelManager;
    KitchenManager kitchenManager;
    public GameObject Camera;
    public ExitUI exitUI;
    SaveLoadManager saveLoad;
    RunPhaseUI run;
    void Start() {
        gameState = GameState.Menu; 
        levelManager = FindObjectOfType<LevelManager>();
        kitchenManager = FindObjectOfType<KitchenManager>();
        saveLoad = FindObjectOfType<SaveLoadManager>();
        StartState(GameState.Kitchen); //Teemun lisäys 17.2
        run = FindObjectOfType<RunPhaseUI>(); //niklas 17.3
    }

    void GoToKitchen() {
        kitchenManager.EnterKitchen();
        saveLoad.Save();
    }

    void GoToPlatforming(bool fromLoad = false) {
        if (fromLoad) {
            string loadID;
            string loadWaypoint;
            saveLoad.GetCurrentLvlAndWaypoint(out loadID, out loadWaypoint);
            levelManager.LoadLevel(loadID, loadWaypoint);
        } else {
            levelManager.LoadLevel("Level1", "Start");
        }
        Camera.GetComponent<PlayerFollower>().followingPlayer = true;
    }

    void ExitKitchen() {
        kitchenManager.ExitKitchen();
    }

    void ExitPlatforming() {
        levelManager.UnloadLevel();
        Camera.GetComponent<PlayerFollower>().followingPlayer = false;
        Camera.transform.position = Camera.GetComponent<PlayerFollower>().startingPosition;
    }

    public void StartState(GameState newState, bool fromLoad = false) {
        if (gameState == newState && !fromLoad) {
            Debug.LogWarning("Same level already!");
            return;
        }
        if(gameState == GameState.Kitchen) {
            ExitKitchen();
        }
        else if(gameState == GameState.Platforming) {
            ExitPlatforming();
        }

        gameState = newState;

        if (gameState == GameState.Kitchen) {
            GoToKitchen();
        }
        else if (gameState == GameState.Platforming) {
            GoToPlatforming(fromLoad);
        }

    }
    void Update() {
        // Debug testi napit
        if (Input.GetKeyDown(KeyCode.K)) {
            StartState(GameState.Kitchen);
        }
        else if (Input.GetKeyDown(KeyCode.P)) {
            StartState(GameState.Platforming);
        } else if (exitUI.exiting == true) {
            StartState(GameState.Platforming);
            exitUI.exiting = false;
            exitUI.enabled = false;
            run.runningUI = false;
        }
    }
}
