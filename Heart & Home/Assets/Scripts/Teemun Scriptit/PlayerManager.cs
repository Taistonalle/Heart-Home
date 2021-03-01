using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PowerUps { Dash };
public class PlayerManager : MonoBehaviour {
    public PowerUps powerUps;
    [Range(0, 100)] public int healthPoints = 100;

    void Update() {
        healthPoints = Mathf.Clamp(healthPoints, 0, 100);

        if (healthPoints <= 0) {
            Death();
        }
    }

    public void Damage(int d) {
        healthPoints -= d;
    }

    void Death() {
        Debug.Log("Death not implemented yet");
    }

}
