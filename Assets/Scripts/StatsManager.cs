using System;
using System.Collections.Generic;
using UnityEngine;

public class StatsManager : MonoBehaviour {
    public static StatsManager Instance { get; private set; }

    private Dictionary<StatsTypeSO, int> statsAmountDictionary;

    // Event to notify money updates
    public event Action<int> OnMoneyChanged;

    private void Awake() {
        Instance = this;

        statsAmountDictionary = new Dictionary<StatsTypeSO, int>();
        StatsTypeListSO statsTypeList = Resources.Load<StatsTypeListSO>(typeof(StatsTypeListSO).Name);

        foreach (StatsTypeSO statsType in statsTypeList.list) {
            statsAmountDictionary[statsType] = 0; // Initialize all stats to 0
        }
    }
    private void Update(){
        if (Input.GetKeyDown(KeyCode.M)){
            StatsTypeListSO statsTypeList = Resources.Load<StatsTypeListSO>(typeof(StatsTypeListSO).Name);
            AddStatsAmount(statsTypeList.list[0], 10);
            TestLogStatsAmountDictionary();
        }
    }
    private void TestLogStatsAmountDictionary(){
        foreach(StatsTypeSO statsType in statsAmountDictionary.Keys){
            Debug.Log(statsType.nameString + ": " + statsAmountDictionary[statsType]);
        }
    }

    public int GetStatsAmount(StatsTypeSO statsType) {
        if (statsAmountDictionary.ContainsKey(statsType)) {
            return statsAmountDictionary[statsType];
        }
        Debug.LogError($"Stat type {statsType.nameString} not found in dictionary!");
        return 0;
    }

    public void AddStatsAmount(StatsTypeSO statsType, int amount) {
        if (statsAmountDictionary.ContainsKey(statsType)) {
            statsAmountDictionary[statsType] += amount;
            TriggerMoneyUpdateIfNecessary(statsType);
        } else {
            Debug.LogError($"Stat type {statsType.nameString} not found in dictionary!");
        }
    }

    public void SubtractStatsAmount(StatsTypeSO statsType, int amount) {
        if (statsAmountDictionary.ContainsKey(statsType)) {
            statsAmountDictionary[statsType] -= amount;
            if (statsAmountDictionary[statsType] < 0) {
                statsAmountDictionary[statsType] = 0; // Prevent stats from going negative
            }
            TriggerMoneyUpdateIfNecessary(statsType);
        } else {
            Debug.LogError($"Stat type {statsType.nameString} not found in dictionary!");
        }
    }

    private void TriggerMoneyUpdateIfNecessary(StatsTypeSO statsType) {
        // Trigger the OnMoneyChanged event if the stat is money
        StatsTypeListSO statsTypeList = Resources.Load<StatsTypeListSO>(typeof(StatsTypeListSO).Name);
        if (statsType == statsTypeList.list[0]) { // Assume money is the first stat
            int currentMoney = statsAmountDictionary[statsType];
            OnMoneyChanged?.Invoke(currentMoney);
        }
    }
}
