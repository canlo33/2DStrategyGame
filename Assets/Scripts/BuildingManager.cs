using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System;

public class BuildingManager : MonoBehaviour
{
    public static BuildingManager Instance { get; private set;}
    [SerializeField] private Building mainBuilding;
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
    }
    void Start()
    {
        mainCamera = Camera.main;
        mainBuilding.GetComponent<HealthSystem>().OnDied += MainBuilding_OnDied;
    }

    private void MainBuilding_OnDied(object sender, EventArgs e)
    {
        SoundManager.Instance.PlayAudio(6);
        GameOverUI.Instance.Display();
    }

    private void Update()
    {
        PlaceBuilding();
    }
    private void PlaceBuilding()
    {
        //This funtion will place a building if the mouse button is clicked and there is no other gameobject on the mouse position.
        // We also check if the costruction rules are fine before building.
        // If we dont meet the construction rules, it will call ToolTipUI to display why we cant place here.
        if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
        {
            if (activeBuildingType != null)
            {
                if (CanBuildInHere(activeBuildingType, UtilsClass.GetMousePositionOnWorld(), out string errorMessage))
                    if (ResourceManager.Instance.CanAffordBuilding(activeBuildingType.constructionCostArray))
                    {
                        ResourceManager.Instance.PayBuildingCost(activeBuildingType.constructionCostArray);
                        BuildingConstruction.Create(UtilsClass.GetMousePositionOnWorld(), activeBuildingType);
                        SoundManager.Instance.PlayAudio(2);
                    }
                    else
                        TooltipUI.Instance.Display("Insufficent resource " + activeBuildingType.GetBuildingInformationString(),
                            new TooltipUI.ToolTipTimer { timer = 2f });
                else
                  TooltipUI.Instance.Display(errorMessage, new TooltipUI.ToolTipTimer { timer = 2f });
            }
        }
    }
    public void ChangeActiveBuildingType(BuildingType newBuildingType)
    {
        // This function will use EventSystems that was created on BuildingManager script and changes the current ActiveBuildingType to the newone.
        activeBuildingType = newBuildingType;
        OnActiveBuildingTypeChanged?.Invoke(this, new OnActiveBuildingTypeChangedEventArgs { activeBuildingType = activeBuildingType });
    }
    public BuildingType ReturnActiveBuildingType()
    {
        return activeBuildingType;
    }
    private bool CanBuildInHere(BuildingType buildingType, Vector3 position, out string errorMessage)
    {
        // First We check what objects are around the place we want to build.
        //If the area is not clear we return false.
        BoxCollider2D boxCollider2D = buildingType.prefab.GetComponent<BoxCollider2D>();
        Collider2D[] collider2DArray = Physics2D.OverlapBoxAll(position + (Vector3)boxCollider2D.offset, boxCollider2D.size, 0);
        bool isAreaClear = collider2DArray.Length == 0;
        if (!isAreaClear)
        {
            errorMessage = "Area is not clear!";
            return false;
        }
        //Secondly, If the area is clear. We need to check if there is another building with the same type around.
        //As we dont want player to spam the same building type all over the same resource node.
        collider2DArray = Physics2D.OverlapCircleAll(position, buildingType.minConstructionDistance);
        foreach (var collider in collider2DArray)
        {
            BuildingTypeHolder buildingTypeHolder = collider.GetComponent<BuildingTypeHolder>();
            if (buildingTypeHolder != null && buildingTypeHolder.buildingType == buildingType)
            {
                errorMessage = "Too close to another same type building!";
                return false;
            }                
        }
        errorMessage = "";
        return true;
    }
    public Building GetMainBuilding()
    {
        return mainBuilding;
    }
}
