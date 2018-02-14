using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthManager : MonoBehaviour 
{

	public GameObject bloodParticles;
	public int maxHealth;


	private float currentHealth;


	public void GetHurt(float damage)
	{
		currentHealth -= damage;

		if (bloodParticles != null)
		{
			Instantiate(bloodParticles, transform.position, transform.rotation); //TODO create a pool for particles (remove DestroyOverTime script)
		}
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
		PowerupsSpawnManager.instance.TrySpawnPowerup(transform.position);
		Destroy(gameObject);
	}
}
