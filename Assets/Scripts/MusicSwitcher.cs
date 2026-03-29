using JetBrains.Annotations;
using UnityEngine;

public class MusicSwitcher : MonoBehaviour
{
    [CanBeNull] public AudioClip intro;
    public AudioClip music;
    private MusicManager musicManager;

    private void Start()
    {
        musicManager = FindAnyObjectByType<MusicManager>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (intro == null)
            {
                if (musicManager.GetMusic() != music) musicManager.SetMusic(music);
            }
            else
            {
                if (musicManager.GetMusic() != music && musicManager.GetMusic() != intro) musicManager.DualLoop(intro, music);
            }
        }
    }
}