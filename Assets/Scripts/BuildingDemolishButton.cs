using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class BuildingDemolishButton : MonoBehaviour
{
    [SerializeField] private Button button;
    [SerializeField] private Building building;

    private void Awake()
    {
        button.onClick.AddListener(() => 
        {
            BuildingType buildingType = GetComponentInParent<BuildingTypeHolder>().buildingType;
            foreach (ResourceAmount resourceAmount in buildingType.constructionCostArray)
                ResourceManager.Instance.AddResource(resourceAmount.resourceType, Mathf.FloorToInt(resourceAmount.amount * 0.5f));
            Destroy(building.gameObject);
        });
    }
}
