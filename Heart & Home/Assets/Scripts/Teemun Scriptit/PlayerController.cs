using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerAttacks))]
public class PlayerController : MonoBehaviour {

    const float skinWidth = 0.015f;
    //public int horizontalRayCount = 4; 
    //public int verticalRayCount = 4;
    float horizontalRaySpacing, verticalRaySpacing;

    BoxCollider2D collider;
    RaycastOrigins raycastOrigins;
    public LayerMask collisionMask;

    public Rigidbody2D playerRB;
    Vector2 scaleChange;
    PlayerAttacks playerAttacks;
    [Range(0.1f, 10f)] public float jumpForce = 2f;
    [Range(10, 100f)] public float dashForce = 10f;
    [Range(1f, 5f)] public float dashCD = 2f;
    [Range(0.1f, 10f)] public float groundFrictionWhenNoInput = 2f;
    [Range(0.1f, 10f)] public float airFrictionWhenNoInput = 2f;
    [Range(1f, 20f)] public float horizontalAccel = 1;
    [Range(1f, 30f)] public float horizontalMaxSpeed = 5;
    public int dashCount = 1;
    public float gravity = 9.81f;
    public bool grounded;
    float glideGravity;
    public bool dash;
    public bool canMoveNormally = true;
    public bool canDash = true;
    public bool facingRight = true, facingLeft;
    bool jump;
    float deadzone = 0.1f;

    float attackCooldownTimer = -1;
    public float lightAttackCooldown = 1f;
    public float heavyAttackCooldown = 1.5f;


    //Poistin animaatiomanagerin viittauksen. Ainoastaan animoitava objekti liitetään skriptiin.
    public GameObject animPlayer;
    Animator animator;
    string currentState;

    //

    //Camera stuff
    //[Range(0f, 3f)]float cameraOffSet;
    //Vector3 camPos;
    //Vector3 newCamPos;

    void UpdateRaycastOrigins() {
        Bounds bounds = collider.bounds;
        bounds.Expand(skinWidth * -2);

        raycastOrigins.bottomLeft = new Vector2(bounds.min.x, bounds.min.y);
        raycastOrigins.bottomRight = new Vector2(bounds.max.x, bounds.min.y);
        raycastOrigins.topLeft = new Vector2(bounds.min.x, bounds.max.y);
        raycastOrigins.topRight = new Vector2(bounds.max.x, bounds.max.y);
    }

    void CalculateRaySpacing() {
        Bounds bounds = collider.bounds;
        bounds.Expand(skinWidth * -2);

        //horizontalRayCount = Mathf.Clamp(horizontalRayCount, 2, int.MaxValue);
        //verticalRayCount = Mathf.Clamp(verticalRayCount, 2, int.MaxValue);

        horizontalRaySpacing = bounds.size.x;
        verticalRaySpacing = bounds.size.y;
    }

    //void HorizontalCollisions() {
    //    var velocityY = playerRB.velocity.y;
    //    var velocityX = playerRB.velocity.x;
    //    float dirX = Mathf.Sign(velocityX);
    //    float rayLenght = Mathf.Abs(velocityX) + skinWidth;

    //    for (int i = 0; i < horizontalRayCount; ++i) {
    //        var rayOrigin = (dirX == -1) ? raycastOrigins.bottomLeft : raycastOrigins.bottomRight;
    //        rayOrigin += Vector2.up * (horizontalRaySpacing * i);
    //        RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.right * dirX, rayLenght, collisionMask);

    //        Debug.DrawRay(rayOrigin, Vector2.right * dirX * rayLenght, Color.red);

    //        if (hit) {
    //            velocityX = (hit.distance - skinWidth) * dirX;
    //            rayLenght = hit.distance;
    //        }

    //    }
    //}

    //void VerticalCollisions() {
    //    var velocityY = playerRB.velocity.y;
    //    var velocityX = playerRB.velocity.x;
    //    float dirY = Mathf.Sign(velocityY);
    //    float rayLenght = Mathf.Abs(velocityY) + skinWidth;

    //    for (int i = 0; i < verticalRayCount; ++i) {
    //        var rayOrigin = (dirY == -1) ? raycastOrigins.bottomLeft : raycastOrigins.topLeft;
    //        rayOrigin += Vector2.right * (verticalRaySpacing * i + velocityX);
    //        RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.up * dirY, rayLenght, collisionMask);

    //        Debug.DrawRay(rayOrigin, Vector2.up * dirY * rayLenght, Color.red);

    //        if (hit) {
    //            velocityY = (hit.distance - skinWidth) * dirY;
    //            rayLenght = hit.distance;
    //        }

    //    }
    //}

    void GroundCollision() {
        float rayLenght = 0.3f;
        Vector2 rayOrigin1 = raycastOrigins.bottomLeft;
        Vector2 rayOrigin2 = rayOrigin1 + Vector2.right * (verticalRaySpacing / 3);
        Vector2 rayOrigin3 = rayOrigin1 + Vector2.right * (verticalRaySpacing / 1.5f);
        Vector2 rayOrigin4 = rayOrigin1 + Vector2.right * verticalRaySpacing;

        RaycastHit2D ray1 = Physics2D.Raycast(rayOrigin1, Vector2.down, rayLenght + skinWidth, collisionMask);
        RaycastHit2D ray2 = Physics2D.Raycast(rayOrigin2, Vector2.down, rayLenght + skinWidth, collisionMask);
        RaycastHit2D ray3 = Physics2D.Raycast(rayOrigin3, Vector2.down, rayLenght + skinWidth, collisionMask);
        RaycastHit2D ray4 = Physics2D.Raycast(rayOrigin4, Vector2.down, rayLenght + skinWidth, collisionMask);

        if (ray1) {
            if(ray1 || ray2 || ray3 || ray4) {
                grounded = true;
            }
            Debug.DrawRay(rayOrigin1, Vector2.down * rayLenght, Color.red);
        }
        else {
            Debug.DrawRay(raycastOrigins.bottomLeft, Vector2.down * rayLenght, Color.white);
        }

        if (ray2) {
            Debug.DrawRay(rayOrigin2, Vector2.down * rayLenght, Color.red);
        }
        else {
            Debug.DrawRay(rayOrigin2, Vector2.down * rayLenght, Color.white);
        }

        if (ray3) {
            Debug.DrawRay(rayOrigin3, Vector2.down * rayLenght, Color.red);
        }
        else {
            Debug.DrawRay(rayOrigin3, Vector2.down * rayLenght, Color.white);
        }

        if (ray4) {
            if (ray4 || ray1 || ray2 || ray3) {
                grounded = true;
            }
            Debug.DrawRay(rayOrigin4, Vector2.down * rayLenght, Color.red);
        }
        else {
            Debug.DrawRay(rayOrigin4, Vector2.down * rayLenght, Color.white);
        }
        // Y pos fix, mikä ei toiminu oikein hyvin ollenkaan alkuun. Nyt jotenkin
        if (grounded && ray1 && ray1.distance < 0.05f) {
            playerRB.position = playerRB.position += new Vector2(0f, 0.03f);
            Debug.Log("Ray1 Fixed player position");
        }
        else if (grounded && ray4 && ray4.distance < 0.05f) {
            playerRB.position = playerRB.position += new Vector2(0f, 0.03f);
            Debug.Log("Ray 4 Fixed player position");
        }

        if (!ray1 && !ray2 && !ray3 && !ray4) {
            grounded = false;
        }

    }

    struct RaycastOrigins {
        public Vector2 topLeft, topRight, bottomLeft, bottomRight;
    }

    public IEnumerator walkTimer() {
        canMoveNormally = false;
        yield return new WaitForSeconds(0.3f);
        canMoveNormally = true;
    }
    IEnumerator dashTimer() {
        canDash = false;
        yield return new WaitForSeconds(dashCD);
        canDash = true;
    }

    void Dash() { //Monster funktion, i know... i know..
        var horiInput = Vector2.right * Input.GetAxis("Horizontal");
        var vertiInput = Vector2.up * Input.GetAxis("Vertical");

        var right = new Vector2(1f, 0f);
        var upRight = new Vector2(1f, 1f);
        var downRight = new Vector2(1f, -1f);

        var left = new Vector2(-1f, 0f);
        var upLeft = new Vector2(-1f, 1f);
        var downLeft = new Vector2(-1f, -1f);

        var up = new Vector2(0f, 1f);
        var down = new Vector2(0f, -1f);

        if (horiInput.x > 0 && vertiInput.y > 0 && canDash && dashCount == 1) {
            print("Dash up right");
            dashCount--;
            StartCoroutine(dashTimer());
            StartCoroutine(walkTimer());
            playerRB.AddForce(upRight * dashForce, ForceMode2D.Impulse);
        }
        //else if (horiInput.x > 0 && vertiInput.y < 0 && canDash) {
        //    print("Dash down right");
        //    StartCoroutine(dashTimer());
        //    StartCoroutine(walkTimer());
        //    playerRB.AddForce(downRight * dashForce, ForceMode2D.Impulse);
        //}
        else if (horiInput.x < 0 && vertiInput.y > 0 && canDash && dashCount == 1) {
            print("Dash up left");
            dashCount--;
            StartCoroutine(dashTimer());
            StartCoroutine(walkTimer());
            playerRB.AddForce(upLeft * dashForce, ForceMode2D.Impulse);
        }
        //else if (horiInput.x < 0 && vertiInput.y < 0 && canDash) {
        //    print("Dash down left");
        //    StartCoroutine(dashTimer());
        //    StartCoroutine(walkTimer());
        //    playerRB.AddForce(downLeft * dashForce, ForceMode2D.Impulse);
        //}
        else if (horiInput.x > 0 && canDash && dashCount == 1) {
            print("Dash right");
            dashCount--;
            StartCoroutine(dashTimer());
            StartCoroutine(walkTimer());
            playerRB.AddForce(right * dashForce, ForceMode2D.Impulse);
        }
        else if (horiInput.x < 0 && canDash && dashCount == 1) {
            print("Dash left");
            dashCount--;
            StartCoroutine(dashTimer());
            StartCoroutine(walkTimer());
            playerRB.AddForce(left * dashForce, ForceMode2D.Impulse);
        }
        else if (vertiInput.y > 0 && canDash && dashCount == 1) {
            print("Dash up");
            dashCount--;
            StartCoroutine(dashTimer());
            StartCoroutine(walkTimer());
            playerRB.AddForce(up * dashForce, ForceMode2D.Impulse);
        }
        //else if (vertiInput.y < 0 && canDash) {
        //    print("Dash down");
        //    StartCoroutine(dashTimer());
        //    StartCoroutine(walkTimer());
        //    playerRB.AddForce(down * dashForce, ForceMode2D.Impulse);
        //}
    }

    void ScaleChanger() {
        var horiInput = Vector2.right * Input.GetAxis("Horizontal");

        if(horiInput.x > 0) {
            scaleChange = new Vector2(1f, 1f);
            facingRight = true;
            facingLeft = false;
        }
        else if(horiInput.x < 0) {
            scaleChange = new Vector2(-1f, 1f);
            facingLeft = true;
            facingRight = false;
        }
    }

    void Start() {
        playerAttacks = GetComponent<PlayerAttacks>();
        playerRB = GetComponent<Rigidbody2D>();
        collider = GetComponent<BoxCollider2D>();
        CalculateRaySpacing();
        glideGravity = gravity / 3;
        scaleChange = transform.localScale;

        animator = animPlayer.GetComponent<Animator>();
        ChangeAnimationState("Silkie_Idle");
        
    }

    void Update() {
        attackCooldownTimer -= Time.deltaTime;
        UpdateRaycastOrigins();
        ScaleChanger();
        transform.localScale = scaleChange;
        //camPos = new Vector3(playerRB.position.x, playerRB.position.y, -10);
        //newCamPos = new Vector2(cameraOffSet, 0);
        if (Input.GetButtonDown("Jump") && grounded) {
            jump = true;
        }

        if (grounded) {
            dashCount = 1;
            gravity = 0f;
        }
        else if (!grounded) {
            gravity = Input.GetButton("Jump") ? glideGravity : 9.81f;
        }

        dash = Input.GetKey(KeyCode.LeftShift) || Input.GetButton("Fire1") ? true : false; //Fire1 nappi toimii ainakin PS4 ohjaimen "R1" nappina
                                                                                           //gravity = Input.GetButton("Jump") ? glideGravity :  9.81f; 

        if (Input.GetKeyDown(KeyCode.V)) { //PlaceHolder napit hyökkäyksille
            if (attackCooldownTimer < 0) {
                attackCooldownTimer = lightAttackCooldown;
                ChangeAnimationState("Silkie_Attack");
                playerAttacks.LightAttack();
            }
        }
        else if (Input.GetKey(KeyCode.B)) {
            playerAttacks.heavyAttackForce += Time.deltaTime;
        }
        else if (Input.GetKeyUp(KeyCode.B)) {
            playerAttacks.HeavyAttack();
            playerAttacks.heavyAttackForce = 5f;
        }
        if (attackCooldownTimer > 0) {
            // attack still going, don't change animation
        }else if (!grounded) {
            if (playerRB.velocity.y > 0) {
                ChangeAnimationState("Silkie_Jump");
            } else {
                ChangeAnimationState("Silkie_Falling");
            }

        }
        else if(Mathf.Abs(Input.GetAxis("Horizontal")) > 0.1f) {            
            ChangeAnimationState("Silkie_Run");
        } else {
            ChangeAnimationState("Silkie_Idle");
        }

    }

    void FixedUpdate() {
        GroundCollision();
        var horizontalInput = Vector2.right * Input.GetAxis("Horizontal");
        var newVelocity = playerRB.velocity + horizontalInput * horizontalAccel * Time.deltaTime;
        newVelocity.x = Mathf.Clamp(newVelocity.x, -horizontalMaxSpeed, horizontalMaxSpeed);
        
        if(Input.GetAxis("Horizontal") > 0 && grounded) {
        }
        //Friction / kitka
        if(Mathf.Abs(horizontalInput.x) < deadzone) {
            newVelocity.x = Mathf.Lerp(newVelocity.x, 0, Time.deltaTime * (grounded ? groundFrictionWhenNoInput : airFrictionWhenNoInput));
        }
        if (jump) {
            newVelocity.y = jumpForce;
            jump = false;
            //playerRB.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }

        if (dash) {
            Dash();
        }

        if (canMoveNormally) {
            playerRB.velocity = newVelocity + new Vector2(0, grounded ? 0 : -gravity * Time.deltaTime); // Uusi Vector2 lisää custom gravityn velocity.y kohtaan.                                                                                       // Jos maassa, antaa arvon nolla, muuten miinustaa custom gravityn y akseliin.
        }


        //Camera manipulation ei toiminut hyvin
        //if(horizontalInput.x > 0) {
        //    cameraOffSet += Time.deltaTime;
        //    Camera.main.transform.position += newCamPos * Time.deltaTime;
        //}
        //else if(horizontalInput.x < 0) {
        //    cameraOffSet -= Time.deltaTime;
        //    Camera.main.transform.position -= newCamPos * Time.deltaTime;
        //}
        //else {
        //    Camera.main.transform.position = camPos;
        //if (cameraOffSet > 0) {
        //    cameraOffSet -= Time.deltaTime;
        //    if (cameraOffSet == 0) {
        //        Camera.main.transform.position = camPos;
        //    }
        //}
        //else if(cameraOffSet < 0) {
        //    cameraOffSet += Time.deltaTime;
        //    if(cameraOffSet == 0) {
        //        Camera.main.transform.position = camPos;
        //    }
        //}
        //}
        //Määrittää sen hetkisen animation staten.
        //Pyörittää stateen liitetyn animaation.
    }

    private void ChangeAnimationState(string newState) {
        if(currentState == newState) {
            return;
        }

        animator.Play(newState);
        currentState = newState;
    }
}
