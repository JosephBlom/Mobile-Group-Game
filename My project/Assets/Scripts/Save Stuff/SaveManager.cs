using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    [SerializeField] Player player;
    [SerializeField] LoginManager loginManager;

    private void Start()
    {
        loginManager = FindFirstObjectByType<LoginManager>();
        LoadPlayer();
    }

    private void OnApplicationQuit()
    {
        SavePlayer();
    }

    public void SavePlayer()
    {
        SaveSystem.SavePlayer(player, loginManager.username);
    }

    public void LoadPlayer()
    {
        PlayerData data = SaveSystem.LoadPlayer(loginManager.username, loginManager.password);

        player.unlockedWorlds = data.unlockedWorlds;
        player.unlockedLevels = data.unlockedLevels;
        player.unlockedTowers = data.unlockedTowers;
        player.password = data.password;
        player.username = data.username;

    }
}
