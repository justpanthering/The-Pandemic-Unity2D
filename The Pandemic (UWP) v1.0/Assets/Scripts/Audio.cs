using UnityEngine.Audio;
using UnityEngine;

[System.Serializable]
public class Audio
{
    public AudioClip clip;
    public string name;

    [Range(0f, 1f)]
    public float volume;

    [HideInInspector]
    public AudioSource source;

    public bool loop;

}
