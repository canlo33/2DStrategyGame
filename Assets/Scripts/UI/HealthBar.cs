using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private HealthSystem healthSystem;
    [SerializeField] private Transform barTransform;
    [SerializeField] private Transform separatorContainer;
    [SerializeField] private Transform separatorTemplate;

    private void Start()
    {
        healthSystem.OnDamaged += HealthSystem_OnDamaged;
        healthSystem.OnHealed += HealthSystem_OnHealed;
        healthSystem.OnHealthChanged += HealthSystem_OnHealthChanged;
        HealthBarSeparators();
        UpdateUI();
    }

    private void HealthSystem_OnHealthChanged(object sender, System.EventArgs e)
    {
        HealthBarSeparators();
    }

    private void HealthSystem_OnHealed(object sender, System.EventArgs e)
    {
        UpdateUI();
    }

    private void HealthSystem_OnDamaged(object sender, System.EventArgs e)
    {
        UpdateUI();
    }

    private void UpdateUI()
    {
        barTransform.localScale = new Vector3(healthSystem.GetCurrentHealthNormalized(), 1f, 1f);
        if (healthSystem.IsHealthFull())
            gameObject.SetActive(false);
        else
            gameObject.SetActive(true);
    }
    private void HealthBarSeparators()
    {
        separatorTemplate.gameObject.SetActive(false);
        //Destroy the old separators except the template.
        foreach (Transform separatorTransform in separatorContainer)
        {
            if (separatorTransform == separatorTemplate) continue;
            Destroy(separatorTransform.gameObject);
        }
        //Positioning health bar separtors accordingly.
        float barSize = 3f;
        float singleBarHealth = barSize / healthSystem.GetMaxHealth();
        int healthSeparatorCount = Mathf.FloorToInt(healthSystem.GetMaxHealth() / 10);
        for (int i = 0; i < healthSeparatorCount; i++)
        {
            Transform separatorTransform = Instantiate(separatorTemplate, separatorContainer);
            separatorTransform.gameObject.SetActive(true);
            separatorTransform.localPosition = new Vector3(singleBarHealth * i * 10, 0f, 0f);
        }
    }

}
