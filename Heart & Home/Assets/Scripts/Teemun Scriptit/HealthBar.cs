using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    Slider slider;
    PlayerManager playerManager;

    void Start() {
        slider = GetComponent<Slider>();
        playerManager = FindObjectOfType<PlayerManager>();
    }

    public void SetHealth() {
        slider.value = playerManager.healthPoints;
    }
}
