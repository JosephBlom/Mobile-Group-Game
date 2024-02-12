using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Coins : MonoBehaviour
{ 
    public int coinTotal = 300;
    [SerializeField] TextMeshProUGUI coinText;

    private void Start()
    {
        updateCoinText();
    }

    void updateCoinText()
    {
        coinText.text = "Coins: " + coinTotal;
    }

    public void addCoins(int coins)
    {
        coinTotal += coins;
        updateCoinText();
    }

    public void subtractCoins(int coins)
    {
        coinTotal -= coins;
        updateCoinText();
    }
}
