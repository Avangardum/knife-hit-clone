using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/AppleData")]
public class AppleData : ScriptableObject
{
    [Range(0, 1)] public float SpawnChance;
    public float DistanceFromLogCenter;
    public float MinimalAngleDifferenceWithInitialKnifeDeg;
}
