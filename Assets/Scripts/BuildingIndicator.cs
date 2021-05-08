using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingIndicator : MonoBehaviour
{
    [SerializeField] private GameObject spriteGameObject;
    private ResourceNearbyOverlay resourceNearbyOverlay;

    private void Awake()
    {
        Hide();
        resourceNearbyOverlay = transform.Find("ResourceNearbyOverlay").GetComponent<ResourceNearbyOverlay>();
    }
    private void Start()
    {
        BuildingManager.Instance.OnActiveBuildingTypeChanged += BuildingManager_OnActiveBuildingTypeChanged;
    }
    private void BuildingManager_OnActiveBuildingTypeChanged(object sender, BuildingManager.OnActiveBuildingTypeChangedEventArgs eventArgs)
    {
        if (eventArgs.activeBuildingType == null)
        {
            Hide();
            resourceNearbyOverlay.Hide();
        }            
        else
        {
            Display(eventArgs.activeBuildingType.sprite);
            if (eventArgs.activeBuildingType.resourceGeneratorData.hasGeneratorData)
                resourceNearbyOverlay.Display(eventArgs.activeBuildingType.resourceGeneratorData);
            else
                resourceNearbyOverlay.Hide();
        }            
    }
    private void Update()
    {
        transform.position = UtilsClass.GetMousePositionOnWorld();
    }
    private void Display(Sprite sprite)
    {
        spriteGameObject.SetActive(true);
        spriteGameObject.GetComponent<SpriteRenderer>().sprite = sprite;
    }
    private void Hide()
    {
        spriteGameObject.SetActive(false);
    }
}
