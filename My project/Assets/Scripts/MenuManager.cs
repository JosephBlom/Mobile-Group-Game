using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    [Header("Canvas Variables")]
    [Tooltip("The is the main canvas of the level which conatins all other canvas objects.")]
    [SerializeField] Canvas mainCanvas;
    [Tooltip("This is the canvas that has the store for towers.")]
    [SerializeField] Canvas shopCanvas;

    [Header("Ship Prefabs")]
    [SerializeField] GameObject ship1;

    public void DisableCanvas()
    {
        shopCanvas.enabled = false;
    }

    private void Start()
    {
        DisableCanvas();
    }

    public void OpenShop()
    {
        shopCanvas.enabled = true;
    }

    public void CloseShop()
    {
        shopCanvas.enabled = false;
    }

    public void BuyShip1()
    {
        GameObject ship = Instantiate(ship1, new Vector3(0, 0, 0), Quaternion.identity);
        ship.transform.SetParent(mainCanvas.transform, false);
    }
}
