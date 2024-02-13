using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonManager : MonoBehaviour
{
    public Player player;
    public List<Button> sceneButtons;

    private void Start()
    {
        player = FindFirstObjectByType<Player>();

        //Get all the buttons.
        GameObject[] btnList = GameObject.FindGameObjectsWithTag("btn");
        for(int i = 0; i < btnList.Length; i++)
        {
            sceneButtons.Add(btnList[i].GetComponent<Button>());
        }

        for(int i = 0; i < sceneButtons.Count; i++)
        {
            if (!player.unlockedWorlds.Contains(btnList[i].name))
            {
                sceneButtons[i].interactable = false;
            }
        }


    }


}
