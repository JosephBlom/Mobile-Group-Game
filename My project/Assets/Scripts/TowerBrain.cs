using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class TowerBrain : MonoBehaviour
{
    [Header("Tracking Variables")]
    [SerializeField] float targetingRange;
    [SerializeField] LayerMask enemyMask;
    [SerializeField] int rotateSpeed;

    [Header("Shooting Variables")]
    public GameObject bulletPrefab;
    [SerializeField] float fireTime;
    [SerializeField] float bulletSpeed;
    private Transform target;

    //Skill Refrences
    private BasicTowerSkills towerSkills;


    private void Awake()
    {
        towerSkills = new BasicTowerSkills();
    }

    private void Start()
    {
        StartCoroutine(Shoot());
    }

    private void Update()
    {
        if(target == null)
        {
            getTarget();
            return;
        }

        rotateToTarget();

        if (!checkTargetInRange())
        {
            target = null;
        }
    }

    // Tracking Start ----------------------------------------------------------------------

    private void getTarget()
    {
        RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position, targetingRange, (Vector2)transform.position, 0f, enemyMask);
        if(hits.Length > 0)
        {
            target = hits[0].transform;
        }
    }

    private void rotateToTarget()
    {
        float angle = Mathf.Atan2(target.position.y - transform.position.y, target.position.x - transform.position.x) * Mathf.Rad2Deg;
        Quaternion targetRotation = Quaternion.Euler(new Vector3(0f, 0f, angle));
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotateSpeed * Time.deltaTime);
    }

    private bool checkTargetInRange()
    {
        if(target == null)
        {
            return false;
        }
        return Vector2.Distance(target.position, transform.position) <= targetingRange;
    }

    // Tracking End ----------------------------------------------------------------------


    // Shooting Start ----------------------------------------------------------------------

    IEnumerator Shoot()
    {
        if (checkTargetInRange())
        {
            Vector3 shootDirection = (target.position - transform.position).normalized;
            GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
            bullet.GetComponent<Rigidbody2D>().velocity = shootDirection * bulletSpeed;
            bullet.transform.right = shootDirection;
            Destroy(bullet, 2);
        }
        yield return new WaitForSeconds(fireTime);
        StartCoroutine(Shoot());
    }

    // Shooting End ----------------------------------------------------------------------


    private void OnDrawGizmosSelected()
    {
        Handles.color = Color.cyan;
        Handles.DrawWireDisc(transform.position, transform.forward, targetingRange);
    }


}
