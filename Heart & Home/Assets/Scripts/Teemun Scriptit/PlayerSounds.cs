using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSounds : MonoBehaviour {
    AudioSource soundSource;
    public AudioClip[] audioClips;
    void Start() {
        soundSource = GetComponent<AudioSource>();
    }

    public void JumpSound() {
        soundSource.PlayOneShot(audioClips[0]);
    }

    public void DashSound() {
        soundSource.PlayOneShot(audioClips[1]);
    }

    public void LightAttackSound() {
        soundSource.PlayOneShot(audioClips[2]);
    }

    public void LandingSound() {
        soundSource.PlayOneShot(audioClips[3]);
    }

    public void PlayerHitSound() {
        soundSource.PlayOneShot(audioClips[4]);
    }
}
