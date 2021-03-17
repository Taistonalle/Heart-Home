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
    public float onDMGFlashSpeed;
    bool canBeDamaged = true;
    SpriteRenderer sR;
    Color alpha;
    HealthBar healthBar;
    PlayerSounds playerSounds;
    TintControl tintControl;

    void Start() {
        healthBar = GetComponentInChildren<HealthBar>();
        tintControl = GetComponentInChildren<TintControl>();
        sR = GetComponentInChildren<SpriteRenderer>();
        playerSounds = GetComponent<PlayerSounds>();
        alpha = Color.white;
        //enabledPowerUps.Add(dash);
    }
    void Update() {
        healthPoints = Mathf.Clamp(healthPoints, 0, 100);
        healthBar.SetHealth();
        sR.color = alpha;


        if (healthPoints <= 0) {
            Death();
        }

        //placeholder test
        if (Input.GetKeyDown(KeyCode.M) && canBeDamaged) {
            Damage(10);
        }
    }

    IEnumerator FlashOnDMG() {
        for (int i = 0; i < 5; i++) {
            alpha.a = 0.5f;
            yield return new WaitForSeconds(onDMGFlashSpeed);
            alpha.a = 0.0f;
            yield return new WaitForSeconds(onDMGFlashSpeed);
        }
        alpha.a = 1f;
        canBeDamaged = true;
    }

    public void Damage(int d) {
        canBeDamaged = false;
        healthPoints -= d;
        tintControl.Damage();
        playerSounds.PlayerHitSound();
        StartCoroutine(FlashOnDMG());
    }

    void Death() {
        Debug.Log("Death not implemented yet");
    }

}
