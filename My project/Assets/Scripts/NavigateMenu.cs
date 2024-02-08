using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class NavigateMenu : MonoBehaviour
{
    public void GoToMercuryLevels()
    {
        SceneManager.LoadScene("MercuryMenu");
    }

    public void GoToMarsLevels()
    {
        SceneManager.LoadScene("MarsMenu");
    }
}
