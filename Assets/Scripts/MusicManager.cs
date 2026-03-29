using System;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    private AudioSource source;
    private AudioClip upcomingLoop;

    private void Start()
    {
        source = GetComponent<AudioSource>();
    }

    public void SetMusic(AudioClip music)
    {
        source.Stop();
        source.loop = true;
        source.clip = music;
        source.Play();
    }

    public void DualLoop(AudioClip intro, AudioClip loop)
    {
        source.Stop();
        source.loop = false;
        source.clip = intro;
        upcomingLoop = loop;
        source.Play();
    }

    private void Update()
    {
        if (upcomingLoop is not null && source is not null && !source.loop && source.time >= source.clip.length)
        {
            SetMusic(upcomingLoop);
            upcomingLoop = null;
        }
    }

    public AudioClip GetMusic()
    {
        return source.clip;
    }
}