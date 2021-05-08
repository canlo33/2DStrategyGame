using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private HealthSystem healthSystem;
    [SerializeField] private Transform barTransform;

    private void Start()
    {
        healthSystem.OnDamaged += HealthSystem_OnDamaged;
        UpdateUI();
    }

    private void HealthSystem_OnDamaged(object sender, System.EventArgs e)
    {
        UpdateUI();
    }

    private void UpdateUI()
    {
        //barTransform.localScale = new Vector3(healthSystem.GetCurrentHealthNormalized(), 1f, 1f);
        if (healthSystem.IsHealthFull())
            gameObject.SetActive(false);
        else
            gameObject.SetActive(true);
    }

}
