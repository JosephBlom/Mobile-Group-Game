using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBrain : MonoBehaviour
{
    [Header("Stats")]
    [SerializeField] int health;
    [Tooltip("The amount of seconds before the shock effect dissapears")]
    [SerializeField] float unshockTime;

    EnemyPathing enemyPathing;

    private void Start()
    {
        enemyPathing = GetComponent<EnemyPathing>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            BulletManager bulletManager = collision.GetComponent<BulletManager>();
            health -= bulletManager.baseDamage;
            if (health <= 0)
            {
                Destroy(gameObject);
            }
            else if (bulletManager.shock)
            {
                enemyPathing.shocked = true;
                StartCoroutine(enemyPathing.resetSpeed(unshockTime));
            }
            if (bulletManager.piercing)
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
