using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingConstruction : MonoBehaviour
{
    private float constructionTimer;
    private float constructionCooldown;
    private BuildingType buildingType;
    private BoxCollider2D boxCollider2D;
    private SpriteRenderer spriteRenderer;
    private Building building;
    private Material constructionMaterial;

    public static BuildingConstruction Create(Vector3 position, BuildingType buildingType)
    {
        Transform prefab = Resources.Load<Transform>("BuildingConstruction");
        Transform buildingConstructionToInstantiate = Instantiate(prefab, position, Quaternion.identity);
        BuildingConstruction buildingConstruction = buildingConstructionToInstantiate.GetComponent<BuildingConstruction>();
        buildingConstruction.SetBuildingType(buildingType);
        return buildingConstruction;
    }
    private void Awake()
    {
        boxCollider2D = GetComponent<BoxCollider2D>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        constructionMaterial = spriteRenderer.material;
        building = GetComponent<Building>();
    }
    private void Update()
    {
        constructionTimer -= Time.deltaTime;
        constructionMaterial.SetFloat("_Progress", GetConstructionTimerNormalized());
        if (constructionTimer <= 0)
        {
            Instantiate(buildingType.prefab, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }         
    }
    private void SetBuildingType(BuildingType buildingType)
    {
        constructionCooldown = buildingType.constuctionTime;
        constructionTimer = constructionCooldown;
        this.buildingType = buildingType;
        GetComponent<BuildingTypeHolder>().buildingType = buildingType;
        boxCollider2D.size = buildingType.prefab.GetComponent<BoxCollider2D>().size;
        boxCollider2D.offset = buildingType.prefab.GetComponent<BoxCollider2D>().offset;
        spriteRenderer.sprite = buildingType.sprite;
    }
    public float GetConstructionTimerNormalized()
    {
        return 1 - constructionTimer / constructionCooldown;
    }
}
