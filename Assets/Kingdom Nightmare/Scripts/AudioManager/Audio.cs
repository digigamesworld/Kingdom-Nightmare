using UnityEngine.Audio;
using UnityEngine;



[System.Serializable]
public class Audio 
{
    public string clipName;
    [Range(0,1)]
    public float Volume;
    [Range(0.1f, 4)]
    public float pitch = 1f;
    [HideInInspector]
    public AudioSource source;
    public AudioClip clip;
    public bool loop;
    public AudioMixerGroup AudioMixerGroup;
}
