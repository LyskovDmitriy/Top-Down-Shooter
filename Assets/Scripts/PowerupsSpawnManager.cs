using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupsSpawnManager : MonoBehaviour 
{

	public static PowerupsSpawnManager instance;


	public GameObject[] powerups; //TODO Can be reorganized through ScriptableObject
	public GameObject[] weapons;
	public GameObject firstPowerupToSpawn;
	public float chanceToSpawnAnyPowerup;
	public float chanceForPowerupToBeWeapon;


	private bool hasSpawnedFirstPowerup;


	public void TrySpawnPowerup(Vector3 position)
	{
		if (firstPowerupToSpawn!= null && !hasSpawnedFirstPowerup)
		{
			hasSpawnedFirstPowerup = true;
			Instantiate(firstPowerupToSpawn, position, Quaternion.identity);
			return;
		}

		if (Random.Range(0.0f, 1.0f) < chanceToSpawnAnyPowerup)
		{
			if (Random.Range(0.0f, 1.0f) < chanceForPowerupToBeWeapon && weapons.Length > 0)
			{
				SpawnWeapon(position);
			}
			else
			{
				SpawnPowerup(position);
			}
		}
	}


	public void SpawnWeapon(Vector3 position)
	{
		Instantiate(weapons[Random.Range(0, weapons.Length)], position, Quaternion.identity);
	}


	public void SpawnPowerup(Vector3 position)
	{
		Instantiate(powerups[Random.Range(0, powerups.Length)], position, Quaternion.identity);
	}


	void Awake()
	{
		if (instance == null)
		{
			instance = this;
		}
		else if (instance != this)
		{
			Destroy(gameObject);
		}

		hasSpawnedFirstPowerup = false;
	}
}
