using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManipulatorTest : MonoBehaviour {
    PlayerController playerController;
    GameObject player;
    Camera mainCam;
    public float camOffRange, camMoveSpeed = 3f, camPosX;
    public bool movingLeft, movingRight, followingPlayer;
    

    void Start() {
        mainCam = Camera.main;
        player = GameObject.FindGameObjectWithTag("Player");
        playerController = FindObjectOfType<PlayerController>();
        
        mainCam.transform.position = player.transform.position + new Vector3(0f, 0f, -10f);
        camPosX = mainCam.transform.position.x;
    }

    void Update() {
        var horizontalInput = Vector2.right * Input.GetAxis("Horizontal");
        movingRight = horizontalInput.x > 0 ? true : false;
        movingLeft = horizontalInput.x < 0 ? true : false;

        //if (followingPlayer) {
        //    mainCam.transform.position = player.transform.position + new Vector3(0f, 0f, -10f);
        //}
    }

    void FixedUpdate() {
        if (movingRight) {
            mainCam.transform.position += Vector3.right * camMoveSpeed * Time.deltaTime;
            //mainCam.transform.position += newCamPos * camMoveSpeed * Time.deltaTime;
        }
        camPosX = Mathf.Clamp(mainCam.transform.position.x, -camOffRange, camOffRange);

        //else if (movingLeft) {
        //    camOffset -= Time.deltaTime;
        //    mainCam.transform.position -= newCamPos * camMoveSpeed * Time.deltaTime;
        //}

        //else {
        //    Camera.main.transform.position = camPos;
        //    if (cameraOffSet > 0) {
        //        cameraOffSet -= Time.deltaTime;
        //        if (cameraOffSet == 0) {
        //            Camera.main.transform.position = camPos;
        //        }
        //    }
        //    else if (cameraOffSet < 0) {
        //        cameraOffSet += Time.deltaTime;
        //        if (cameraOffSet == 0) {
        //            Camera.main.transform.position = camPos;
        //        }
        //    }
        //}
    }
}
