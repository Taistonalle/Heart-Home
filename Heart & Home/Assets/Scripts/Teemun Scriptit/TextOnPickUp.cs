using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextOnPickUp : MonoBehaviour {
    Text infoText;
    public float infoTextTime;
    void Start() {
        infoText = GetComponent<Text>();
        infoText.text = "";
    }

    IEnumerator TextTimer() {
        yield return new WaitForSeconds(infoTextTime);
        infoText.text = "";
    }

    public void NewText(string text) {
        infoText.text = text;
        StartCoroutine(TextTimer());
    }
}
