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

[System.Serializable]
public class WeaponInfoAndType
{
	public WeaponType type;
	public WeaponStats stats;
}

[System.Serializable]
public struct ProjectilePoolInfo
{
	public WeaponType weaponType;
	public GameObject projectilePrefab;
	public ObjectPool projectilePool;
}

public enum PowerupType { SpeedIncrease, SlowMotion, HealthKit, Explosion }

public enum WeaponType { Handgun, AssaultRifle, RocketLauncher, SniperRifle, Blaster, UZI }

public enum Perk { None, FasterThanLight, Surgeon, Dodger, RandomWeapon }
