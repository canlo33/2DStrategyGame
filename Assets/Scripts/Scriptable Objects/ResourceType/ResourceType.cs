using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ResourceType", menuName = "ScriptableObjects/ResourceType")]
public class ResourceType : ScriptableObject
{
    public new string name;
    public string symbol;
    public Sprite sprite;
    public string colorHex;
}
