using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{ 
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit2D[] allHits = Physics2D.RaycastAll(Input.mousePosition, -Vector2.up);
            if (allHits[0].collider.CompareTag("BasicTower"))
            {
                Debug.Log("Hit Tower");
            }
        }
    }
}
