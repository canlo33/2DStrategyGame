using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ResourcesUI : MonoBehaviour
{
    [SerializeField]private Transform resourceTemplate;
    private ResourceTypeList resourceTypeList;
    private Dictionary<ResourceType, Transform> resourceTypeTransformDictionary;
    private void Awake()
    {
        resourceTypeList = Resources.Load<ResourceTypeList>(typeof(ResourceTypeList).Name);
        resourceTypeTransformDictionary = new Dictionary<ResourceType, Transform>();
        resourceTemplate.gameObject.SetActive(false);

        //Here we are dynamicly instatiating our Resource Templete foreach resource type we have and set its image to the corresponding resource.
        //We also position it dynamicly by position each resource to be 160f away from each other.
        int index = 0;
        foreach (var resourceType in resourceTypeList.list)
        {
            Transform resourceTransform = Instantiate(resourceTemplate, transform);
            resourceTransform.gameObject.SetActive(true);
            float positionOffset = -160f;
            resourceTransform.GetComponent<RectTransform>().anchoredPosition = new Vector2(positionOffset * index, 0);
            resourceTransform.Find("Image").GetComponent<Image>().sprite = resourceType.sprite;
            resourceTypeTransformDictionary[resourceType] = resourceTransform;
            index++;
        }
    }
    private void Start()
    {
        ResourceManager.Instance.OnResourceAmountChanged += ResourceManager_OnResourceAmountChanged;
        UpdateResourceAmount();
    }

    private void ResourceManager_OnResourceAmountChanged(object sender, System.EventArgs e)
    {
        UpdateResourceAmount();
    }
    private void UpdateResourceAmount()
    {
        foreach (var resourceType in resourceTypeList.list)
        {
            int resourceAmount = ResourceManager.Instance.GetResourceAmount(resourceType);
            Transform resourceTransform = resourceTypeTransformDictionary[resourceType];
            resourceTransform.Find("Text").GetComponent<TextMeshProUGUI>().SetText(resourceAmount.ToString());
        }
    }
}
