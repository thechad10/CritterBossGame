using UnityEngine.Audio;
using UnityEngine;

[System.Serializable]
public class Sound
{
    public string name;

    public AudioSource source;

    public AudioMixerGroup mixer;

    public AudioClip clip;

    public AudioResource random;

    [Range(0f, 1f)]
    public float volume;

    public bool loop;

    public bool PlayAwake;



}
