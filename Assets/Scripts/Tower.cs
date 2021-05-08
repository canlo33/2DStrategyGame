using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    private EnemyController targetEnemy;
    private float shootTimer;
    [SerializeField] private float shootCooldown;
    private float checkTargetTimer;
    private float checkTargetCooldown = .2f;
    private float towerRange = 20f;
    [SerializeField] private Transform arrowSpawnPoint;
    private void Update()
    {
        HandleTargeting();
        HandleAttack();
    }
    private void LookForTarget()
    {
        //This function will look for the buildings around and check whoever is the closest. Then it will mark the closest one as target.
        //If there is no building around, it will automatically target the Main Building.
        Collider2D[] collider2DArray = Physics2D.OverlapCircleAll(transform.position, towerRange);
        foreach (var collider2D in collider2DArray)
        {
            EnemyController enemy = collider2D.GetComponent<EnemyController>();
            if (enemy != null)
                if (targetEnemy == null)
                    targetEnemy = enemy;
                else
                    if (Vector3.Distance(transform.position, enemy.transform.position) <
                        Vector3.Distance(transform.position, targetEnemy.transform.position))
                    targetEnemy = enemy;
        }
    }
    private void HandleTargeting()
    {
        checkTargetTimer -= Time.deltaTime;
        if (checkTargetTimer < 0)
        {
            checkTargetTimer = checkTargetCooldown;
            LookForTarget();
        }
    }
    private void HandleAttack()
    {
        shootTimer -= Time.deltaTime;
        if(shootTimer < 0)
        {
            shootTimer = shootCooldown;
            if (targetEnemy != null)
                ArrowProjectile.RespownArrow(arrowSpawnPoint.position, targetEnemy);
        }
    }
}
