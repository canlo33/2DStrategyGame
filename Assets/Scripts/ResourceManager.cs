using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ResourceManager : MonoBehaviour
{
    public static ResourceManager Instance { get; private set; }
    private Dictionary<ResourceType, int> resourceAmounts;
    private ResourceTypeList resourceTypeList;
    public event EventHandler OnResourceAmountChanged;
    [SerializeField] private List<ResourceAmount> startingResourceAmountList;

    void Awake()
    {
        Instance = this;
        resourceAmounts = new Dictionary<ResourceType, int>();
        resourceTypeList = Resources.Load<ResourceTypeList>(typeof(ResourceTypeList).Name);
        // Set all the resourcetypes and their amount to 0.
        foreach (var resourceType in resourceTypeList.list)
            resourceAmounts[resourceType] = 0;
        foreach (var resourceAmount in startingResourceAmountList)
            AddResource(resourceAmount.resourceType, resourceAmount.amount);
    }

    // This function will add resources to the selected resourceType and will be accessed from other scripts.
    // Event is invoked everytime resouce amount is changed(first checks if the listener is not null) and send message
    // to the UI system to update the UI. So we dont have to update the UI on every single frame. This is to improve performance.
    public void AddResource(ResourceType resourceType, int amount)
    {
        resourceAmounts[resourceType] += amount;
        OnResourceAmountChanged?.Invoke(this, EventArgs.Empty);
    }

    // This function will return the amount of the selected resourceType and will be accessed from other scripts.
    public int GetResourceAmount(ResourceType resourceType)
    {
        return resourceAmounts[resourceType];
    }
    // This function will check if the player can afford the cost of certain type of building and will return the result in bool.
    public bool CanAffordBuilding(BuildingCost[] buildingCostArray)
    {
        foreach (var buildingCost in buildingCostArray)
        {
            //Cant afford
            if (GetResourceAmount(buildingCost.resourceType) < buildingCost.costAmount)
                return false;
        }
        //We can afford 
        return true;
    }
    // This function will be called to pay the construction cost for each building.
    public void PayBuildingCost(BuildingCost[] buildingCostArray)
    {
        foreach (var buildingCost in buildingCostArray)
            resourceAmounts[buildingCost.resourceType] -= buildingCost.costAmount;
    }


}
