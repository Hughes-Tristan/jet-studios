// Author: Jazzel Radaza
// Created: 2024-11-16
// Description: Stores a list of weapon types for use in weapon selection and placement systems.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/WeaponTypeList")]
public class WeaponTypeListSO : ScriptableObject
{
    public List<WeaponTypeSO> list; // List of all available weapon types
}
