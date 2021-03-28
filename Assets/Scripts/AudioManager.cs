using System;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;

    public static AudioManager instance;

    private void Awake() {
        if (instance == null) {
            instance = this;
        } else {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(this);

        foreach (Sound sound in sounds) {
            sound.source = gameObject.AddComponent<AudioSource>();
            sound.source.clip = sound.clip;
            sound.source.volume = sound.volume;
            sound.source.loop = sound.loop;
            // sound.source.pitch = sound.pitch;
        }
    }

    private void Start() {
        Play("Theme");
    }

    public void Play(string name) {
        Sound requestedSound = getSoundByName(name);

        if (requestedSound == null) {
            Debug.LogWarning(requestedSound + " does not matches any sound!");
            return ;
        }
        
        requestedSound.source.Play();
    }

    public Sound getSoundByName(string name) {
        return Array.Find(sounds, sound => sound.name == name);
    }
}
