using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager : MonoBehaviour
{
    public static ResourceManager Instance { get; private set; }
    [SerializeField] private List<ResourceType> resourceType;
    private Dictionary<ResourceType, int> resourceAmounts;

    // Start is called before the first frame update
    void Awake()
    {
        resourceAmounts = new Dictionary<ResourceType, int>();
        // Set all the resourcetypes and their amount to 0.
        foreach (var resourceType in resourceType)
            resourceAmounts[resourceType] = 0;
    }

    public void AddResource(ResourceType resourceType, int amount)
    {
        resourceAmounts[resourceType] += amount;
    }

}
