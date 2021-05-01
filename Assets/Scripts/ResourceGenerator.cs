using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceGenerator : MonoBehaviour
{
    private ResourceGeneratorData resourceGeneratorData;
    private float coolDown;
    private float timer;
    [SerializeField] private BuildingType buildingType;
    public BuildingType BuildingType { get; private set; }
    private void Awake()
    {
        resourceGeneratorData = buildingType.resourceGeneratorData;
        BuildingType = buildingType;
    }
    private void Start()
    {
        int nearbyResourceAmount = GetNearbyResourceAmount(resourceGeneratorData, transform.position);
        if (nearbyResourceAmount == 0)
            enabled = false;
        // Else if we have resource, we lower our cooldown for each nearbyResource we have.
        else
        {
            coolDown = (resourceGeneratorData.coolDown / 2f) + resourceGeneratorData.coolDown *
                (1 - (float)nearbyResourceAmount / resourceGeneratorData.maxResourceAmount);
        }
    }
    void Update()
    {
        GenerateResource();
    }
    private void GenerateResource()
    {
        timer -= Time.deltaTime;
        if (timer <= 0f)
        {
            timer = coolDown;
            ResourceManager.Instance.AddResource(resourceGeneratorData.resourceType, 1);
        }
    }
    public static int GetNearbyResourceAmount(ResourceGeneratorData resourceGeneratorData, Vector3 position)
    {
        Collider2D[] collider2DArray = Physics2D.OverlapCircleAll(position, resourceGeneratorData.resourceDetectionRadius);
        int nearbyResourceAmount = 0;
        //We get the amount of related resources around the position of the ResourceGenerator a
        foreach (var collider2D in collider2DArray)
        {
            ResourceNode resourceNode = collider2D.GetComponent<ResourceNode>();
            //Check if the resource is not empty and it is the resource we are looking for, then increase amount.
            if (resourceNode != null && resourceNode.resourceType == resourceGeneratorData.resourceType)
                nearbyResourceAmount++;
        }
        //We clamp the nearbyResourceAmount between 0 and maxResourceAmount.
        nearbyResourceAmount = Mathf.Clamp(nearbyResourceAmount, 0, resourceGeneratorData.maxResourceAmount);
        //If there are no resources around we dont need to generate.
        return nearbyResourceAmount;
    }
    public ResourceGeneratorData GetResourceGeneratorData()
    {
        return resourceGeneratorData;
    }
    public float GetTimerNormalized()
    {
        return timer / coolDown;
    }
    public float GetResourceGeneratingRate()
    {
        return 1 / coolDown;
    }
}
[System.Serializable]
public class ResourceGeneratorData
{
    public float coolDown;
    public ResourceType resourceType;
    public float resourceDetectionRadius;
    public int maxResourceAmount;
}
