using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PollenManager : MonoBehaviour {
    //PollenController pollenController;
    RaycastHit2D playerOnTop;
    PlayerController pController;
    //AudioSource audioSource;
    //public GameObject dropPrefab;
    //public AudioClip deathSound;
    public LayerMask contact;
    TintControl tintControl;
    [Range(0, 100)] public int healthPoints = 100;
    public float boxCastXAxis, boxCastYAxis, XAxisOffset, YAxisOffset, headBounceForce;
    public bool canBeBounced;
    float bouncedCD = 2f;
    bool bounced, dead;

    void Start() {
        tintControl = GetComponentInChildren<TintControl>();
        pController = FindObjectOfType<PlayerController>();
        //pollenController = GetComponent<PollenController>();
        //audioSource = GetComponent<AudioSource>();
    }

    void Update() {
        //dropPrefab.transform.position = transform.position;
        healthPoints = Mathf.Clamp(healthPoints, 0, 100);

        if (healthPoints <= 0 && !dead) {
            Death();
            dead = true;
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
        //Instantiate(dropPrefab);
        //audioSource.Stop();
        //audioSource.PlayOneShot(deathSound);
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
