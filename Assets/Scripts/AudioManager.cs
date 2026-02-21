using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    public AudioSource buttonAudioSource;
    public AudioSource cardAudioSource;

    private void Awake()
    {
        if (Instance != null)
        {
            Instance = null;
        }
        Instance = this;

    }
    public void PlayButtonFx()
    {
        buttonAudioSource.Play();
    }
    public void PlayCardFlipFx()
    {
        cardAudioSource.Play();
    }
}
