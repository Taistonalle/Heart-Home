using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadFromSave : MonoBehaviour, ILevelLoad {
    SaveLoadManager saveLoadManager;
    public string rockSaveDataKey, newRockSaveDataKey, triggerSaveData;
    public GameObject rock, newRock, trigger;

    public void OnLevelLoad() {
        bool rockData = rock.activeSelf;
        if (saveLoadManager.ContainsString(rockSaveDataKey)) {
            rockData = saveLoadManager.GetString(rockSaveDataKey) == "true";
        }
        rock.SetActive(rockData);

        bool newRockData = newRock.activeSelf;
        if (saveLoadManager.ContainsString(newRockSaveDataKey)) {
            newRockData = saveLoadManager.GetString(newRockSaveDataKey) == "true";
        }
        newRock.SetActive(newRockData);

        bool triggerData = trigger.activeSelf;
        if (saveLoadManager.ContainsString(triggerSaveData)) {
            triggerData = saveLoadManager.GetString(triggerSaveData) == "true";
        }
        trigger.SetActive(triggerData);
    }

    public void OnLevelUnload() {
        saveLoadManager.SetString(rockSaveDataKey, rock.activeSelf ? "true" : "false");
        saveLoadManager.SetString(newRockSaveDataKey, newRock.activeSelf ? "true" : "false");
        saveLoadManager.SetString(triggerSaveData, trigger.activeSelf ? "true" : "false");
    }

    void Awake() {
        saveLoadManager = FindObjectOfType<SaveLoadManager>();
    }
}
