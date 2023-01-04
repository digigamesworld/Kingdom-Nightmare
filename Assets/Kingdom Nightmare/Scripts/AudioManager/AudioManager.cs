using UnityEngine.Audio;
using UnityEngine;
using System;

public class AudioManager : SingletonMB<AudioManager>
{


    public Audio[] audios;
    private void Awake()
    {

        foreach (Audio audio in audios)
        {
            audio.source = gameObject.AddComponent<AudioSource>();
            audio.source.clip = audio.clip;
            audio.source.volume = audio.Volume;
            audio.source.loop = audio.loop;
            audio.source.pitch = audio.pitch;
            audio.source.playOnAwake = false;
            audio.source.outputAudioMixerGroup = audio.AudioMixerGroup;
        }
    }


    //play a sound by its name
    public void Play(string name)
    {
        Audio aud = Array.Find(audios, audio => audio.clipName == name);
        if (aud == null)
        {
            Debug.LogWarning("there is no such a audio clip on the list");
        }
        else
        {
            aud.source.Play();
        }


    }
    //stop an audio by its name
    public void Stop(string name)
    {
        Audio aud = Array.Find(audios, audio => audio.clipName == name);
        if (aud == null)
        {
            Debug.LogWarning("there is no such a audio clip on the list");
        }
        else
        {
            aud.source.Stop();
        }
    }

    //stop all sounds
    public void Stop()
    {
        foreach (Audio a in audios)
        {
            a.source.Stop();
        }
    }
    public void Stop(AudioMixerGroup mixerGroup)
    {
        foreach (Audio a in audios)
        {
            if(mixerGroup == a.AudioMixerGroup)
            a.source.Stop();
        }
    }
    public void SetVolume(string name, float volume, AudioMixerGroup mixerGroup)
    {
        Audio aud = Array.Find(audios, audio => audio.clipName == name);
        if (aud == null)
        {
            Debug.LogWarning("there is no such a audio clip on the list");
        }
        else
        {
            if(aud.AudioMixerGroup == mixerGroup)
            {
                aud.source.volume = volume;
            }
       
        }

    }
    public void SetAllVolume(float volume, AudioMixerGroup mixerGroup)
    {
        foreach (Audio aud in audios)
        {
            if (aud.AudioMixerGroup == mixerGroup)
            {
                aud.source.volume = volume;
            }

        }

    }
    //this function can be used to mute all sounds volume
    public void SetAllVolumeToZero()
    {
        foreach (Audio a in audios)
        {
        
                a.source.volume = 0;

        }
    }
    //this function set back all sounds volume to one
    public void   SetAllVolumesToDefault(AudioMixerGroup mixerGroup)
    {
        foreach (Audio a in audios)
        {
            if(a.AudioMixerGroup == mixerGroup)
            a.source.volume = a.Volume;
        }
    }
}
