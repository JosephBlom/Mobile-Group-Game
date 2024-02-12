using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletManager : MonoBehaviour
{
    [Header("Stats")]
    public float baseDamage = 1;
    public List<string> possibleAbilities = new List<string>();
    public List<string> unlockedAbilities = new List<string>();

    [Tooltip("Leave This Space Empty")]
    public Transform target;
    public bool homing;

    Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (homing)
        {
            chaseEnemy();
        }
    }

    void chaseEnemy()
    {
        if (target == null)
        {
            Destroy(gameObject);
            return;
        }
        float angle = Mathf.Atan2(target.position.y - transform.position.y, target.position.x - transform.position.x) * Mathf.Rad2Deg;
        Quaternion targetRotation = Quaternion.Euler(new Vector3(0f, 0f, angle));
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, 400 * Time.deltaTime);

        Vector3 shootDirection = (target.position - transform.position).normalized;
        rb.velocity = shootDirection * 5;
        gameObject.transform.right = shootDirection;
    }

    public void UnlockAbility(string abiltyName)
    {
        unlockedAbilities.Add(abiltyName);
    }

}
