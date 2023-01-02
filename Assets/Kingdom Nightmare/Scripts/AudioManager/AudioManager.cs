using UnityEngine.Audio;
using UnityEngine;
using System;

public class AudioManager : MonoBehaviour
{

    public static AudioManager instance;
    public Audio[] audios;
    private void Awake()
    {
        MakeSingleton();
        foreach (Audio audio in audios)
        {
            audio.source = gameObject.AddComponent<AudioSource>();
            audio.source.clip = audio.clip;
            audio.source.volume = audio.Volume;
            audio.source.loop = audio.loop;
            audio.source.pitch = audio.pitch;
            audio.source.playOnAwake = false;
        }
    }
    //create an instance of audio manager
    private void MakeSingleton()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
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
    public void Stop(bool soundFx)
    {
        foreach (Audio a in audios)
        {
            if(soundFx == a.soundFx)
            a.source.Stop();
        }
    }
    public void SetVolume(string name, float volume, bool soundFx)
    {
        Audio aud = Array.Find(audios, audio => audio.clipName == name);
        if (aud == null)
        {
            Debug.LogWarning("there is no such a audio clip on the list");
        }
        else
        {
            if(aud.soundFx == soundFx)
            {
                aud.source.volume = volume;
            }
       
        }

    }
    public void SetAllVolume(float volume, bool soundFx)
    {
        foreach (Audio aud in audios)
        {
            if (aud.soundFx == soundFx)
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
    public void   SetAllVolumesToDefault(bool Sfx)
    {
        foreach (Audio a in audios)
        {
            if(a.soundFx == Sfx)
            a.source.volume = a.Volume;
        }
    }
}
