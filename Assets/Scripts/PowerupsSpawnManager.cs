using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupsSpawnManager : MonoBehaviour 
{

	public static PowerupsSpawnManager instance;


	public GameObject[] powerups; //TODO Can be reorganized through ScriptableObject
	public GameObject[] weapons;
	public float chanceToSpawnAnyPowerup;
	public float chanceForPowerupToBeWeapon;
	//TODO make firat powerup be a weapon


	public void TrySpawnPowerup(Vector3 position)
	{
		if (Random.Range(0.0f, 1.0f) < chanceToSpawnAnyPowerup)
		{
			if (Random.Range(0.0f, 1.0f) < chanceForPowerupToBeWeapon)
			{
				Instantiate(weapons[Random.Range(0, weapons.Length)], position, Quaternion.identity);
			}
			else
			{
				Instantiate(powerups[Random.Range(0, powerups.Length)], position, Quaternion.identity);
			}
		}
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
	}
}
