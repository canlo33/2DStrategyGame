using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceGenerator : MonoBehaviour
{
    [SerializeField]private float coolDown;
    private float timer;
    [SerializeField] private ResourceType resourceType;

    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        if(timer <= 0f)
        {
            timer = coolDown;
            ResourceManager.Instance.AddResource(resourceType, 1);
            Debug.Log(resourceType.name + " :")
        }
    }
}
