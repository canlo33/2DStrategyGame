using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EnemyWaveUI : MonoBehaviour
{
    [SerializeField] private WaveManager waveManager;
    [SerializeField] private TextMeshProUGUI waveNumberText;
    [SerializeField] private TextMeshProUGUI waveMessageText;
    [SerializeField] private RectTransform wavePositionIndicator;
    private Camera mainCamera;

    private void Start()
    {
        mainCamera = Camera.main;
        waveManager.OnWaveNumberChanged += WaveManager_OnWaveNumberChanged;
        SetNumberText("Wave " + waveManager.GetCurrentWaveNumber());
    }

    private void WaveManager_OnWaveNumberChanged(object sender, System.EventArgs e)
    {
        SetNumberText("Wave " + waveManager.GetCurrentWaveNumber());
    }
    private void Update()
    {
        UpdateWaveTimer();
        UpdateWavePositionIndicator();
    }
    private void UpdateWavePositionIndicator()
    {
        //Point the WavePositionIndicator towards to the wave location and offset it towards that direction
        Vector3 nextSpawnDirection = (waveManager.GetSpawnPosition() - mainCamera.transform.position).normalized;
        wavePositionIndicator.anchoredPosition = nextSpawnDirection * 300f;
        wavePositionIndicator.eulerAngles = new Vector3(0f, 0f, UtilsClass.GetAngleFromVector(nextSpawnDirection));
    }
    private void UpdateWaveTimer()
    {
        float nextWaveTimer = waveManager.GetNextWaveTimer();
        if (nextWaveTimer <= 0f)
            SetMessageText("");
        else
            SetMessageText("Next Wave in " + nextWaveTimer.ToString("F0") + "s");

    }
    private void SetMessageText(string message)
    {
        waveMessageText.text = message;
    }
    private void SetNumberText(string message)
    {
        waveNumberText.text = message;
    }
}
