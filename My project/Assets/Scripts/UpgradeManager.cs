using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class UpgradePath1
{
    int baseDamage;
    public bool shock;
    public bool piercing;
}

[System.Serializable]
public class UpgradePath2
{
    public int baseDamage;
    public bool shock;
    public bool piercing;
}

[System.Serializable]
public class UpgradePath3
{
    public int baseDamage;
    public bool shock;
    public bool piercing;
}


public class UpgradeManager : MonoBehaviour
{ 
    [SerializeField] GameObject bullet;


    BulletManager bulletManager;
    TowerBrain towerBrain;

    private void Start()
    {
        bulletManager = bullet.GetComponent<BulletManager>();
        towerBrain = GetComponent<TowerBrain>();
    }

    public void LevelUpPath1()
    {
        //Set varibles on towerBrain and bulletManager
    }

    public void LevelUpPath2()
    {
        //Set varibles on towerBrain and bulletManager
    }

    public void LevelUpPath3()
    {
        //Set varibles on towerBrain and bulletManager
    }

    
}
