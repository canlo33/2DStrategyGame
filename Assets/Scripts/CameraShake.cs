using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraShake : MonoBehaviour
{
    public static CameraShake Instance { get; private set; }
    [SerializeField] private CinemachineVirtualCamera virtualCamera;
    [SerializeField] private CinemachineBasicMultiChannelPerlin cinemachineMultiChannelPerlin;
    private float timer;
    private float coolDown;
    private float startingIntensity;

    private void Awake()
    {
        Instance = this;
        virtualCamera = GetComponent<CinemachineVirtualCamera>();
        cinemachineMultiChannelPerlin = virtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
    }
    private void Update()
    {
        if (timer < coolDown)
        {
            timer += Time.deltaTime;
            float amplitude = Mathf.Lerp(startingIntensity, 0f, timer / coolDown);
            cinemachineMultiChannelPerlin.m_AmplitudeGain = amplitude;
        }
    }
    public void ShakeCamera(float intensity, float duration)
    {
        coolDown = duration;
        timer = 0f;
        startingIntensity = intensity;
        cinemachineMultiChannelPerlin.m_AmplitudeGain = intensity;
    }
}
