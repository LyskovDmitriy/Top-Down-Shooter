using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthManager : MonoBehaviour 
{

	public GameObject bloodParticles;
	public GameObject[] powerups; //TODO Can be reorganized through ScriptableObject
	public float chanceToSpawnPowerup;
	public int maxHealth;


	private float currentHealth;


	public void GetHurt(float damage)
	{
		currentHealth -= damage;
		Instantiate(bloodParticles, transform.position, transform.rotation); //TODO create a pool for particles (remove DestroyOverTime script)

		if (currentHealth <= 0)
		{
			Die();
		}
	}


	void Start()
	{
		currentHealth = maxHealth;
	}


	void Die()
	{
		if (Random.Range(0.0f, 1.0f) < chanceToSpawnPowerup)
		{
			SpawnRandomPowerup();
		}
		Destroy(gameObject);
	}


	void SpawnRandomPowerup()
	{
		int powerupIndex = Random.Range(0, powerups.Length);
		Instantiate(powerups[powerupIndex], transform.position, Quaternion.identity);
	}
}
