using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFollower : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject Player;
    public bool followingPlayer;
    public Vector3 startingPosition;
    public Vector3 offset;
    public float zoomSpeed = 4.0f;
    public float minZoom = 5f;
    public float maxZoom = 15f;
    private float currentzoom = 10f;
    void Start()
    {
        startingPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        currentzoom -= Input.GetAxis("Mouse ScrollWheel") * zoomSpeed;
        currentzoom = Mathf.Clamp(currentzoom, minZoom, maxZoom);
    }
    private void LateUpdate() {
        if (followingPlayer == true) {
            transform.position = Player.transform.position - offset * currentzoom;
        }
    }
}
