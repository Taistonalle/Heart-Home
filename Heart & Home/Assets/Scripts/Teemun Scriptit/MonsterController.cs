using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MonsterController : MonoBehaviour {
    public LayerMask detectLayer;
    MonsterManager mManager;
    [Range(0.2f, 2f)] public float monsterSpeed = 1f;
    [Range(5f, 20f)] public float detectRadius = 5f;
    public Vector2 returnPoint, mPos;
    RaycastHit2D circleHit;
    RaycastHit2D circleHitB;
    GameObject player;
    Rigidbody2D mRb;

    void Start() {
        player = GameObject.FindGameObjectWithTag("Player");
        mManager = GetComponent<MonsterManager>();
        mRb = GetComponent<Rigidbody2D>();
    }

    void Update() {
        mPos = gameObject.transform.position;
    }
    void FixedUpdate() {
        circleHit = Physics2D.CircleCast(gameObject.transform.position, detectRadius, Vector2.right, 0f, detectLayer);
        circleHitB = Physics2D.CircleCast(gameObject.transform.position, detectRadius / 2, Vector2.right, 0f, detectLayer);
        StateUpdater();

        if(mManager.monsterState == MonsterStates.Chase) {
            Chase();
        }
    }

    void StateUpdater() {
        if (circleHit && mManager.monsterState == MonsterStates.Patrol) { //Player enters detecting circle
            mManager.monsterState = MonsterStates.Chase;
        }
        else if (!circleHit && mManager.monsterState == MonsterStates.Chase) { //Player leaves detection cirle
            mManager.monsterState = MonsterStates.Searching;
        }
        else if(circleHitB) { //Player enters attacking range
            mManager.monsterState = MonsterStates.Attacking;
        }
        else if(!circleHitB && mManager.monsterState == MonsterStates.Attacking) { // Player leaves attacking range
            mManager.monsterState = MonsterStates.Chase;
        }
        else if (circleHit && mManager.monsterState == MonsterStates.Searching) { //Player enters detection while still searching
            mManager.monsterState = MonsterStates.Chase;
        }
        else if(circleHit && mManager.monsterState == MonsterStates.Return) { //Player enters detection while returning to return point
            mManager.monsterState = MonsterStates.Chase;
        }
        else if (mPos == returnPoint && mManager.monsterState == MonsterStates.Return) {//Monster has returned to his point
            mManager.monsterState = MonsterStates.Patrol;
        }
    }

    public void Patrol() {

    }

    public void Chase() {
        mRb.position = Vector2.Lerp(mRb.position, player.transform.position, monsterSpeed * Time.deltaTime); //Placeholder tapa jahtaamiselle
    }

    public void Searching() {

    }

    public void Return() {

    }

    public void Attacking() {

    }

    void OnDrawGizmos() {
        if (mManager.monsterState == MonsterStates.Chase) {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(gameObject.transform.position, detectRadius);
        }
        else if (mManager.monsterState == MonsterStates.Searching) {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(gameObject.transform.position, detectRadius);
        }
        else if (mManager.monsterState == MonsterStates.Return) {
            Gizmos.color = Color.cyan;
            Gizmos.DrawWireSphere(gameObject.transform.position, detectRadius);
        }
        else if(mManager.monsterState == MonsterStates.Attacking) {
            Gizmos.color = Color.magenta;
            Gizmos.DrawWireSphere(gameObject.transform.position, detectRadius / 2);
        }
        else {
            Gizmos.color = Color.white;
            Gizmos.DrawWireSphere(gameObject.transform.position, detectRadius);
        }
    }

}
