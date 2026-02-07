using UnityEngine;

public class MusicSwitcher : MonoBehaviour
{
    public AudioClip music;
    private MusicManager musicManager;

    private void Start()
    {
        musicManager = FindAnyObjectByType<MusicManager>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (musicManager.GetMusic() != music) musicManager.SetMusic(music);
    }
}