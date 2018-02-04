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


	public void GetHurt(float damage)
	{
		currentHealth = Mathf.Clamp(currentHealth - damage, 0.0f, maxHealth);
		healthBar.value = currentHealth;

		if (currentHealth <= 0)
		{
			onPlayerDeath();
		}
	}


	void Start () 
	{
		currentHealth = maxHealth;
		healthBar.maxValue = maxHealth;
		healthBar.value = currentHealth;

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
