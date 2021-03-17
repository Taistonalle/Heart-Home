using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersonalInventoryLock : MonoBehaviour
{
    public PlayerController controller;

    
    void OnEnable() {
        //controller.enabled = false;
        Time.timeScale = 0f;
    }

    void OnDisable() {
        //controller.enabled = true;
        Time.timeScale = 1f;
    }
}
