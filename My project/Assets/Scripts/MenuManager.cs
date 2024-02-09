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

    [Header("Tower Prefabs")]
    [Tooltip("This Should Be the Canvas Object For The Tower Not The Actualy Tower.")]
    [SerializeField] GameObject basicTower;
    [Tooltip("This Should Be the Canvas Object For The Tower Not The Actualy Tower.")]
    [SerializeField] GameObject longRangeTower;
    [Tooltip("This Should Be the Canvas Object For The Tower Not The Actualy Tower.")]
    [SerializeField] GameObject heavyTower;

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

    public void BuyBasicTower()
    {
        GameObject ship = Instantiate(basicTower, new Vector3(0, 0, 0), Quaternion.identity);
        ship.transform.SetParent(mainCanvas.transform, false);
    }

    public void BuyLongRangeTower()
    {
        GameObject ship = Instantiate(longRangeTower, new Vector3(0, 0, 0), Quaternion.identity);
        ship.transform.SetParent(mainCanvas.transform, false);
    }

    public void BuyHeavyTower()
    {
        GameObject ship = Instantiate(heavyTower, new Vector3(0, 0, 0), Quaternion.identity);
        ship.transform.SetParent(mainCanvas.transform, false);
    }
}
