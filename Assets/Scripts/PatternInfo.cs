using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/PatternInfo")]
public class PatternInfo : ScriptableObject
{                    
    public float summonInterval;
    public SummonInfo[] data;
    public float coolTime;
    public string description;
}
