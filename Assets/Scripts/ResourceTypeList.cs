using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/ResourceTypeList")]
public class ResourceTypeList : ScriptableObject
{
    public List<ResourceType> list;
}
