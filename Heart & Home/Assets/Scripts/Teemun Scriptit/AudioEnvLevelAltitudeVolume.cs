using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class AudioEnvLevelAltitudeVolume : MonoBehaviour, ILevelLoad {
    public float maxVolume, minVolume;
    public float maxY, minY;
    public AnimationCurve curve;
    public string audioId;
    Transform player;

    public void OnLevelLoad() {
    }

    public void OnLevelUnload() {
        AudioFW.RestoreVolume(audioId);
    }

    void Start() {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        var tm = FindObjectOfType<Tilemap>();
        var cellBounds = tm.cellBounds;
        minY = tm.GetCellCenterWorld(cellBounds.min).y;
        maxY = tm.GetCellCenterWorld(cellBounds.max).y;
    }

    void Update() {
        var t = (player.position.y - minY) / (maxY - minY);

        t = curve.Evaluate(t);

        var volume = Mathf.Lerp(minVolume, maxVolume, t);

        AudioFW.AdjustVolume(audioId, volume);

    }
}
