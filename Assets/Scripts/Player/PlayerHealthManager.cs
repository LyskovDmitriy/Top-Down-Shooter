using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthManager : MonoBehaviour 
{

	public static event System.Action onPlayerDeath;


	public Slider healthBar;
	public int maxHealth;


	private float currentHealth;
	private float chanceToEvade;


	public void GetHurt(float damage)
	{
		if (chanceToEvade != 0.0f)
		{
			if (Random.Range(0.0f, 1.0f) < chanceToEvade)
			{
				return;
			}
		}
		currentHealth = Mathf.Clamp(currentHealth - damage, 0.0f, maxHealth);
		healthBar.value = currentHealth;

		if (currentHealth <= 0)
		{
			onPlayerDeath();
		}
	}


	public void SetChanceToEvade(float evasion)
	{
		chanceToEvade = evasion;
	}


	void Start () 
	{
		currentHealth = maxHealth;
		healthBar.maxValue = maxHealth;
		healthBar.value = currentHealth;
		chanceToEvade = 0.0f;

		onPlayerDeath += Die;
	}


	void Die()
	{
		Destroy(gameObject);
	}


	void OnDestroy()
	{
		onPlayerDeath -= Die;
	}
}
