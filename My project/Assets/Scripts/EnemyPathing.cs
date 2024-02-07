using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPathing : MonoBehaviour
{

    [SerializeField] Rigidbody2D rb;
    [SerializeField] EnemySpawning enemySpawning;
    [SerializeField] float rotateSpeed = 400;

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
            rotateToPoint();
        }
        else
        {
            rb.velocity = direction * moveSpeed;
            rotateToPoint();
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

    private void rotateToPoint()
    {
        float angle = Mathf.Atan2(travelPoints[nextPosition].position.y - transform.position.y, travelPoints[nextPosition].position.x - transform.position.x) * Mathf.Rad2Deg;
        Quaternion targetRotation = Quaternion.Euler(new Vector3(0f, 0f, angle));
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotateSpeed * Time.deltaTime);
    }

    public IEnumerator resetSpeed(float unshockTime)
    {
        yield return new WaitForSeconds(unshockTime);
        moveSpeed *= 2;
    }
}
