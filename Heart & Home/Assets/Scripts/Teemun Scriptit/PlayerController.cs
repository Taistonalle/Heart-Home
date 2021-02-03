using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    Rigidbody2D playerRB;
    [SerializeField] bool grounded = false;
    [SerializeField] bool jumpInput = false;
    //[Range(0.1f, 10f)] public float speed = 1f;
    [Range(0.1f, 10f)] public float jumpForce = 2f;
    [Range(0.1f, 10f)] public float groundFrictionWhenNoInput = 2f;
    [Range(0.1f, 10f)] public float airFrictionWhenNoInput = 2f;
    [Range(1f, 20f)] public float horizontalAccel = 1;
    [Range(1f, 30f)] public float horizontalMaxSpeed = 5;
    bool jump;
    float deadzone = 0.1f;

    void Start() {
        playerRB = GetComponent<Rigidbody2D>();
    }

    void Update() {
        if (Input.GetButtonDown("Jump")) {
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
       

        playerRB.velocity = newVelocity;
    }
}
