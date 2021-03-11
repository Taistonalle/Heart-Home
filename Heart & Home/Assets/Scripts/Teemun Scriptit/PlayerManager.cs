using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//public enum PowerUps { Dash };
public class PlayerManager : MonoBehaviour {
    //public struct EnabledPowerUps {
    //    public bool Dash;
    //}
    //public PowerUps powerUps;
    //public EnabledPowerUps powerUps;
    //public List<bool> enabledPowerUps = new List<bool>();
    //bool dash;
    [Range(0, 100)] public int healthPoints = 100;
    HealthBar healthBar;
    TintControl tintControl;

    void Start() {
        healthBar = GetComponentInChildren<HealthBar>();
        tintControl = GetComponentInChildren<TintControl>();
        //enabledPowerUps.Add(dash);
    }
    void Update() {
        healthPoints = Mathf.Clamp(healthPoints, 0, 100);
        healthBar.SetHealth();

        if (healthPoints <= 0) {
            Death();
        }

        //placeholder test
        if (Input.GetKeyDown(KeyCode.M)) Damage(10);
    }

    public void Damage(int d) {
        healthPoints -= d;
        tintControl.Damage();
    }

    void Death() {
        Debug.Log("Death not implemented yet");
    }

}
