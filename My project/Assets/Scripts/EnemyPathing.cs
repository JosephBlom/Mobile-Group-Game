using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPathing : MonoBehaviour
{

    [SerializeField] Rigidbody2D rb;

    public Transform[] travelPoints;
    public float moveSpeed;
    public int nextPosition;

    GameObject pathPoints;

    private void Start()
    {
        nextPosition = 1;
        pathPoints = GameObject.FindGameObjectWithTag("List");
        travelPoints = pathPoints.GetComponentsInChildren<Transform>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector2 direction = (travelPoints[nextPosition].position - transform.position).normalized;
        rb.velocity = direction * moveSpeed;
        //transform.position = Vector2.MoveTowards(transform.position, travelPoints[nextPosition].position, moveSpeed * Time.deltaTime);
        if (Vector2.Distance(transform.position, travelPoints[nextPosition].position) < 0.2f)
        {
            nextPosition++;
            if(nextPosition > travelPoints.Length -1)
            {
                nextPosition = travelPoints.Length - 1;
            }
        }
    }
}
