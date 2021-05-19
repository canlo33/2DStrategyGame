using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Building : MonoBehaviour
{
    private HealthSystem healthSystem;
    private BuildingType buildingType;
    [SerializeField] private Transform demolishButton;
    private void Awake()
    {
        demolishButton = transform.Find("BuildingDemolishButton");
        if(demolishButton != null)
            demolishButton.gameObject.SetActive(false);
    }
    private void Start()
    {
        healthSystem = GetComponent<HealthSystem>();
        buildingType = GetComponent<BuildingTypeHolder>().buildingType;
        healthSystem.Initialize(buildingType.maxHealth, true);
        healthSystem.OnDied += HealthSystem_OnDied;
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
