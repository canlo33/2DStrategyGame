using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="BuildingType",menuName = "ScriptableObjects / BuildingType")]
public class BuildingType : ScriptableObject
{
    public new string name = "Enter Building Name";
    public Transform prefab;
    public ResourceGeneratorData resourceGeneratorData;
}
