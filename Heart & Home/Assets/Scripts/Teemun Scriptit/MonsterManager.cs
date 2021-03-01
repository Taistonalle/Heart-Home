using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterManager : MonoBehaviour {
    public Color flashColor;
    public Color defaultColor;
    SpriteRenderer sR;
    [Range(0.1f, 0.5f)] public float flashTime = 0.2f;
    [Range(0, 100)] public int healthPoints = 100;

    void Start() {
        sR = GetComponent<SpriteRenderer>();
        sR.color = defaultColor;
    }

    void Update() {
        healthPoints = Mathf.Clamp(healthPoints, 0, 100);

        if (healthPoints <= 0) {
            Death();
        }
    }

    void ResetMaterial() {
        sR.color = defaultColor;
    }

    public void Damage(int d) {
        healthPoints -= d;
        sR.color = flashColor;
        Invoke("ResetMaterial", flashTime);
    }
    void Death() {
        Destroy(gameObject);
        print("Monster defeated!");
    }
}
