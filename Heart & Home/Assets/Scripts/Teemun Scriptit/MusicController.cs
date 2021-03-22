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

    void ChangeMusic() {
        soundSource.Stop();
    }

    void Update() {
        if (gameManager.gameState == GameState.Platforming && !platform) {
            ChangeMusic();
            platform = true;
            kitchen = false;
            soundSource.PlayOneShot(music[0]);
        }
        else if (gameManager.gameState == GameState.Kitchen && !kitchen) {
            ChangeMusic();
            kitchen = true;
            platform = false;
            soundSource.PlayOneShot(music[1]);
        }
    }
}
