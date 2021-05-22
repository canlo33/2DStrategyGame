using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Building : MonoBehaviour
{
    private HealthSystem healthSystem;
    private BuildingType buildingType;
    [SerializeField] private Transform demolishButton;
    [SerializeField] private Transform repairButton;
    private void Awake()
    {
        demolishButton = transform.Find("BuildingDemolishButton");
        repairButton = transform.Find("BuildingRepairButton");
        if (demolishButton != null)
            demolishButton.gameObject.SetActive(false);
        repairButton.gameObject.SetActive(false);
    }
    private void Start()
    {
        healthSystem = GetComponent<HealthSystem>();
        buildingType = GetComponent<BuildingTypeHolder>().buildingType;
        healthSystem.Initialize(buildingType.maxHealth, true);
        healthSystem.OnDied += HealthSystem_OnDied;
        healthSystem.OnDamaged += HealthSystem_OnDamaged;
        healthSystem.OnHealed += HealthSystem_OnHealed;
    }

    private void HealthSystem_OnDamaged(object sender, EventArgs e)
    {
        repairButton.gameObject.SetActive(true);
    }

    private void HealthSystem_OnHealed(object sender, EventArgs e)
    {
        if(healthSystem.IsHealthFull())
           repairButton.gameObject.SetActive(false);
    }

    private void HealthSystem_OnDied(object sender, EventArgs eventArgs)
    {
        //This function will be called when the currentHealth drops to zero and will destroy the game object.
        Destroy(gameObject);
    }
    private void OnMouseEnter()
    {
        if (demolishButton != null)
            demolishButton.gameObject.SetActive(true);
    }
    private void OnMouseExit()
    {
        if (demolishButton != null)
            demolishButton.gameObject.SetActive(false);
    }
}
