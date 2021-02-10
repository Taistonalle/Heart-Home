using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProtoGrablingHookTesting : MonoBehaviour {
    //Need: transform points(hiiren osoitin?), pull ja löysäys mekaniikka köyteen, mihin koukku voi osua. Köydelle ja koukulle visuaali. Laukauisu napista.
    public Vector2 mousePos;
    PlayerController playerController;
    Rigidbody2D playerRB;
    public float ropeSpeed = 2f;

    void Start() {
        playerController = FindObjectOfType<PlayerController>();
        playerRB = playerController.GetComponent<Rigidbody2D>();
    }

    void Update() {
        mousePos = Camera.main.ScreenToWorldPoint(new Vector2(Input.mousePosition.x, Input.mousePosition.y));
        //if (Input.GetMouseButtonDown(0)) {
        //    print(mousePos);
        //}
    }

    void FixedUpdate() {
        if (Input.GetMouseButton(0)) {
            playerController.gravity = Input.GetMouseButton(0) ? 0 : 9.81f;
            playerRB.position = Vector2.Lerp(playerRB.position, mousePos, ropeSpeed * Time.deltaTime);
        }
    }
}
