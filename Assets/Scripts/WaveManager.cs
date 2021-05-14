using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    private enum State
    {
        WaitingForNextWave,
        SpawningWave,
    }
    public event EventHandler OnWaveNumberChanged;
    private State currentState;
    private int currentWaveNumber;
    private float nextWaveTimer;
    private float nextWaveCooldown;
    private float nextEnemySpawnTimer;
    private int remainingEnemyToSpawn;
    [SerializeField] private List<Transform> spawnPointList;
    [SerializeField] private Transform wavePortal;
    private Vector3 spawnPosition;
    private void Start()
    {

        currentState = State.WaitingForNextWave;
        spawnPosition = spawnPointList[UnityEngine.Random.Range(0, spawnPointList.Count)].position;
        wavePortal.position = spawnPosition;
        nextWaveTimer = 3f;
    }
    private void Update()
    {
        //Check which state we are currently at. If we are waiting for the next wave then just run the timer for nextWaveTimer.
        //If we are spawning enemies, then run nextEnemySpawnTimer and start spawning enemies one after another.
        switch (currentState)
        {
            case State.WaitingForNextWave:
                nextWaveTimer -= Time.deltaTime;
                if (nextWaveTimer < 0)
                    SpawnWave();
                break;
            case State.SpawningWave:
                if (remainingEnemyToSpawn > 0)
                {
                    nextEnemySpawnTimer -= Time.deltaTime;
                    if (nextEnemySpawnTimer < 0)
                    {
                        nextEnemySpawnTimer = UnityEngine.Random.Range(0f, 0.2f);
                        EnemyController.RespownEnemy(spawnPosition + UtilsClass.GetRandomDirection() * UnityEngine.Random.Range(0f, 10f));
                        remainingEnemyToSpawn--;
                    }
                    if (remainingEnemyToSpawn <= 0)
                    {
                        currentState = State.WaitingForNextWave;
                        spawnPosition = spawnPointList[UnityEngine.Random.Range(0, spawnPointList.Count)].position;
                        wavePortal.position = spawnPosition;
                        nextWaveTimer = 10f;
                    }                        
                }
                break;
        }
    }
    private void SpawnWave()
    {        
        remainingEnemyToSpawn = 5 + currentWaveNumber * 3;
        currentState = State.SpawningWave;
        currentWaveNumber++;
        OnWaveNumberChanged?.Invoke(this, EventArgs.Empty);
    }
    public int GetCurrentWaveNumber()
    {
        return currentWaveNumber;
    }
    public float GetNextWaveTimer()
    {
        return nextWaveTimer;
    }
    public Vector3 GetSpawnPosition()
    {
        return spawnPosition;
    }
}
