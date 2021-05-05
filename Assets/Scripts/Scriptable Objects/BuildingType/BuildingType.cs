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
    public BuildingCost[] constructionCostArray;
    public int maxHealth;

    public string GetBuildingInformationString()
    {
        string str = "";
        foreach (BuildingCost constuctionCost in constructionCostArray)
            str += "<color=#" + constuctionCost.resourceType.colorHex + ">" +
                constuctionCost.resourceType.symbol + ": " + constuctionCost.costAmount +
                "</color> ";
        return str;
    }
}
[System.Serializable]
public class BuildingCost
{
    public ResourceType resourceType;
    public int costAmount;
}
