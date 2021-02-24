using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttacks : MonoBehaviour {
    GameObject player;
    Vector2 midPoint;
    RaycastHit2D midRay;
    public LayerMask collisionMask;
    public bool facingLeft, facingRight = true;
    [Range(0.5f, 3f)] public float attackRange = 1f;
    [Range(1f, 5f)] public float lightAttackForce = 1f;
    [Range(5f, 10f)] public float heavyAttackForce = 5f;

    void Start() {
        player = GameObject.FindGameObjectWithTag("Player");
        midRay = Physics2D.Raycast(midPoint, Vector2.right, attackRange, collisionMask);
    }

    void Update() {
        midPoint = new Vector2(player.transform.position.x, player.transform.position.y);
        heavyAttackForce = Mathf.Clamp(heavyAttackForce, 5f, 10f);
    }

    void FixedUpdate() {
        RayFromMiddle();
    }

    void RayFromMiddle() {
        //var midPoint = new Vector2(player.transform.position.x, player.transform.position.y);
        if (facingRight) {
            midRay = Physics2D.Raycast(midPoint, Vector2.right, attackRange, collisionMask);
        }
        else if (facingLeft) {
            midRay = Physics2D.Raycast(midPoint, Vector2.left, attackRange, collisionMask);
        }

        if (midRay && facingRight) {
            Debug.DrawRay(midPoint, Vector2.right * attackRange, Color.red);
        }
        else {
            Debug.DrawRay(midPoint, Vector2.right * attackRange, Color.white);
        }

        if (midRay && facingLeft) {
            Debug.DrawRay(midPoint, Vector2.left * attackRange, Color.red);
        }
        else {
            Debug.DrawRay(midPoint, Vector2.left * attackRange, Color.white);
        }
    }

    public void LightAttack() {
        var upRight = new Vector2 (1f,1f);
        var upLeft = new Vector2(-1f,1f);

        if (midRay.rigidbody && facingRight) {
            //Play attack animation here
            midRay.rigidbody.AddForce(upRight * lightAttackForce, ForceMode2D.Impulse);
        }
        else if(!midRay.rigidbody && facingRight) {
            //Play attack animation
            Debug.LogWarning("No target in range(right)");
        }

        if (midRay.rigidbody && facingLeft) {
            //Play attack animation here
            midRay.rigidbody.AddForce(upLeft * lightAttackForce, ForceMode2D.Impulse);
        }
        else if(!midRay.rigidbody && facingLeft) {
            //Play attack animation
            Debug.LogWarning("No target in range(left)");
        }
    }

    public void HeavyAttack() {
        var upRight = new Vector2(1f, 1f);
        var upLeft = new Vector2(-1f, 1f);

        if (midRay.rigidbody && facingRight) {
            //Play attack animation here
            midRay.rigidbody.AddForce(upRight * heavyAttackForce, ForceMode2D.Impulse);
        }
        else if (!midRay.rigidbody && facingRight) {
            //Play attack animation
            Debug.LogWarning("No target in range(right)");
        }

        if (midRay.rigidbody && facingLeft) {
            //Play attack animation here
            midRay.rigidbody.AddForce(upLeft * heavyAttackForce, ForceMode2D.Impulse);
        }
        else if (!midRay.rigidbody && facingLeft) {
            //Play attack animation
            Debug.LogWarning("No target in range(left)");
        }
    }
}
