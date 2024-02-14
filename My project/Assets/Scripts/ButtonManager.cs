using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ButtonManager : MonoBehaviour
{
    public Player player;
    public List<Button> sceneButtons;

    [SerializeField] TextMeshProUGUI testerText;
    string test;

    private void Start()
    {
        player = FindFirstObjectByType<Player>();

        //Get all the buttons.
        GameObject[] btnList = GameObject.FindGameObjectsWithTag("btn");
        for(int i = 0; i < btnList.Length; i++)
        {
            sceneButtons.Add(btnList[i].GetComponent<Button>());
        }
        if(player.unlockedWorlds.Count == 0)
        {
            player.unlockedWorlds.Add("Mercury");
        }

        for(int i = 0; i < sceneButtons.Count; i++)
        {
            if (!player.unlockedWorlds.Contains(btnList[i].name))
            { 
                sceneButtons[i].interactable = false;
            }
        }
        for(int i = 0; i < player.unlockedWorlds.Count; i++)
        {
            test += player.unlockedWorlds[i] + ", ";
        }
        testerText.text = test;
    }


}
