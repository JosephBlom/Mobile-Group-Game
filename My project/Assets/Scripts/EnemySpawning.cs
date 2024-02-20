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

    int nextIndex;
    string indexPath;
    string[] indexList;
    string[] finalList;

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
            string[] splitName = SceneManager.GetActiveScene().name.Split(' ');
            if (splitName[1].Equals("3"))
            {
                if (player.unlockedWorlds.Contains("Uranus"))
                {
                    player.unlockedWorlds.Add("Neptune");
                    player.unlockedLevels.Add("Neptune 1");
                }
                else if (player.unlockedWorlds.Contains("Saturn"))
                {
                    player.unlockedWorlds.Add("Uranus");
                    player.unlockedLevels.Add("Uranus 1");
                }
                else if (player.unlockedWorlds.Contains("Jupiter"))
                {
                    player.unlockedWorlds.Add("Saturn");
                    player.unlockedLevels.Add("Saturn 1");
                }
                else if (player.unlockedWorlds.Contains("Mars"))
                {
                    player.unlockedWorlds.Add("Jupiter");
                    player.unlockedLevels.Add("Jupiter 1");
                }
                else if (player.unlockedWorlds.Contains("Earth"))
                {
                    player.unlockedWorlds.Add("Mars");
                    player.unlockedLevels.Add("Mars 1");
                }
                else if (player.unlockedWorlds.Contains("Venus"))
                {
                    player.unlockedWorlds.Add("Earth");
                    player.unlockedLevels.Add("Earth 1");
                }
                else if (player.unlockedWorlds.Contains("Mercury"))
                {
                    player.unlockedWorlds.Add("Venus");
                    player.unlockedLevels.Add("Venus 1");
                }
            }
            nextIndex = SceneManager.GetActiveScene().buildIndex;
            nextIndex++;
            indexPath = SceneUtility.GetScenePathByBuildIndex(nextIndex);
            indexList = indexPath.Split('/');
            finalList = indexList[3].Split('.');
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

