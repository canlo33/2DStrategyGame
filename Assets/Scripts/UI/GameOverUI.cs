using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameOverUI : MonoBehaviour
{
    public static GameOverUI Instance { get; private set; }
    [SerializeField] private Button playAgainButton;
    [SerializeField] private Button mainMenuButton;
    [SerializeField] private TextMeshProUGUI wavesSurvivedText;
    private void Awake()
    {
        Instance = this;
        playAgainButton.onClick.AddListener(() =>
       {
           GameSceneManager.LoadScene(GameSceneManager.Scene.GameScene);
       });
        mainMenuButton.onClick.AddListener(() =>
        {
            GameSceneManager.LoadScene(GameSceneManager.Scene.MainMenuScene);
        });
        Hide();
    }
    public void Display()
    {
        gameObject.SetActive(true);
        wavesSurvivedText.SetText("Survived " + WaveManager.Instance.GetCurrentWaveNumber() +" Waves!");
    }
    private void Hide()
    {
        gameObject.SetActive(false);
    }
}
