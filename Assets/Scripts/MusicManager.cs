using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public readonly Dictionary<MusicId, AudioClip> music = new();
    public MusicId currentMusic = MusicId.Silence;

    private void Start()
    {
    }

    private void Update()
    {
    }
}