using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitForWorlds : MonoBehaviour
{
    
    
    public void goBack()
    {
        SceneManager.LoadScene("LevelSelect");
    }



}
