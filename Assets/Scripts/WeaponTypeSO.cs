using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/WeaponType")]
public class WeaponTypeSO : ScriptableObject {
    public string nameString;

    public Transform prefab;
    public int price;                  
    public int health;                 
}
