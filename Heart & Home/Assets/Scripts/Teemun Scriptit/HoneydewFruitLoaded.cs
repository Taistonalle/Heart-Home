using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoneydewFruitLoaded : MonoBehaviour, ILevelLoad {
    SaveLoadManager saveLoadManager;
    public string fruitSaveDataKey;
    public GameObject fruit;

    public void OnLevelLoad() {
        bool rockData = fruit.activeSelf;
        if (saveLoadManager.ContainsString(fruitSaveDataKey)) {
            rockData = saveLoadManager.GetString(fruitSaveDataKey) == "true";
        }
        fruit.SetActive(rockData);
    }

    public void OnLevelUnload() {
        saveLoadManager.SetString(fruitSaveDataKey, fruit.activeSelf ? "true" : "false");
    }

    void Awake() {
        saveLoadManager = FindObjectOfType<SaveLoadManager>();
    }
}
