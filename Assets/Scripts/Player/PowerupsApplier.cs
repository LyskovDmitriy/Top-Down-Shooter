using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupsApplier : MonoBehaviour 
{

	public static PowerupsApplier instance;


	public WeaponType currentWeapon;
	public PowerupsInfo powerupsInfo;
	public WeaponsCollection weaponsCollection;


	private PlayerController playerController;
	private PlayerHealthManager playerHealth;


	public void Apply(PowerupType type)
	{
		switch (type)
		{
			case PowerupType.SpeedIncrease:
				StartCoroutine(IncreaseSpeed());
				break;
			case PowerupType.SlowMotion:
				StartCoroutine(SlowDownTime());
				break;
			case PowerupType.HealthKit:
				HealPlayer();
				break;
			case PowerupType.Explosion:
				Explode();
				break;
			default:
				Debug.LogWarning("Can't recognize powerup type");
				break;
		}
	}


	public void ChangeWeapon(WeaponType type)
	{
		WeaponStats currentWeapon = weaponsCollection.GetWeaponInfo(type);
		ObjectPool pool = PoolsManager.instance.GetPool(type);
		playerController.SetWeapon(currentWeapon, pool);
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

		playerController = GetComponent<PlayerController>();
		playerHealth = GetComponent<PlayerHealthManager>();
	}


	void Start()
	{
		ChangeWeapon(currentWeapon); //Starting weapon
	}


	IEnumerator IncreaseSpeed()
	{
		playerController.moveSpeed *= powerupsInfo.SpeedMultiplier;

		yield return new WaitForSeconds(powerupsInfo.SpeedIncreaseDuration);

		playerController.moveSpeed /= powerupsInfo.SpeedMultiplier;
	}


	IEnumerator SlowDownTime()
	{
		Time.timeScale *= powerupsInfo.SlowDownMultiplier;
		Time.fixedDeltaTime *= powerupsInfo.SlowDownMultiplier;
		playerController.moveSpeed *= powerupsInfo.AdditionalPlayerSlowDownMultiplier;

		yield return new WaitForSeconds(powerupsInfo.SpeedIncreaseDuration * Time.timeScale);

		playerController.moveSpeed /= powerupsInfo.AdditionalPlayerSlowDownMultiplier;
		Time.fixedDeltaTime /= powerupsInfo.SlowDownMultiplier;
		Time.timeScale = 1.0f;
	}


	void Explode()
	{
		Instantiate(powerupsInfo.ExplosionParticles, transform.position, transform.rotation);
		Collider2D[] enemies = Physics2D.OverlapCircleAll(transform.position, powerupsInfo.ExplosionRadius);

		for (int i = 0; i < enemies.Length; i++)
		{
			EnemyHealthManager enemyHealth = enemies[i].GetComponent<EnemyHealthManager>();

			if (enemyHealth != null)
			{
				enemyHealth.GetHurt(1000);
			}
		}
	}


	void HealPlayer()
	{
		playerHealth.GetHurt(-powerupsInfo.HealthPerKit);
	}


	void OnDestroy()
	{
		StopAllCoroutines();
		if (Time.timeScale != 1.0f)
		{
			Time.timeScale = 1.0f;
		}
	}
}
