using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPathing : MonoBehaviour
{

    [SerializeField] Rigidbody2D rb;
    [SerializeField] EnemySpawning enemySpawning;

    public Transform[] travelPoints;
    public float moveSpeed;
    public int nextPosition;
    public int startPosition;

    public bool shocked;

    string[] pathNames = { "Path1", "Path2", "Path3", "Path4", "Path5" };
    GameObject pathPoints;

    private void Start()
    {
        nextPosition = 1;
        pathPoints = GameObject.FindGameObjectWithTag(pathNames[startPosition]);
        travelPoints = pathPoints.GetComponentsInChildren<Transform>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector2 direction = (travelPoints[nextPosition].position - transform.position).normalized;
        if (shocked)
        {
            rb.velocity = direction * (moveSpeed/2);
        }
        else
        {
            rb.velocity = direction * moveSpeed;
        }
        
        if (Vector2.Distance(transform.position, travelPoints[nextPosition].position) < 0.2f)
        {
            nextPosition++;
            if(nextPosition > travelPoints.Length -1)
            {
                nextPosition = travelPoints.Length - 1;
            }
        }
    }

    public IEnumerator resetSpeed(float unshockTime)
    {
        yield return new WaitForSeconds(unshockTime);
        moveSpeed *= 2;
    }
}
