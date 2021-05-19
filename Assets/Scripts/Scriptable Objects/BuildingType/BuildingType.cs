using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="BuildingType",menuName = "ScriptableObjects / BuildingType")]
public class BuildingType : ScriptableObject
{
    public new string name = "Enter Building Name";
    public Transform prefab;
    public ResourceGeneratorData resourceGeneratorData;
    public Sprite sprite;
    public float minConstructionDistance = 7f;
    public ResourceAmount[] constructionCostArray;
    public int maxHealth;
    public float constuctionTime;

    public string GetBuildingInformationString()
    {
        string str = "";
        foreach (ResourceAmount resourceAmount in constructionCostArray)
            str += "<color=#" + resourceAmount.resourceType.colorHex + ">" +
                resourceAmount.resourceType.symbol + ": " + resourceAmount.amount +
                "</color> ";
        return str;
    }
}
