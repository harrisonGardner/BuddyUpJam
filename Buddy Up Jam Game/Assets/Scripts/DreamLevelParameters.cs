using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DreamLevelParameters", menuName = "LevelData/DreamLevelParameters", order = 1)]
public class DreamLevelParameters : ScriptableObject
{
    public float spawnRate;

    public int maxAlive;

    public int amountToSpawn;
}
