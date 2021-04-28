using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteSortingOrder : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    [SerializeField]private float positionOffset;
    [SerializeField] private bool runOnce = true;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    private void LateUpdate()
    {
        spriteRenderer.sortingOrder = (int)(-(transform.position.y + positionOffset) * 5f);
        if (runOnce)
            Destroy(this);
    }
}
