using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFollower : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject Player;
    public bool followingPlayer;
    public Vector3 startingPosition;
    void Start()
    {
        startingPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (followingPlayer == true) {
            var targetPos = Player.transform.position + new Vector3(0, 0, -10);
            transform.position = targetPos;
        }
    }
}
