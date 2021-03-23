using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectedUI : MonoBehaviour
{
    public bool selected;
    Vector3 originalScale;
    Vector3 selectedScale = new Vector3(1.2f, 1.2f, 1f);
    public bool activated;


    private void Awake() {
        originalScale = transform.localScale;
    }
    private void Update() {
        if (selected) {
            transform.localScale = selectedScale;
        } else {
            transform.localScale = originalScale;
            if (activated == true) {
                activated = false;
            }
        }
    }

}
