using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public string[] unlockedWorlds;
    public string[] beatenLevels;
    public string[] unlockedTowers;
    public string password;
    public string username;

    public PlayerData(Player player)
    {
        unlockedWorlds = player.unlockedWorlds;
        beatenLevels = player.beatenLevels;
        unlockedTowers = player.unlockedTowers;
        password = player.password;
        username = player.username;
    }
}
