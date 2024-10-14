using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro; // Make sure to include this for TextMeshPro

public class WaveManager : MonoBehaviour
{
    // Reference to the wave bar image
    public Image waveBar;

    // Wave management variables
    public int currentWave = 1;
    public int totalWaves = 5;

    // Grill count variables
    public TMP_Text grillCountText; // Use TMP_Text for TextMeshPro
    private int totalGrills = 5; // Total number of grills
    private int intactGrills = 5; // Number of intact grills

    // Money counter variables
    public TMP_Text moneyCounterText; // Use TMP_Text for TextMeshPro
    private int money = 20; // Current amount of money

    // Purchase box variables
    public Image purchaseBox; // Reference to the purchase box image
    public int purchaseCost = 100; // Cost to purchase

    void Start()
    {
        // Update the displays at the start
        UpdateGrillCount();
        UpdateMoneyCounter();
    }

    void Update()
    {
        // Update the wave progress bar
        waveBar.fillAmount = (float)currentWave / totalWaves;

        // Update the purchase box color based on money
        if (money >= purchaseCost)
        {
            purchaseBox.color = Color.green; // Change to available color
        }
        else
        {
            purchaseBox.color = Color.gray; // Change to unavailable color
        }
    }

    public void NextWave()
    {
        if (currentWave < totalWaves)
        {
            currentWave++;
            UpdateGrillCount(); // Optionally refresh the grill count after each wave
        }
    }

    public void UpdateGrillCount()
    {
        grillCountText.text = $"{intactGrills}/{totalGrills}"; // Update grill count display
    }

    public void EarnMoney(int amount)
    {
        money += amount; // Add the earned amount to money
        UpdateMoneyCounter(); // Update the money display
    }

    private void UpdateMoneyCounter()
    {
        moneyCounterText.text = $"Money: ${money}"; // Update the UI text
    }

    // Additional methods for managing grills, money, etc., can be added here
}
