using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;
    private AudioSource audioSource;
    [SerializeField] private List<AudioClip> audioClips;
    private float soundVolume = .5f;
    private void Awake()
    {
        Instance = this;
        audioSource = GetComponent<AudioSource>();
    }
    public void PlayAudio(int audioNumber)
    {
        audioSource.PlayOneShot(audioClips[audioNumber], soundVolume);
    }
    public void IncreaseSoundVolume()
    {
        soundVolume += .1f;
        soundVolume = Mathf.Clamp01(soundVolume);
    }
    public void DecreaseSoundVolume()
    {
        soundVolume -= .1f;
        soundVolume = Mathf.Clamp01(soundVolume);
    }
    public float GetSoundVolume()
    {
        return soundVolume;
    }
}
