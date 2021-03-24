using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum KorentoState { Idle, Dashing, TakingDamage };
public class KorentoController : MonoBehaviour {
    [Range(1f, 10f)] public float dashSpeed = 3f;
    [Range(1f, 10f)] public float idleTime = 2f;
    [Range(10, 20)] public int korentoDamage = 10;
    [Range(1f, 2f)] public float sinFrequency = 2f;
    [Range(0.2f, 0.5f)] public float sinMagnitude = 0.5f;
    [Range(0.1f, 1f)] public float takingDMGTime = 0.5f;
    public GameObject[] flightPoints;
    GameObject currentFlightPoint;
    public KorentoState state;
    bool idleRunning;
    Rigidbody2D korentoRB;
    PlayerManager pManager;

    void Start() {
        pManager = FindObjectOfType<PlayerManager>();
        korentoRB = GetComponent<Rigidbody2D>();
        state = KorentoState.Idle;
        currentFlightPoint = flightPoints[4];
    }

    void OnCollisionEnter2D(Collision2D collision) {
        var target = collision.gameObject;
        if(state == KorentoState.Dashing && target.tag == "Player") {
            pManager.Damage(korentoDamage);
        }
    }

    void Update() {
    }
    void FixedUpdate() {
        if (state == KorentoState.Idle) IdleMovement();

        if (state == KorentoState.Idle && !idleRunning) {
            StartCoroutine(Idle());
        }
        else if (state == KorentoState.Dashing) KorentoDash();
        else if (state == KorentoState.TakingDamage) StartCoroutine(TakingDamage());
    }

    void KorentoDash() {
        Vector3 korentoPos = korentoRB.position;
        korentoRB.position = Vector2.Lerp(korentoRB.position, currentFlightPoint.transform.position, dashSpeed * Time.deltaTime);
        var distance = Vector2.Distance(korentoPos, currentFlightPoint.transform.position);
        if (distance <= 0.2f) state = KorentoState.Idle;
    }

    void IdleMovement() {
        gameObject.transform.position = currentFlightPoint.transform.position + transform.up * Mathf.Sin(Time.time * sinFrequency) * sinMagnitude;
    }

    IEnumerator Idle() {
        idleRunning = true;
        yield return new WaitForSeconds(idleTime);
        idleRunning = false;
        ChooseFlightPoint();
        state = KorentoState.Dashing;
    }

    IEnumerator TakingDamage() {
        yield return new WaitForSeconds(takingDMGTime);
        state = KorentoState.Idle;
    }

    void ChooseFlightPoint() {
        var randomPoint = Random.Range(0, flightPoints.Length);
        var samePoint = flightPoints[randomPoint] == currentFlightPoint;
        if(samePoint) {
            Debug.LogWarning("Same flight point, rerolling");
            ChooseFlightPoint();
        }
        else {
            currentFlightPoint = flightPoints[randomPoint];
        }
    }
}
