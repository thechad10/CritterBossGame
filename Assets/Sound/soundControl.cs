using UnityEngine.Audio;
using System;
using UnityEngine;

public class SoundControl : MonoBehaviour
{
    public bool stopPlay;
    public Sound[] sounds;
    public static SoundControl instance;
    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);

        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.loop = s.loop;
            s.source.playOnAwake = s.PlayAwake;
            s.source.outputAudioMixerGroup = s.mixer;
            s.source.resource = s.random;

        }

    }
    public void Update()
    {
        foreach (Sound s in sounds)
        {
            s.source.volume = s.volume;
        }
    }
    public void Play(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
            return;
        s.source.Play();
    }
    public void StopPlay(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
            return;
        s.source.Stop();
    }

}
