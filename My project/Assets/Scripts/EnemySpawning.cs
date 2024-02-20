using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemySpawning : MonoBehaviour
{
    [SerializeField] int waveAmount = 1;

    public List<Vector3> enemySpawns = new List<Vector3>();
    public List<Enemy> enemies = new List<Enemy>();
    public int currWave;
    public int waveValue;
    public List<GameObject> enemiesToSpawn = new List<GameObject>();
    public List<GameObject> aliveEnemies = new List<GameObject>();
    public int waveDuration;

    private float waveTimer;
    private float spawnInterval;
    private float spawnTimer;

    int coinsPerWave = 200;
    Coins coin;

    Player player;

    void Start()
    {
        player = FindObjectOfType<Player>();
        coin = FindObjectOfType<Coins>();
        GenerateWave();
    }

    void FixedUpdate()
    {
        if(currWave > waveAmount)
        {
            int currentCount = SceneManager.sceneCount;
            if (currentCount > 3)
            {
                currentCount = 3;
            }
            currentCount++;
            int nextIndex = SceneManager.GetActiveScene().buildIndex;
            nextIndex++;
            string indexPath = SceneUtility.GetScenePathByBuildIndex(nextIndex);
            string[] indexList = indexPath.Split('/');
            string[] finalList = indexList[3].Split('.');
            player.unlockedLevels.Add(finalList[0]);
            SaveSystem.SavePlayer(player, player.username);
            SceneManager.LoadScene("LevelSelect");
        }
        else
        {
            if (spawnTimer <= 0)
            {
                if (enemiesToSpawn.Count > 0)
                {
                    int selectedSpawn = Random.Range(0, enemySpawns.Count);
                    enemiesToSpawn[0].GetComponent<EnemyPathing>().startPosition = selectedSpawn;
                    Instantiate(enemiesToSpawn[0], enemySpawns[selectedSpawn], Quaternion.identity);
                    aliveEnemies.Add(enemiesToSpawn[0]);
                    enemiesToSpawn.RemoveAt(0);
                    spawnTimer = spawnInterval;
                }
                else if (aliveEnemies.Count <= 0)
                {
                    waveTimer = 0;
                    coin.addCoins(coinsPerWave);
                    coinsPerWave += 50;
                    currWave++;
                    GenerateWave();
                }
            }
            else
            {
                spawnTimer -= Time.fixedDeltaTime;
                waveTimer -= Time.fixedDeltaTime;
            }
        }

        
    }

    public void GenerateWave()
    {
        waveValue = currWave * 8;
        GenerateEnemies();

        spawnInterval = waveDuration / enemiesToSpawn.Count;
        waveTimer = waveDuration;
    }

    public void GenerateEnemies()
    {
        List<GameObject> generatedEnemies = new List<GameObject>();
        while (waveValue > 0)
        {
            int randEnemyId = Random.Range(0, enemies.Count);
            int randEnemyCost = enemies[randEnemyId].cost;
            if (waveValue - randEnemyCost >= 0)
            {
                generatedEnemies.Add(enemies[randEnemyId].enemyPrefab);
                waveValue -= randEnemyCost;
            }
            else if (waveValue <= 0)
            {
                break;
            }
        }
        enemiesToSpawn.Clear();
        enemiesToSpawn = generatedEnemies;
    }
}

[System.Serializable]
public class Enemy
{
    public GameObject enemyPrefab;
    public int cost;
}

