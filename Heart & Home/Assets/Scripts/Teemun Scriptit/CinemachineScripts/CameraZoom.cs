using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraZoom : MonoBehaviour {
    CinemachineVirtualCamera cineCam;
    [Range(2f, 6f)] public float zoomAmount;
    void Start() {
        cineCam = FindObjectOfType<CinemachineVirtualCamera>();

        cineCam.m_Lens.OrthographicSize = zoomAmount;
    }
}
