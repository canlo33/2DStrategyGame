using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingIndicator : MonoBehaviour
{
    [SerializeField] private GameObject spriteGameObject;

    private void Awake()
    {
        Hide();
    }
    private void Start()
    {
        BuildingManager.Instance.OnActiveBuildingTypeChanged += BuildingManager_OnActiveBuildingTypeChanged;
    }
    private void BuildingManager_OnActiveBuildingTypeChanged(object sender, BuildingManager.OnActiveBuildingTypeChangedEventArgs eventArgs)
    {
        if (eventArgs.activeBuildingType == null)
            Hide();
        else
            Display(eventArgs.activeBuildingType.sprite);
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
