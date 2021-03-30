using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioEnvLevelSetVolume : MonoBehaviour, ILevelLoad {
    public float volume;
    public string audioId;

    public void OnLevelLoad() {
    }

    public void OnLevelUnload() {
        AudioFW.RestoreVolume(audioId);
    }

    void Start() {
        AudioFW.AdjustVolume(audioId, volume);
    }

    void Update() {

    }
}
