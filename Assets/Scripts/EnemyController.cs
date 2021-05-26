using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private Building mainBuilding;
    private Transform targetTransform;
    private new Rigidbody2D rigidbody;
    private HealthSystem healthSystem;
    private float checkTargetTimer;
    private float checkTargetCooldown = 0.2f;
    [SerializeField] private Transform enemyDieParticles;

    //This static function will allow us to instantiate new enemies at given position.
    public static EnemyController RespownEnemy(Vector3 position)
    {
        Transform enemyPrefab = Resources.Load<Transform>("Enemy");
        Transform enemyToInstantiate = Instantiate(enemyPrefab, position, Quaternion.identity);
        EnemyController enemy = enemyToInstantiate.GetComponent<EnemyController>();
        return enemy;
    }
    private void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        healthSystem = GetComponent<HealthSystem>();
        healthSystem.OnDied += HealthSystem_OnDied;
        healthSystem.OnDamaged += HealthSystem_OnDamaged;
        mainBuilding = BuildingManager.Instance.GetMainBuilding();
        if(mainBuilding != null)
            targetTransform = mainBuilding.transform;
        checkTargetTimer = Random.Range(0f, checkTargetCooldown);
    }

    private void HealthSystem_OnDamaged(object sender, System.EventArgs e)
    {
        SoundManager.Instance.PlayAudio(4);
    }

    private void HealthSystem_OnDied(object sender, System.EventArgs e)
    {
        SoundManager.Instance.PlayAudio(3);
        Instantiate(enemyDieParticles, transform.position, Quaternion.identity);
        CameraShake.Instance.ShakeCamera(7f, .2f);
        Destroy(gameObject);
    }

    private void Update()
    {
        Move();
        HandleTargeting();
    }
    private void Move()
    {
        //This function will move the enemy towards to targetTransform.
        if (targetTransform != null)
        {
            Vector3 moveDirection = (targetTransform.position - transform.position).normalized;
            float moveSpeed = 6f;
            rigidbody.velocity = moveDirection * moveSpeed;
            checkTargetTimer -= Time.deltaTime;
        }
        else
            rigidbody.velocity = Vector2.zero;
    }
    private void FindNearestBuilding()
    {
        //This function will look for the buildings around and check whoever is the closest. Then it will mark the closest one as target.
        //If there is no building around, it will automatically target the Main Building.
        float maxRadius = 10f;
        Collider2D[] collider2DArray = Physics2D.OverlapCircleAll(transform.position, maxRadius);
        foreach (var collider2D in collider2DArray)
        {
            Building building = collider2D.GetComponent<Building>();
            if (building != null)
                if (targetTransform == null)
                    targetTransform = building.transform;
                else
                    if (Vector3.Distance(transform.position, building.transform.position) <
                        Vector3.Distance(transform.position, targetTransform.position))
                        targetTransform = building.transform;
        }
        if (targetTransform == null && mainBuilding != null)
            targetTransform = mainBuilding.transform;
    }
    private void HandleTargeting()
    {
        checkTargetTimer -= Time.deltaTime;
        if (checkTargetTimer < 0)
        {
            checkTargetTimer = checkTargetCooldown;
            FindNearestBuilding();
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Check if we collided with a building, if so then damage the building, damage and kill itself as well.
        Building building = collision.gameObject.GetComponent<Building>();
        if(building != null)
        {
            HealthSystem healthSystem = building.GetComponent<HealthSystem>();
            healthSystem.Damage(10);
            this.healthSystem.Damage(500);
            Destroy(gameObject);
        }
    }
}
