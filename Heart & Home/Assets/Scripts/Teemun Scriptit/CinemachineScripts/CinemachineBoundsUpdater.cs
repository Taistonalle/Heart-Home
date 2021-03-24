using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CinemachineBoundsUpdater : MonoBehaviour {
    Collider2D boundsCollider;
    CinemachineVirtualCamera cineCam;
    CinemachineConfiner cineBounds;
    void Start() {
        boundsCollider = GetComponent<PolygonCollider2D>();
        cineCam = FindObjectOfType<CinemachineVirtualCamera>();
        cineBounds = cineCam.GetComponent<CinemachineConfiner>();

        cineBounds.m_BoundingShape2D = boundsCollider;
    }

    //void OnTriggerEnter2D(Collider2D collision) {
    //    cineBounds.m_BoundingShape2D = boundsCollider;
    //}


}
