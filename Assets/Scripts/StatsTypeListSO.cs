// Author: Jazzel Radaza
// Created: 2024-11-20
// Description: Stores a list of stat types to manage and reference in the game.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/StatsTypeList")]
public class StatsTypeListSO : ScriptableObject
{
    public List<StatsTypeSO> list; // List of all stat types used in the game
}
