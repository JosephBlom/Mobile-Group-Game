using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class TowerBrain : MonoBehaviour
{
    [Header("Tracking Variables")]
    public float targetingRange;
    public int targetingRangeLvl;
    [SerializeField] LayerMask enemyMask;
    [SerializeField] int rotateSpeed;

    [Header("Shooting Variables")]
    public GameObject bulletPrefab;
    [SerializeField] GameObject shootSpot;
    [SerializeField] float fireTime;
    public float attackSpeed;
    public int attackSpeedLvl;
    [SerializeField] float bulletSpeed;
    public float baseDamage = 3;
    public float damageMult;
    public int damageLvl;

    [Header("Extra Variables")]
    public int cost;
    public List<string> possibleAbilities = new List<string>();
    public List<string> unlockedAbilities = new List<string>();

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
            GameObject bullet = Instantiate(bulletPrefab, shootSpot.transform.position, Quaternion.identity);
            applyBuffs(bullet);
            bullet.GetComponent<Rigidbody2D>().velocity = shootDirection * bulletSpeed;
            bullet.transform.right = shootDirection;
            if (unlockedAbilities.Contains("HomingBullets"))
            {
                bullet.GetComponent<BulletManager>().target = target;
                bullet.GetComponent<BulletManager>().homing = true;
            }
            else
            {
                Destroy(bullet, 2);
            }
        }
        yield return new WaitForSeconds(fireTime/ (1+attackSpeed));
        StartCoroutine(Shoot());
    }

    // Shooting End ----------------------------------------------------------------------

    void applyBuffs(GameObject bullet)
    {
        bullet.GetComponent<BulletManager>().unlockedAbilities = this.unlockedAbilities;
        bullet.GetComponent<BulletManager>().baseDamage = this.baseDamage * (damageMult + 1);
    }

    //private void OnDrawGizmosSelected()
    //{
    //    Handles.color = Color.cyan;
    //    Handles.DrawWireDisc(transform.position, transform.forward, targetingRange);
    //}


}
