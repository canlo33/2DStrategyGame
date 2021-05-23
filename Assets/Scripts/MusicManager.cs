using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    private AudioSource audioSource;
    private float musicVolume = .5f;
    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.volume = musicVolume;
    }
    public void IncreaseMusicVolume()
    {
        musicVolume += .1f;
        musicVolume = Mathf.Clamp01(musicVolume);
        audioSource.volume = musicVolume;
    }
    public void DecreaseMusicVolume()
    {
        musicVolume -= .1f;
        musicVolume = Mathf.Clamp01(musicVolume);
        audioSource.volume = musicVolume;
    }
    public float GetMusicVolume()
    {
        return musicVolume;
    }
}
