using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/WeaponTypeList")]
public class WeaponTypeListSO : ScriptableObject {
    public List<WeaponTypeSO> list;
}
