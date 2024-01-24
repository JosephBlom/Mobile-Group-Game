using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPathing : MonoBehaviour
{

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
    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, travelPoints[nextPosition].position, moveSpeed * Time.deltaTime);
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
