using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TintControl : MonoBehaviour {
    public float tintAmount;
    public Material tintMat;
    [ColorUsageAttribute(true,true)]
    public Color dmgColor;
    [Range(1f, 10f)] public float blinkSpeed;

    void Update() {
        tintAmount = Mathf.Lerp(tintAmount, 0, Time.deltaTime * blinkSpeed);
        tintAmount = Mathf.Clamp(tintAmount, 0, 1);
        tintMat.SetFloat("_TintAmount", tintAmount);
    }

    public void Damage() {
        tintMat.SetColor("_TintColor", dmgColor);
        tintAmount += 1;
    }
}
