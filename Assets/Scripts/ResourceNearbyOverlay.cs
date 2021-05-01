using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ResourceNearbyOverlay : MonoBehaviour
{
    private ResourceGeneratorData resourceGeneratorData;
    [SerializeField] private TextMeshPro text;
    [SerializeField] private SpriteRenderer icon;

    private void Awake()
    {
        Hide();
    }
    private void Update()
    {
        int nearbyResourceAmount = ResourceGenerator.GetNearbyResourceAmount(resourceGeneratorData, transform.position);
        float percentage = Mathf.RoundToInt((float)nearbyResourceAmount / resourceGeneratorData.maxResourceAmount * 100f);
        text.SetText(percentage + "%");
    }
    public void Display(ResourceGeneratorData resourceGeneratorData)
    {
        this.resourceGeneratorData = resourceGeneratorData;
        gameObject.SetActive(true);
        icon.sprite = resourceGeneratorData.resourceType.sprite;
    }
    public void Hide()
    {
        gameObject.SetActive(false);
    }


}
