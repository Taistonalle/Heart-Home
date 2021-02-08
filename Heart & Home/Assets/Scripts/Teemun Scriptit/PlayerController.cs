using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    Rigidbody2D playerRB;
    [Range(0.1f, 10f)] public float jumpForce = 2f;
    [Range(0.1f, 10f)] public float groundFrictionWhenNoInput = 2f;
    [Range(0.1f, 10f)] public float airFrictionWhenNoInput = 2f;
    [Range(1f, 20f)] public float horizontalAccel = 1;
    [Range(1f, 30f)] public float horizontalMaxSpeed = 5;
    public bool grounded;
    bool jump;
    float deadzone = 0.1f;

    //Camera stuff
    //[Range(0f, 3f)]float cameraOffSet;
    //Vector3 camPos;
    //Vector3 newCamPos;

    void Start() {
        playerRB = GetComponent<Rigidbody2D>();
    }

    void Update() {
        //camPos = new Vector3(playerRB.position.x, playerRB.position.y, -10);
        //newCamPos = new Vector2(cameraOffSet, 0);

        if (Input.GetButtonDown("Jump") && grounded) {
            jump = true;
        }
       
        //jump = Input.GetButtonDown("Jump");
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


        playerRB.velocity = newVelocity;
    }
}
