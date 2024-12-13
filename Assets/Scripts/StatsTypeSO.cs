// Author: Jazzel Radaza
// Created: 2024-11-20
// Description: Represents a single type of stat (e.g., money, health) used in the game.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/StatsType")]
public class StatsTypeSO : ScriptableObject
{
    public string nameString; // Name of the stat type (e.g., "Money", "Health")
}
