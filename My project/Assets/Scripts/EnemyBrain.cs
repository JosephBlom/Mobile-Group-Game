using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBrain : MonoBehaviour
{
    [Header("Stats")]
    [SerializeField] float health;
    [Tooltip("The amount of seconds before the shock effect dissapears.")]
    [SerializeField] float unshockTime;
    [Tooltip("The amount of damage that this enemy will do to the tower.")]
    [SerializeField] float damage;

    EnemyPathing enemyPathing;
    EnemySpawning enemySpawning;

    private void Start()
    {
        enemyPathing = GetComponent<EnemyPathing>();
        enemySpawning = FindFirstObjectByType<EnemySpawning>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            BulletManager bulletManager = collision.GetComponent<BulletManager>();
            health -= bulletManager.baseDamage;
            if (health <= 0)
            {
                enemySpawning.aliveEnemies.RemoveAt(0);
                Destroy(gameObject);
            }
            else if (bulletManager.unlockedAbilities.Contains("Shock"))
            {
                enemyPathing.shocked = true;
                StartCoroutine(enemyPathing.resetSpeed(unshockTime));
            }
            if (bulletManager.unlockedAbilities.Contains("Piercing"))
            {
                return;
            }
            else
            {
                Destroy(collision.gameObject);
            }
        }
    }
}
