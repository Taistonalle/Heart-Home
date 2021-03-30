using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttacks : MonoBehaviour {
    GameObject player;
    PlayerController pController;
    MonsterManager mManager;
    KorentoManager kManager;
    PollenManager pollenManager;
    Vector2 midPoint;
    RaycastHit2D midRay;
    public LayerMask collisionMask;
    [Range(0.5f, 3f)] public float attackRange = 1f;
    [Range(1f, 5f)] public float lightAttackForce = 1f;
    [Range(10, 20)] public int lightAttackDMG = 10;
    [Range(5f, 10f)] public float heavyAttackForce = 5f;
    [Range(30, 50)] public int heavyAttackDMG = 30;
    [Range(0.1f, 1f)] public float heavyDMGBuildUpSpeed = 1f;
    public int defaultHeavyDMG;
    public float defaultHeavyAForce;
    public bool canDmgBuildUp = true;
    void Start() {
        player = GameObject.FindGameObjectWithTag("Player");
        pController = player.GetComponent<PlayerController>();
        midRay = Physics2D.Raycast(midPoint, Vector2.right, attackRange, collisionMask);
        defaultHeavyDMG = heavyAttackDMG;
        defaultHeavyAForce = heavyAttackForce;
    }

    void Update() {
        midPoint = new Vector2(player.transform.position.x, player.transform.position.y);
        heavyAttackForce = Mathf.Clamp(heavyAttackForce, 5f, 10f);
        lightAttackDMG = Mathf.Clamp(lightAttackDMG, 10, 20);
        heavyAttackDMG = Mathf.Clamp(heavyAttackDMG, 30, 50);
    }

    void FixedUpdate() {
        RayFromMiddle();
    }

    void RayFromMiddle() {
        if (pController.facingRight) {
            midRay = Physics2D.Raycast(midPoint, Vector2.right, attackRange, collisionMask);
        }
        else if (pController.facingLeft) {
            midRay = Physics2D.Raycast(midPoint, Vector2.left, attackRange, collisionMask);
        }

        if (midRay && pController.facingRight) {
            Debug.DrawRay(midPoint, Vector2.right * attackRange, Color.red);
        }
        else {
            Debug.DrawRay(midPoint, Vector2.right * attackRange, Color.white);
        }

        if (midRay && pController.facingLeft) {
            Debug.DrawRay(midPoint, Vector2.left * attackRange, Color.red);
        }
        else {
            Debug.DrawRay(midPoint, Vector2.left * attackRange, Color.white);
        }
    }

    void MManagerAndLightDMG() {
        if (midRay.rigidbody.tag == "Monster") {
            mManager = midRay.rigidbody.GetComponent<MonsterManager>();
            mManager.Damage(lightAttackDMG);
        }
        else if (midRay.rigidbody.tag == "Korento") {
            kManager = midRay.rigidbody.GetComponent<KorentoManager>();
            kManager.Damage(lightAttackDMG);
        }
        else if (midRay.rigidbody.tag == "Pollen") {
            pollenManager = midRay.rigidbody.GetComponent<PollenManager>();
            pollenManager.Damage(lightAttackDMG);
        }
    }
    
    void MManagerAndHeavyDMG() {
        if (midRay.rigidbody.tag == "Monster") {
            mManager = midRay.rigidbody.GetComponent<MonsterManager>();
            mManager.Damage(heavyAttackDMG);
        }
        else if (midRay.rigidbody.tag == "Korento") {
            kManager = midRay.rigidbody.GetComponent<KorentoManager>();
            kManager.Damage(heavyAttackDMG);
        }
    }
    
    public void LightAttack() {
        var upRight = new Vector2 (1f,1f);
        var upLeft = new Vector2(-1f,1f);

        if (midRay.rigidbody && pController.facingRight) {
            //Play attack animation here
            MManagerAndLightDMG();
            midRay.rigidbody.AddForce(upRight * lightAttackForce, ForceMode2D.Impulse);
        }
        else if(!midRay.rigidbody && pController.facingRight) {
            //Play attack animation
            Debug.LogWarning("No target in range(right)");
        }

        if (midRay.rigidbody && pController.facingLeft) {
            //Play attack animation here
            MManagerAndLightDMG();
            midRay.rigidbody.AddForce(upLeft * lightAttackForce, ForceMode2D.Impulse);
        }
        else if(!midRay.rigidbody && pController.facingLeft) {
            //Play attack animation
            Debug.LogWarning("No target in range(left)");
        }
    }
    
    public IEnumerator HeavyDMGBuildUp() {
        heavyAttackDMG += 1;
        heavyAttackForce += 0.2f;
        canDmgBuildUp = false;
        yield return new WaitForSeconds(heavyDMGBuildUpSpeed);
        canDmgBuildUp = true;
    }

    public void HeavyAttack() {
        var upRight = new Vector2(1f, 1f);
        var upLeft = new Vector2(-1f, 1f);

        if (midRay.rigidbody && pController.facingRight) {
            //Play attack animation here
            MManagerAndHeavyDMG();
            midRay.rigidbody.AddForce(upRight * heavyAttackForce, ForceMode2D.Impulse);
        }
        else if (!midRay.rigidbody && pController.facingRight) {
            //Play attack animation
            Debug.LogWarning("No target in range(right)");
        }

        if (midRay.rigidbody && pController.facingLeft) {
            //Play attack animation here
            MManagerAndHeavyDMG();
            midRay.rigidbody.AddForce(upLeft * heavyAttackForce, ForceMode2D.Impulse);
        }
        else if (!midRay.rigidbody && pController.facingLeft) {
            //Play attack animation
            Debug.LogWarning("No target in range(left)");
        }
    }
    
}
