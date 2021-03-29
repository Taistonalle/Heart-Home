using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MonsterController : MonoBehaviour {
    public LayerMask detectLayer;
    MonsterManager mManager;
    [Range(0.2f, 2f)] public float monsterSpeed = 1f;
    [Range(5f, 20f)] public float detectRadius = 5f;
    public float patrolIdleTime = 2f;
    public bool chaseEnabled, attackEnabled, searchEnabled;
    float iddleCD, chaseSpeed, normalSpeed;
    bool movingLeft = true, canIdle = true;
    Vector2 returnPoint, monsterPos, patrolPoint1, patrolPoint2, scale;
    public GameObject[] patrolPoints;
    RaycastHit2D circleHit, circleHitB;
    GameObject player;
    Animator animator;
    Rigidbody2D mRb;

    void Start() {
        player = GameObject.FindGameObjectWithTag("Player");
        mManager = GetComponent<MonsterManager>();
        mRb = GetComponent<Rigidbody2D>();
        animator = GetComponentInChildren<Animator>();
        scale = transform.localScale;
        iddleCD = patrolIdleTime * 2;
        normalSpeed = monsterSpeed;
        chaseSpeed = monsterSpeed * 1.2f;
        monsterPos = gameObject.transform.position;
        returnPoint = monsterPos;
        patrolPoint1 = patrolPoints[0].transform.position;
        patrolPoint2 = patrolPoints[1].transform.position;
    }

    void Update() {
        monsterPos = gameObject.transform.position;
        transform.localScale = scale;
        ScaleChange();
    }
    void FixedUpdate() {
        circleHit = Physics2D.CircleCast(gameObject.transform.position, detectRadius, Vector2.right, 0f, detectLayer);
        circleHitB = Physics2D.CircleCast(gameObject.transform.position, detectRadius / 3, Vector2.right, 0f, detectLayer);
        StateUpdater();

        if (mManager.monsterState == MonsterStates.Patrol) Patrol();
        else if (mManager.monsterState == MonsterStates.Idle) Idle();
        else if (mManager.monsterState == MonsterStates.Chase) Chase();
        else if (mManager.monsterState == MonsterStates.Attacking) Attacking();
        else if (mManager.monsterState == MonsterStates.Searching) Searching();
        else if (mManager.monsterState == MonsterStates.Return) Return();
    }

    void ScaleChange() {
        scale = movingLeft ? new Vector2(-1f, 1f) : new Vector2(1f, 1f);
    }

    void StateUpdater() {
        if (circleHit && mManager.monsterState == MonsterStates.Patrol && chaseEnabled) { //Player enters detecting circle
            mManager.monsterState = MonsterStates.Chase;
        }
        else if (circleHit && mManager.monsterState == MonsterStates.Idle && chaseEnabled) { //Player enters detecting circle while in idle state and chase is enabled
            mManager.monsterState = MonsterStates.Chase;
        }
        else if (!circleHit && mManager.monsterState == MonsterStates.Chase && searchEnabled) { //Player leaves detection cirle with search enabled
            mManager.monsterState = MonsterStates.Searching;
        }
        else if (!circleHit && mManager.monsterState == MonsterStates.Chase && chaseEnabled) { //Player leaves detection cirle while chase is enabled
            mManager.monsterState = MonsterStates.Patrol;
        }
        else if(circleHitB && attackEnabled) { //Player enters attacking range
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
        else if (monsterPos == returnPoint && mManager.monsterState == MonsterStates.Return) {//Monster has returned to his point
            mManager.monsterState = MonsterStates.Patrol;
        }
    }

    IEnumerator PatrolWaitTime() {
        mManager.monsterState = MonsterStates.Idle;
        yield return new WaitForSeconds(patrolIdleTime);
        mManager.monsterState = MonsterStates.Patrol;
    }
    IEnumerator IdleCooldown() {
        canIdle = false;
        yield return new WaitForSeconds(iddleCD);
        canIdle = true;
    }

    public void Patrol() {
        animator.Play("Walk");
        monsterSpeed = normalSpeed;
        var mRbXPos = mRb.position.x;
        mRbXPos = Mathf.Clamp(mRbXPos, patrolPoint1.x, patrolPoint2.x);

        if (movingLeft) {
            mRb.position = mRb.position += Vector2.left * monsterSpeed * Time.deltaTime;
        }
        else if (!movingLeft) {
            mRb.position = mRb.position + Vector2.right * monsterSpeed * Time.deltaTime;  
        }

        if (mRbXPos <= patrolPoint1.x && canIdle) {
            movingLeft = false;
            StartCoroutine(PatrolWaitTime());
            StartCoroutine(IdleCooldown());
        }
        else if (mRbXPos >= patrolPoint2.x && canIdle) {
            movingLeft = true;
            StartCoroutine(PatrolWaitTime());
            StartCoroutine(IdleCooldown());
        }
    }
    void Idle() {
        //Placeholder funktio jos tahtoo jotain erikoita idleen. Esim oma animaatio.
        animator.Play("Idle");
    }

    public void Chase() {
        monsterSpeed = chaseSpeed;
        mRb.position = Vector2.Lerp(mRb.position, player.transform.position, monsterSpeed * Time.deltaTime); //Placeholder tapa jahtaamiselle
    }

    public void Searching() {

    }

    public void Return() {

    }

    public void Attacking() {

    }

    void OnDrawGizmos() {
        if (!Application.isPlaying) return;
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
            Gizmos.DrawWireSphere(gameObject.transform.position, detectRadius / 3);
        }
        else {
            Gizmos.color = Color.white;
            Gizmos.DrawWireSphere(gameObject.transform.position, detectRadius);
        }
    }

}
