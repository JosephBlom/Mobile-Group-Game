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
    public void GoToEarthLevels()
    {
        SceneManager.LoadScene("EarthMenu");
    }
    public void GoToNeptuneLevels()
    {
        SceneManager.LoadScene("NeptuneMenu");
    }
    public void GoToSaturnLevels()
    {
        SceneManager.LoadScene("SaturnMenu");
    }
    public void GoToJupiterLevels()
    {
        SceneManager.LoadScene("JupiterMenu");
    }
    public void GoToUranusLevels()
    {
        SceneManager.LoadScene("UranusMenu");
    }
    public void GoToVenusLevels()
    {
        SceneManager.LoadScene("VenusMenu");
    }

    public void GoToMercuryLevel1()
    {
        SceneManager.LoadScene("Mercury 1");
    }
    public void GoToMercuryLevel2()
    {
        SceneManager.LoadScene("Mercury 2");
    }
    public void GoToMercuryLevel3()
    {
        SceneManager.LoadScene("Mercury 3");
    }
}
