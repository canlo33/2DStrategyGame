using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ResourceGeneratorOverlay : MonoBehaviour
{
    [SerializeField] private ResourceGenerator resourceGenerator;
    private ResourceGeneratorData resourceGeneratorData;
    [SerializeField] private Transform bar;
    [SerializeField] private SpriteRenderer icon;
    [SerializeField] private TextMeshPro text;
    private void Start()
    {
        resourceGeneratorData = resourceGenerator.GetResourceGeneratorData();
        icon.GetComponent<SpriteRenderer>().sprite = resourceGeneratorData.resourceType.sprite;
        text.SetText(resourceGenerator.GetResourceGeneratingRate().ToString("F1"));
    }
    private void Update()
    {
        bar.localScale = new Vector3(1- resourceGenerator.GetTimerNormalized(), 1, 1);
    }
}
