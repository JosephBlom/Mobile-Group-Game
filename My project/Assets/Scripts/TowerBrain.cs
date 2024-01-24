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


    private Transform target;

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
        float angle = Mathf.Atan2(target.position.y - transform.position.y, target.position.x - transform.position.x) * Mathf.Rad2Deg - 90;
        Quaternion targetRotation = Quaternion.Euler(new Vector3(0f, 0f, angle));
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotateSpeed * Time.deltaTime);
    }

    private bool checkTargetInRange()
    {
        return Vector2.Distance(target.position, transform.position) <= targetingRange;
    }

    private void OnDrawGizmosSelected()
    {
        Handles.color = Color.cyan;
        Handles.DrawWireDisc(transform.position, transform.forward, targetingRange);
    }


}
