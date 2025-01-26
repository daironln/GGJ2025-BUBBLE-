using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.Audio;

public class PlayerAudioController : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioSource audioSourceSecundary;

    // Start is called before the first frame update
    void Start()
    {
        // audioSource = GetComponent<AudioSource>();
    }

    public void PlayDashSound(){
        AudioManager.Instance.PlayPlayerAudio(audioSource, 2);
    }

    public void PlayAttackSound(){
        AudioManager.Instance.PlayPlayerAudio(audioSourceSecundary, 1);
    }

    public void PlayPlayerAudio(int index)
    {
        AudioManager.Instance.PlayPlayerAudio(audioSource, index);
    }
}
