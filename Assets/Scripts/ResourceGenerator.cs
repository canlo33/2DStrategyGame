using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceGenerator : MonoBehaviour
{
    [SerializeField]private float coolDown;
    private float timer;
    [SerializeField] private ResourceType resourceType;

    void Update()
    {
        timer -= Time.deltaTime;
        if(timer <= 0f)
        {
            timer = coolDown;
            ResourceManager.Instance.AddResource(resourceType, 1);
        }
    }
}
[System.Serializable]
public class ResourceGeneratorData
{
    public float coolDown;
    public ResourceType resourceType;
}
