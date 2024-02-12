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
    [Tooltip("If the enemy is on fire.")]
    [SerializeField] bool onFire = false;
    [Tooltip("Amount of coins dropped on death.")]
    [SerializeField] int coins;

    EnemyPathing enemyPathing;
    EnemySpawning enemySpawning;
    Coins coin;

    private void Start()
    {
        coin = FindObjectOfType<Coins>();
        enemyPathing = GetComponent<EnemyPathing>();
        enemySpawning = FindFirstObjectByType<EnemySpawning>();
    }

    private void FixedUpdate()
    {
        if (onFire)
        {
            health -= 0.2f;
        }
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
                coin.addCoins(coins);
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
            if (bulletManager.unlockedAbilities.Contains("FireShot"))
            {
                onFire = true;
            }
        }
    }
}
