// Author: Jazzel Radaza
// Created: 2024-11-16
// Description: Represents a single weapon type, including its name, prefab, price, and health.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/WeaponType")]
public class WeaponTypeSO : ScriptableObject
{
    public string nameString; // Name of the weapon (e.g., "Axe Knight")

    public Transform prefab; // Prefab associated with the weapon
    public int price;        // Cost of the weapon in in-game currency
    public int health;       // Health value of the weapon
}
