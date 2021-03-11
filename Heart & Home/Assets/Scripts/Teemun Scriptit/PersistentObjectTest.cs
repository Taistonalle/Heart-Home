using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersistentObjectTest : MonoBehaviour, ILevelLoad {
    SaveLoadManager saveLoadManager;
    public GameObject onOffTest;
    public string saveDataKey;

    public void OnLevelLoad() {
        bool data = onOffTest.activeSelf;
        if (saveLoadManager.ContainsString(saveDataKey)) {
            data = saveLoadManager.GetString(saveDataKey) == "true";
        }
        onOffTest.SetActive(data);
    }

    public void OnLevelUnload() {
        saveLoadManager.SetString(saveDataKey, onOffTest.activeSelf ? "true" : "false");
    }

    void Awake() {
        saveLoadManager = FindObjectOfType<SaveLoadManager>();
    }

    void OnTriggerEnter2D(Collider2D collider) {
        onOffTest.SetActive(!onOffTest.activeSelf);
    }

    void Update() {

    }
}
