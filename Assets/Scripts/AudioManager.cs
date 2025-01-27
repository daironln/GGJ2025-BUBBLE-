using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : PersistentSingleton<AudioManager>
{
    // [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip[] envAudioClips;
    [SerializeField] private AudioClip[] playerAudioClips;

    private void Start()
    {
        // audioSource = GetComponent<AudioSource>();
    }

    public void PlayEnvAudio(AudioSource audioSource, int index)
    {
        audioSource.clip = envAudioClips[index];
        audioSource.Play();
    }

    public void PlayPlayerAudio(AudioSource audioSource,int index)
    {
        audioSource.clip = playerAudioClips[index];
        audioSource.Play();
    }
}
