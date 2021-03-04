using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MonsterStates { Patrol, Chase, Searching, Attacking, Return };

public class MonsterManager : MonoBehaviour {
    public MonsterStates monsterState;
    TintControl tintControl;
    [Range(0, 100)] public int healthPoints = 100;

    void Start() {
        tintControl = GetComponentInChildren<TintControl>();
    }

    void Update() {
        healthPoints = Mathf.Clamp(healthPoints, 0, 100);

        if (healthPoints <= 0) {
            Death();
        }
    }

    void ResetMaterial() {
    }

    public void Damage(int d) {
        healthPoints -= d;
        tintControl.Damage();
    }
    void Death() {
        Destroy(gameObject);
        print("Monster defeated!");
    }

}
