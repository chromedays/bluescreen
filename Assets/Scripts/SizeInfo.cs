using System.Collections;
using System.Collections.Generic;
using UnityEngine;                        

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/SizeInfo")]
public class SizeInfo : ScriptableObject
{
    public float sizeX; //[RATIO!]
    public float sizeY; //[RATIO!]
};
