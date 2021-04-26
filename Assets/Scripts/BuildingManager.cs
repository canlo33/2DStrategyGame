using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System;

public class BuildingManager : MonoBehaviour
{
    public static BuildingManager Instance { get; private set;}
    Camera mainCamera;
    private BuildingTypeList buildingTypeList;
    private BuildingType activeBuildingType;
    public event EventHandler<OnActiveBuildingTypeChangedEventArgs> OnActiveBuildingTypeChanged;
    public class OnActiveBuildingTypeChangedEventArgs : EventArgs
    {
        public BuildingType activeBuildingType;
    }
    private void Awake()
    {
        Instance = this;
        buildingTypeList = Resources.Load<BuildingTypeList>(typeof(BuildingTypeList).Name);
        activeBuildingType = buildingTypeList.list[0];
    }
    void Start()
    {
        mainCamera = Camera.main;
    }
    private void Update()
    {
        PlaceBuilding();
    }
    //This funtion will place a building if the mouse button is clicked and there is no other gameobject on the mouse position.
    private void PlaceBuilding()
    {        
        if(Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
        {
            if(activeBuildingType != null)
                Instantiate(activeBuildingType.prefab, UtilsClass.GetMousePositionOnWorld(), Quaternion.identity);
        }
    }
    // This function will use EventSystems that was created on BuildingManager script and changes the current ActiveBuildingType to the newone.
    public void ChangeActiveBuildingType(BuildingType newBuildingType)
    {
        activeBuildingType = newBuildingType;
        OnActiveBuildingTypeChanged?.Invoke(this, new OnActiveBuildingTypeChangedEventArgs { activeBuildingType = activeBuildingType });
    }
    public BuildingType ReturnActiveBuildingType()
    {
        return activeBuildingType;
    }
}
