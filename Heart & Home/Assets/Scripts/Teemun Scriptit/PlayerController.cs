using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    Rigidbody2D playerRB;
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
    bool jump;
    float deadzone = 0.1f;

    //Camera stuff
    //[Range(0f, 3f)]float cameraOffSet;
    //Vector3 camPos;
    //Vector3 newCamPos;

    IEnumerator walkTimer() {
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

    void Start() {
        playerRB = GetComponent<Rigidbody2D>();
        glideGravity = gravity / 3;
    }

    void Update() {
        //camPos = new Vector3(playerRB.position.x, playerRB.position.y, -10);
        //newCamPos = new Vector2(cameraOffSet, 0);
        if (Input.GetButtonDown("Jump") && grounded) {
            jump = true;
        }

        if (grounded) {
            dashCount = 1;
        }

        dash = Input.GetKey(KeyCode.LeftShift) ? true : false;
        gravity = Input.GetButton("Jump") ? glideGravity :  9.81f; 
                                                                                     
    }

    void FixedUpdate() {
        var horizontalInput = Vector2.right * Input.GetAxis("Horizontal");
        var newVelocity = playerRB.velocity + horizontalInput * horizontalAccel * Time.deltaTime;
        newVelocity.x = Mathf.Clamp(newVelocity.x, -horizontalMaxSpeed, horizontalMaxSpeed);

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
            playerRB.velocity = newVelocity + new Vector2(0, grounded ? 0 : -gravity * Time.deltaTime); // Uusi Vector2 lisää custom gravityn velocity.y kohtaan.
                                                                                                        // Jos maassa, antaa arvon nolla, muuten miinustaa custom gravityn y akseliin.
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

    }
}
