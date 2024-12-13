// Author: Jazzel Radaza
// Created: 2024-11-18
// Last Modified: 2024-12-05 by Jazzel (added money update notifications)
// Description: Manages various player stats (e.g., money, health) and triggers events when certain stats change.

using System;
using System.Collections.Generic;
using UnityEngine;

public class StatsManager : MonoBehaviour
{
    public static StatsManager Instance { get; private set; } // Singleton instance

    private Dictionary<StatsTypeSO, int> statsAmountDictionary; // Stores stats and their values

    // Event to notify subscribers when the money stat changes
    public event Action<int> OnMoneyChanged;

    private void Awake()
    {
        Instance = this;

        statsAmountDictionary = new Dictionary<StatsTypeSO, int>();
        StatsTypeListSO statsTypeList = Resources.Load<StatsTypeListSO>(typeof(StatsTypeListSO).Name);

        // Initialize all stats to zero
        foreach (StatsTypeSO statsType in statsTypeList.list)
        {
            statsAmountDictionary[statsType] = 0;
        }
    }

    private void Update()
    {
        // Test code: Add 10 to the first stat (assumed to be money) when the 'M' key is pressed
        if (Input.GetKeyDown(KeyCode.M))
        {
            StatsTypeListSO statsTypeList = Resources.Load<StatsTypeListSO>(typeof(StatsTypeListSO).Name);
            AddStatsAmount(statsTypeList.list[0], 10); // Add to money
            TestLogStatsAmountDictionary();
        }
    }

    // Debugging utility to log the current stats dictionary
    private void TestLogStatsAmountDictionary()
    {
        foreach (StatsTypeSO statsType in statsAmountDictionary.Keys)
        {
            Debug.Log(statsType.nameString + ": " + statsAmountDictionary[statsType]);
        }
    }

    // Retrieves the value of a specific stat
    public int GetStatsAmount(StatsTypeSO statsType)
    {
        if (statsAmountDictionary.ContainsKey(statsType))
        {
            return statsAmountDictionary[statsType];
        }

        Debug.LogError($"Stat type {statsType.nameString} not found in dictionary!");
        return 0; // Return 0 if the stat doesn't exist
    }

    // Adds an amount to the specified stat
    public void AddStatsAmount(StatsTypeSO statsType, int amount)
    {
        if (statsAmountDictionary.ContainsKey(statsType))
        {
            statsAmountDictionary[statsType] += amount;
            TriggerMoneyUpdateIfNecessary(statsType); // Notify if the stat is money
        }
        else
        {
            Debug.LogError($"Stat type {statsType.nameString} not found in dictionary!");
        }
    }

    // Subtracts an amount from the specified stat
    public void SubtractStatsAmount(StatsTypeSO statsType, int amount)
    {
        if (statsAmountDictionary.ContainsKey(statsType))
        {
            statsAmountDictionary[statsType] -= amount;

            // Ensure stats do not drop below 0
            if (statsAmountDictionary[statsType] < 0)
            {
                statsAmountDictionary[statsType] = 0;
            }

            TriggerMoneyUpdateIfNecessary(statsType); // Notify if the stat is money
        }
        else
        {
            Debug.LogError($"Stat type {statsType.nameString} not found in dictionary!");
        }
    }

    // Triggers the OnMoneyChanged event if the stat is money
    private void TriggerMoneyUpdateIfNecessary(StatsTypeSO statsType)
    {
        StatsTypeListSO statsTypeList = Resources.Load<StatsTypeListSO>(typeof(StatsTypeListSO).Name);

        // Assuming money is the first stat in the list
        if (statsType == statsTypeList.list[0])
        {
            int currentMoney = statsAmountDictionary[statsType];
            OnMoneyChanged?.Invoke(currentMoney); // Notify listeners
        }
    }
}
