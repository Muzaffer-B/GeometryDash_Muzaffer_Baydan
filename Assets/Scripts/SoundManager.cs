using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [Header("Audio Sources")]
    [SerializeField] private AudioSource audio;
    [SerializeField] private AudioSource bgaudio;

    [Header("Audio Clips")]

    [SerializeField] private AudioClip clickudio;

    private void Start()
    {
        audio = GetComponent<AudioSource>();
        PlayerController.onMouseClick += PlaySound;
    }

    private void OnDestroy()
    {
        PlayerController.onMouseClick -= PlaySound;

    }

    private void Update()
    {
        if (!bgaudio.isPlaying)
        {
            bgaudio.Play();
        }
    }
    void PlaySound()
    {
        audio.clip = clickudio;
        audio.Play();
    }

    public void DisableSounds()
    {

        audio.volume = 0;
        bgaudio.volume = 0;
    }
    public void EnableSounds()
    {
        
        audio.volume = 1;
        bgaudio.volume = 1;
        bgaudio.Play();

    }

}
