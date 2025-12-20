using UnityEngine;

public class MusicManager : MonoBehaviour
{
    private AudioSource source;

    private void Start()
    {
        source = GetComponent<AudioSource>();
    }

    public void SetMusic(AudioClip music)
    {
        source.Stop();
        source.clip = music;
        source.Play();
    }

    public AudioClip GetMusic()
    {
        return source.clip;
    }
}