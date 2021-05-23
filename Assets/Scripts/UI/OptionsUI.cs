using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class OptionsUI : MonoBehaviour
{
    [SerializeField] private MusicManager musicManager;
    [SerializeField] private Button soundIncreaseButton;
    [SerializeField] private Button soundDecreaseButton;
    [SerializeField] private Button musicIncreaseButton;
    [SerializeField] private Button musicDecreaseButton;
    [SerializeField] private Button mainMenuButton;
    [SerializeField] private TextMeshProUGUI soundVolumeText;
    [SerializeField] private TextMeshProUGUI musicVolumeText;
    private void Awake()
    {
        soundIncreaseButton.onClick.AddListener(() => { 
            SoundManager.Instance.IncreaseSoundVolume();
            UpdateText();
        });
        soundDecreaseButton.onClick.AddListener(() => { 
            SoundManager.Instance.DecreaseSoundVolume();
            UpdateText();
        });
        musicIncreaseButton.onClick.AddListener(() => {
            musicManager.IncreaseMusicVolume();
            UpdateText();
        });
        musicDecreaseButton.onClick.AddListener(() => {
            musicManager.DecreaseMusicVolume();
            UpdateText();
        });
        mainMenuButton.onClick.AddListener(() => {
            Time.timeScale = 1f;
            GameSceneManager.LoadScene(GameSceneManager.Scene.MainMenuScene);
        });
    }
    private void Start()
    {
        UpdateText();
        gameObject.SetActive(false);
    }
    private void UpdateText()
    {
        string soundVolume = Mathf.RoundToInt(SoundManager.Instance.GetSoundVolume() * 10).ToString();
        string musicVolume = Mathf.RoundToInt(musicManager.GetMusicVolume() * 10).ToString();
        soundVolumeText.SetText(soundVolume);
        musicVolumeText.SetText(musicVolume);
    }
    public void ToggleVisible()
    {
        //if active, hide it. If hidden, then pause the game and show the optionsUI.
        gameObject.SetActive(!gameObject.activeSelf);
        if(gameObject.activeSelf)
            Time.timeScale = 0f;
        else
            Time.timeScale = 1f;
    }
}
