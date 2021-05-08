using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowProjectile : MonoBehaviour
{
    private EnemyController enemy;
    private float moveSpeed = 20f;
    private Vector3 lastMoveDirection;
    private float destroyTimer = 2f;

    public static ArrowProjectile RespownArrow(Vector3 position, EnemyController enemy)
    {
        Transform arrowPrefab = Resources.Load<Transform>("ArrowProjectile");
        Transform arrowToInstantiate = Instantiate(arrowPrefab, position, Quaternion.identity);
        ArrowProjectile arrow = arrowToInstantiate.GetComponent<ArrowProjectile>();
        arrow.SetTarget(enemy);
        return arrow;
    }
    private void Update()
    {
        Move();
    }
    private void SetTarget(EnemyController enemy)
    {
        this.enemy = enemy;
    }
    private void Move()
    {
        //Get direction and move towards that direction. If the enemy dies while the arrow is mid-air,
        //Arrow will just go towards to the last direction and destroy itself once it collides with sth.
        Vector3 moveDirection;
        if (enemy != null)
        {
            moveDirection = (enemy.transform.position - transform.position).normalized;
            lastMoveDirection = moveDirection;
        }            
        else
            moveDirection = lastMoveDirection;
        transform.position += moveDirection * moveSpeed * Time.deltaTime;
        //Look towards the direction headed.
        transform.eulerAngles = new Vector3(0, 0, UtilsClass.GetAngleFromVector(moveDirection));
        destroyTimer -= Time.deltaTime;
        //After the timer runs out, destroy the arrow.
        if (destroyTimer < 0)
            Destroy(gameObject);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        EnemyController enemy = collision.GetComponent<EnemyController>();
        if (enemy != null)
        {
            int damageAmount = 10;
            enemy.GetComponent<HealthSystem>().Damage(damageAmount);
            Destroy(gameObject);
        }            
    }
}
