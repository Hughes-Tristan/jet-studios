using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/StatsTypeList")]
public class StatsTypeListSO : ScriptableObject{
    public List<StatsTypeSO> list;
}
