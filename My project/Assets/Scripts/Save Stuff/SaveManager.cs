using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    [SerializeField] Player player;

    private void Start()
    {
        LoadPlayer();
    }

    private void OnApplicationQuit()
    {
        SavePlayer();
    }

    public void SavePlayer()
    {
        SaveSystem.SavePlayer(player);
    }

    public void LoadPlayer()
    {
        PlayerData data = SaveSystem.LoadPlayer();

        player.unlockedWorlds = data.unlockedWorlds;
        player.beatenLevels = data.beatenLevels;
        player.unlockedTowers = data.unlockedTowers;

    }
}
