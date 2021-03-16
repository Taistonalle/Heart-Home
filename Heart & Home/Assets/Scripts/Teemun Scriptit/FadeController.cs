using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class FadeController : MonoBehaviour {
    Image image;
    public Color fade;
    [Range(0.01f,0.5f)] public float fadeSpeed;

    void Start() {
        image = GetComponent<Image>();
        fade = image.color;
    }

    public IEnumerator FadeUp() {
        while (fade.a <= 1.1f) {
            fade.a += 0.025f;
            yield return new WaitForSeconds(fadeSpeed);
        }
        StartCoroutine(FadeDown());
    }

    public IEnumerator FadeDown() {
        while (fade.a >= 0) {
            fade.a -= 0.025f;
            yield return new WaitForSeconds(fadeSpeed);
        }
    }

    void Update() {
        image.color = fade;
        if (Input.GetKeyDown(KeyCode.L)) StartCoroutine(FadeUp());
        //if (fade.a >= 1f) StartCoroutine(FadeDown());
    }
}
