using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Building : MonoBehaviour
{
    private HealthSystem healthSystem;
    [SerializeField]private BuildingType buildingType;
    private void Start()
    {
        healthSystem = GetComponent<HealthSystem>();
        healthSystem.Initialize(buildingType.maxHealth, true);
        healthSystem.OnDied += HealthSystem_OnDied;
    }
    private void HealthSystem_OnDied(object sender, EventArgs eventArgs)
    {
        //This function will be called when the currentHealth drops to zero and will destroy the game object.
        Destroy(gameObject);
    }
}
