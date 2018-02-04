using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour 
{
	
	public float damage;
	public float timeBetweenAttacks;


	private PlayerHealthManager playerHealth;


	IEnumerator AttackPlayerOverTime()
	{
		while (true)
		{
			if (playerHealth != null)
			{
				playerHealth.GetHurt(damage);
				yield return new WaitForSeconds(timeBetweenAttacks);
			}
			else
			{
				yield break;
			}
		}
	}


	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.CompareTag("Player"))
		{
			if (playerHealth == null)
			{
				playerHealth = other.GetComponent<PlayerHealthManager>();
				StartCoroutine(AttackPlayerOverTime());
			}
		}
	}


	void OnTriggerExit2D(Collider2D other)
	{
		if (other.CompareTag("Player"))
		{
			playerHealth = null;
		}
	}
}
