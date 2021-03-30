using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioFW : MonoBehaviour {
    // how to use:
    // put sound effects in their own objects under SFX
    // then anywhere in the code, call 'AudioFW.Play(id)'
    // where id is the name of the sound effect object.

    Dictionary<string, AudioSource> sfx = new Dictionary<string, AudioSource>();
    Dictionary<string, AudioSource> loops = new Dictionary<string, AudioSource>();
    Dictionary<string, AudioFWRandomizer> randomSfx = new Dictionary<string, AudioFWRandomizer>();
    Dictionary<string, List<string>> audioEnvs = new Dictionary<string, List<string>>();
    Dictionary<string, float> restoreVolume = new Dictionary<string, float>();
    string currentAudioEnv = "";
    public static void Play(string id) {
        instance.PlayImpl(id);
    }
    public static void PlayLoop(string id) {
        instance.PlayLoopImpl(id);
    }
    public static void StopLoop(string id) {
        instance.StopLoopImpl(id);
    }

    public static void AdjustPitch(string id, float pitch) {
        instance.AdjustPitchImpl(id, pitch);
    }
    public static void AdjustVolume(string id, float volume) {
        instance.AdjustVolumeImpl(id, volume);
    }
    public static void RestoreVolume(string id) {
        instance.RestoreVolumeImpl(id);
    }



    public static void FadeAmbient(string id) {
        instance.FadeAmbientImpl(id);
    }

    void FadeAmbientImpl(string id) {
        if (!audioEnvs.ContainsKey(id)) {
            Debug.LogWarning("No enviroment with ID " + id);
            return;
        } 
        if (id == currentAudioEnv) return;
        if(currentAudioEnv != "") {
            var lastLoops = audioEnvs[currentAudioEnv];
            foreach (var loopId in lastLoops) {
                StopLoopImpl(loopId);
            }
        }
        

        var newLoops = audioEnvs[id];
        foreach (var loopId in newLoops) {
            PlayLoopImpl(loopId);
        }

        currentAudioEnv = id;
    }

    void PlayImpl(string id) {
        if (!sfx.ContainsKey(id)) {
            Debug.LogWarning("No sound with ID " + id);
            return;
        }
        var clip = sfx[id].clip;
        if (randomSfx.ContainsKey(id)) {
            print("randomizing: " + id);
            var clips = randomSfx[id].randomClips;
            if (clips.Length == 0) {
                Debug.LogWarning("Randomizer has no clips to pick from, ID: " + id);
                return;
            }
            clip = clips[Random.Range(0, clips.Length)];
        }
        sfx[id].PlayOneShot(clip);
    }
    void PlayLoopImpl(string id) {
        if (!loops.ContainsKey(id)) {
            Debug.LogWarning("No sound with ID " + id);
            return;
        }
        if (!loops[id].isPlaying) {
            loops[id].Play();
        }
    }
    void StopLoopImpl(string id) {
        if (!loops.ContainsKey(id)) {
            Debug.LogWarning("No sound with ID " + id);
            return;
        }
        loops[id].Stop();
    }
    void AdjustPitchImpl(string id, float pitch) {
        if (!loops.ContainsKey(id)) {
            Debug.LogWarning("No sound with ID " + id);
            return;
        }
        loops[id].pitch = Mathf.Clamp(pitch, -3f, 3f);
        //print("Pitch adjusted");
    }

    void AdjustVolumeImpl(string id, float volume) {
        if (!loops.ContainsKey(id)) {
            Debug.LogWarning("No sound with ID " + id);
            return;
        }
        loops[id].volume = volume;
        //print("Pitch adjusted");
    }
    void RestoreVolumeImpl(string id) {
        if (!loops.ContainsKey(id)) {
            Debug.LogWarning("No sound with ID " + id);
            return;
        }
        loops[id].volume = restoreVolume[id];
        //print("Pitch adjusted");
    }
    static public AudioFW instance {
        get {
            if (!_instance) {
                var a = GameObject.FindObjectsOfType<AudioFW>();
                if (a.Length == 0)
                    Debug.LogWarning("No AudioFW in scene");
                else if (a.Length > 1)
                    Debug.LogWarning("Multiple AudioFW in scene");
                _instance = a[0];
            }
            return _instance;
        }
    }
    static AudioFW _instance;

    void FindAudioSources() {
        var audioSources = transform.Find("SFX").GetComponentsInChildren<AudioSource>();
        foreach (var a in audioSources) {            
            sfx.Add(a.name, a);
            var rand = a.GetComponent<AudioFWRandomizer>();
            if (rand != null) {
                randomSfx.Add(a.name, rand);
            }


        }
        var audioSources2 = transform.Find("Loops").GetComponentsInChildren<AudioSource>();
        foreach (var a in audioSources2) {
            restoreVolume.Add(a.name, a.volume);
            loops.Add(a.name, a);
        }

        var envs = transform.Find("AudioEnv").GetComponentsInChildren<AudioFWEnv>();
        foreach (var a in envs) {
            audioEnvs.Add(a.name, new List<string>(a.loops));
        }

    }

    void Awake() {
        FindAudioSources();
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.A))
            //AudioFW.Play("ScuttleDeath");
            //DebugPrint();

        if (Input.GetKey(KeyCode.Alpha0)) {
            FadeAmbientImpl("Metsä");
        }
        if (Input.GetKey(KeyCode.Alpha9)) {
            FadeAmbientImpl("Juurakko");
        }
    }

    void DebugPrint() {
        string s = "Audio loaded: ";
        foreach (var id in sfx.Keys)
            s += id + " ";
        print(s);
    }
}
