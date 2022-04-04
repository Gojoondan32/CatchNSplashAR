using UnityEngine.Audio;
using UnityEngine;
using System;

public class AudioManager : MonoBehaviour
{
    [System.Serializable]
    public class Sound
    {
        public string name;
        public AudioClip audioClip;
        [Range(0f, 1f)]
        public float volume;
        [Range(0f, 1f)]
        public float pitch;

        [HideInInspector] public AudioSource source;
    }

    public Sound[] sounds;
    // Start is called before the first frame update
    void Awake()
    {
        foreach(Sound currentSound in sounds)
        {
            currentSound.source = gameObject.AddComponent<AudioSource>();
            currentSound.source.clip = currentSound.audioClip;
            currentSound.source.volume = currentSound.volume;
            currentSound.source.pitch = currentSound.pitch;
        }
    }

    public void PlaySounds(string name)
    {
        Sound soundToPlay = Array.Find(sounds, sound => sound.name == name);
        soundToPlay.source.Play();
    }
}
