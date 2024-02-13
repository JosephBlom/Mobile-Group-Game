using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public List<string> unlockedWorlds;
    public List<string> unlockedLevels;
    public List<string> unlockedTowers;
    public string password;
    public string username;

    public PlayerData(Player player)
    {
        unlockedWorlds = player.unlockedWorlds;
        unlockedLevels = player.unlockedLevels;
        unlockedTowers = player.unlockedTowers;
        password = player.password;
        username = player.username;
    }
}
