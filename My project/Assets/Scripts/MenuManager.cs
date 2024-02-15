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
    [Tooltip("This Should Be the Canvas Object For The Tower Not The Actualy Tower.")]
    [SerializeField] GameObject rapidTower;
    [Tooltip("This Should Be the Actual Object For The Tower Not The Actualy Tower.")]
    [SerializeField] GameObject basicTowerObject;
    [Tooltip("This Should Be the Actual Object For The Tower Not The Actualy Tower.")]
    [SerializeField] GameObject longRangeTowerObject;
    [Tooltip("This Should Be the Actual Object For The Tower Not The Actualy Tower.")]
    [SerializeField] GameObject heavyTowerObject;
    [Tooltip("This Should Be the Actual Object For The Tower Not The Actualy Tower.")]
    [SerializeField] GameObject rapidTowerObject;

    Coins playerBank;

    private void Awake()
    {
        playerBank = FindObjectOfType<Coins>();
    }

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
        if (playerBank.coinTotal >= basicTowerObject.GetComponent<TowerBrain>().cost)
        {
            GameObject ship = Instantiate(basicTower, new Vector3(0, 0, 0), Quaternion.identity);
            ship.transform.SetParent(mainCanvas.transform, false);
            playerBank.subtractCoins(basicTowerObject.GetComponent<TowerBrain>().cost);
        }
        else
        {
            Debug.Log("This Tower is Too Expensive!");
        }
    }

    public void BuyLongRangeTower()
    {
        if (playerBank.coinTotal >= longRangeTowerObject.GetComponent<TowerBrain>().cost)
        {
            GameObject ship = Instantiate(longRangeTower, new Vector3(0, 0, 0), Quaternion.identity);
            ship.transform.SetParent(mainCanvas.transform, false);
            playerBank.subtractCoins(longRangeTowerObject.GetComponent<TowerBrain>().cost);
        }
        else
        {
            Debug.Log("This Tower is Too Expensive!");
        }
    }

    public void BuyHeavyTower()
    {
        if(playerBank.coinTotal >= heavyTowerObject.GetComponent<TowerBrain>().cost)
        {
            GameObject ship = Instantiate(heavyTower, new Vector3(0, 0, 0), Quaternion.identity);
            ship.transform.SetParent(mainCanvas.transform, false);
            playerBank.subtractCoins(heavyTowerObject.GetComponent<TowerBrain>().cost);
        }
        else
        {
            Debug.Log("This Tower is Too Expensive!");
        }
    }
    public void BuyRapidTower()
    {
        if (playerBank.coinTotal >= rapidTowerObject.GetComponent<TowerBrain>().cost)
        {
            GameObject ship = Instantiate(rapidTower, new Vector3(0, 0, 0), Quaternion.identity);
            ship.transform.SetParent(mainCanvas.transform, false);
            playerBank.subtractCoins(rapidTowerObject.GetComponent<TowerBrain>().cost);
        }
        else
        {
            Debug.Log("This Tower is Too Expensive!");
        }
    }
}
