using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicController : MonoBehaviour {
    Gamemanager gameManager;
    AudioSource soundSource;
    public AudioClip[] music;
    public bool platform, kitchen;

    void Start() {
        soundSource = GetComponent<AudioSource>();
        gameManager = FindObjectOfType<Gamemanager>();
        platform = gameManager.gameState == GameState.Platforming;
        kitchen = gameManager.gameState == GameState.Kitchen;
    }

    void StopCurrentMusic() {
        soundSource.Stop();
    }

    void Update() {
        if (gameManager.gameState == GameState.Platforming && !platform) {
            StopCurrentMusic();
            platform = true;
            kitchen = false;
            soundSource.clip = music[0];
            soundSource.Play();
        }
        else if (gameManager.gameState == GameState.Kitchen && !kitchen) {
            StopCurrentMusic();
            kitchen = true;
            platform = false;
            soundSource.clip = music[1];
            soundSource.Play();
        }
    }
}
