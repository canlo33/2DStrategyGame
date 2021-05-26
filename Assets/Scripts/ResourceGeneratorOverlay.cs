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
        if (resourceGenerator.GetCoolDown() != 0)
            text.SetText(resourceGenerator.GetResourceGeneratingRate().ToString("F1"));
        else
            text.SetText("0");
    }
    private void Update()
    {
        if(resourceGenerator.GetCoolDown() != 0)
            bar.localScale = new Vector3(1 - resourceGenerator.GetTimerNormalized(), 1, 1);
        else
            bar.localScale = new Vector3(1, 1, 1);
    }
}
