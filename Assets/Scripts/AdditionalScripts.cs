using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct EnemyWave
{
	public GameObject enemyPrefab;
	public int spawnNumber;
	public float timeBetweenSpawns;
}


public enum PowerupType { SpeedIncrease, SlowMotion }
