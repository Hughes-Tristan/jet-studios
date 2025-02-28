
// Developer(s): Evita Kanaan
// Last Updated: 11-21-24
// Wave Manager Class

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro; 

public class WaveManager : MonoBehaviour
{
    // Reference to the wave bar image
    public Image waveBar;

    // Object of wave manager, checks if instance of wave manager exists
    public static WaveManager Instance { get; private set; }

    // Wave management variables
    public int currentWave = 1;
    public int totalWaves = 5;

    // Grill count variables
    public TMP_Text grillCountText; // Use TMP_Text for TextMeshPro
    private int totalGrills = 5; // Total number of grills
    private int intactGrills = 5; // Number of intact grills

    // Money counter variables
    public TMP_Text moneyCounterText; // Use TMP_Text for TextMeshPro
    private int money = 100; // Current amount of money

    // Purchase box variables
    public Image purchaseBox; // Reference to the purchase box image
    public int purchaseCost = 100; // Cost to purchase

    // Singleton pattern
    void Awake()
    {
        if (Instance == null) { // If instance of wave manager is null or not
            Instance = this; 
        } else {
            Destroy(gameObject); // To not create a duplicate instance
        }

    }

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

    // this function operates fundamentally exactly like earn money
    // takes the amount of money you spent and subtracts from your overall money
    // then updates the money counter
    public void spendMoney(int amount)
    {
        money -= amount;
        UpdateMoneyCounter();
    }

    private void UpdateMoneyCounter()
    {
        moneyCounterText.text = $"Money: ${money}"; // Update the UI text
    }

    // this function is a getter function used to get the amount of money
    public int getMoney()
    {
        return money;
    }

}
