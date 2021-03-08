using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MonsterStates { Patrol, Idle, Chase, Searching, Attacking, Return };

public class MonsterManager : MonoBehaviour {
    public MonsterStates monsterState;
    RaycastHit2D playerOnTop;
    MonsterController mController;
    PlayerController pController;
    LayerMask contact;
    TintControl tintControl;
    [Range(0, 100)] public int healthPoints = 100;
    public float boxCastXAxis, boxCastYAxis, XAxisOffset, YAxisOffset, headBounceForce;
    public bool canBeBounced;
    float bouncedCD = 2f;
    bool bounced;

    void Start() {
        tintControl = GetComponentInChildren<TintControl>();
        mController = GetComponent<MonsterController>();
        pController = FindObjectOfType<PlayerController>();
        contact = mController.detectLayer;
    }

    void Update() {
        healthPoints = Mathf.Clamp(healthPoints, 0, 100);

        if (healthPoints <= 0) {
            Death();
        }
    }

    void FixedUpdate() {
        playerOnTop = Physics2D.BoxCast(gameObject.transform.position + new Vector3(XAxisOffset, YAxisOffset), new Vector2(boxCastXAxis, boxCastYAxis), 0, Vector2.up, 0.1f, contact);
        PlayerHitsTop();
    }

    IEnumerator HeadBounceCD() {
        bounced = true;
        yield return new WaitForSeconds(bouncedCD);
        bounced = false;
    }
    void PlayerHitsTop() {
        if (playerOnTop && !bounced && canBeBounced) {
            Damage(50);
            StartCoroutine(HeadBounceCD());
            pController.StartCoroutine("walkTimer");
            pController.playerRB.AddForce(Vector2.up * headBounceForce, ForceMode2D.Impulse);
        }
    }

    public void Damage(int d) {
        healthPoints -= d;
        tintControl.Damage();
    }
    void Death() {
        Destroy(gameObject);
        print("Monster defeated!");
    }

    void OnDrawGizmos() {
        if (playerOnTop) {
            Gizmos.color = Color.red;
            Gizmos.DrawWireCube(gameObject.transform.position + new Vector3(XAxisOffset, YAxisOffset), new Vector3(boxCastXAxis, boxCastYAxis));
        }
        else {
            Gizmos.color = Color.white;
            Gizmos.DrawWireCube(gameObject.transform.position + new Vector3(XAxisOffset, YAxisOffset), new Vector3(boxCastXAxis, boxCastYAxis));
        }
    }
}
