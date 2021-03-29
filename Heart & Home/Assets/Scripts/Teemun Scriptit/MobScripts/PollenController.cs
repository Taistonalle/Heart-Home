using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PollenController : MonoBehaviour {
    PlayerManager pManager;
    Rigidbody2D pollenRB;
    Vector3 currentPos;
    Vector2 scale;
    [Range(1f, 2f)] public float sinFrequency = 2f;
    [Range(0.05f, 0.5f)] public float sinMagnitude = 0.5f;
    [Range(30f, 100f)] public float speed = 50f;
    [Range(10, 50)] public int pollenDMG = 10;
    public Transform[] patrolPoints;
    bool movingLeft = true;

    void Start() {
        pollenRB = GetComponent<Rigidbody2D>();
        pManager = FindObjectOfType<PlayerManager>();
        scale = transform.localScale;
    }

    void Update() {
        transform.localScale = scale;
        currentPos = transform.position;
        FloatMovement();
        ScaleChange();
    }

    void FixedUpdate() {
        Movement();
    }
    void FloatMovement() {
        transform.position =  currentPos + new Vector3(0f, 0.5f) * Mathf.Sin(Time.time * sinFrequency) * sinMagnitude;
    }

    void Movement() {
        if (movingLeft) pollenRB.velocity = new Vector2(-speed, 0f) * Time.deltaTime;
        else if (!movingLeft) pollenRB.velocity = new Vector2(speed, 0f) * Time.deltaTime;

        if (pollenRB.position.x <= patrolPoints[0].position.x) movingLeft = false;
        else if (pollenRB.position.x >= patrolPoints[1].position.x) movingLeft = true;
    }

    void ScaleChange() {
        if (movingLeft) scale = new Vector2(1f, 1f);
        else if (!movingLeft) scale = new Vector2(-1f, 1f);
    }

    void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.tag == "Player") {
            pManager.Damage(pollenDMG);
        }
    }
}
