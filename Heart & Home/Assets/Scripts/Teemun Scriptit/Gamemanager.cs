using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum GameState { Menu, Pause, Platforming, Kitchen };
public class Gamemanager : MonoBehaviour {
    [SerializeField] GameState gameState;
    LevelManager levelManager;
    KitchenManager kitchenManager;
    public GameObject Camera;
    void Start() {
        gameState = GameState.Menu;
        levelManager = FindObjectOfType<LevelManager>();
        kitchenManager = FindObjectOfType<KitchenManager>();
    }

    void GoToKitchen() {
        kitchenManager.EnterKitchen();
    }

    void GoToPlatforming() {
        levelManager.LoadLevel("Level1", "Start");
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

    void StartState(GameState newState) {
        if (gameState == newState) {
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
            GoToPlatforming();
        }

    }
    void Update() {
        // Debug testi napit
        if (Input.GetKeyDown(KeyCode.K)) {
            StartState(GameState.Kitchen);
        }
        else if (Input.GetKeyDown(KeyCode.P)) {
            StartState(GameState.Platforming);
        }
    }
}
