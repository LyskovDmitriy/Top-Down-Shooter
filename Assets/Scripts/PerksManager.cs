using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerksManager : MonoBehaviour 
{

	public static PerksManager instance;
	public static event System.Action onPerkApplied;


	public PerksInfo perksInfo;


	private PlayerController playerController;
	private PlayerHealthManager playerHealth;
	private float surgeonCounter;
	private float teleportationCounter;
	private float powerupsRainCounter;
	private bool perksActive;
	private bool surgeonIsActive;
	private bool teleportationIsActive;
	private bool powerupsRainIsActive;


	public void ApplyPerk(Perk perkToAdd)
	{
		switch (perkToAdd)
		{
			case Perk.None:
				break;
			case Perk.FasterThanLight:
				FasterThanLight();
				break;
			case Perk.Surgeon:
				Surgeon();
				break;
			case Perk.Dodger:
				Dodger();
				break;
			case Perk.RandomWeapon:
				RandomWeapon();
				break;
			case Perk.SpontaneousTeleportation:
				ActivateTeleportation();
				break;
			case Perk.PowerupsRain:
				ActivatePowerupsRain();
				break;
			default:
				Debug.Log("Can't find perk to add");
				break;
		}

		if (onPerkApplied != null)
		{
			onPerkApplied();
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

		EnemySpawner.onAllEnemiesDeath += StopPerksFunctioning;
		PerksManager.onPerkApplied += ResumePerksFunctioning;

		playerController = FindObjectOfType<PlayerController>();
		playerHealth = FindObjectOfType<PlayerHealthManager>();

		surgeonIsActive = false;
		teleportationIsActive = false;
		perksActive = true;
	}


	void Update()
	{
		if (playerController == null || !perksActive)
		{
			return;
		}
		//checks whether perk's methods should be called
		SurgeonUsageCheck();
		TeleportationUsageCheck();
		PowerupsRainCheck();
	}


	void ResumePerksFunctioning()
	{
		perksActive = true;
	}
		
	void StopPerksFunctioning()
	{
		perksActive = false;
	}


	Vector3 GetRandomPositionInCameraView()
	{
		Vector3 randomPosition = Vector3.zero;
		Camera camera = Camera.main;
		float halfCameraHeight = camera.orthographicSize;
		float halfCameraWidth = halfCameraHeight * camera.aspect;
		float randomOffsetX = Random.Range(-halfCameraWidth, halfCameraWidth);
		float randomOffsetY = Random.Range(-halfCameraHeight, halfCameraHeight);
		randomPosition.x = camera.transform.position.x + randomOffsetX;
		randomPosition.y = camera.transform.position.y + randomOffsetY;
		return randomPosition;
	}


	void FasterThanLight()
	{
		playerController.moveSpeed *= perksInfo.FasterThanLightMovespeedMultiplier;
	}


	void Surgeon()
	{
		surgeonIsActive = true;
		surgeonCounter = perksInfo.SurgeonTimeToRestoreOneHealthPoint;
	}


	void SurgeonUsageCheck()
	{
		if (surgeonIsActive)
		{		
			if (surgeonCounter <= 0.0f)
			{
				playerHealth.GetHurt(-1.0f);
				surgeonCounter = perksInfo.SurgeonTimeToRestoreOneHealthPoint;
			}
			else
			{
				surgeonCounter -= Time.deltaTime;
			}
		}
	}


	void Dodger()
	{
		playerHealth.SetChanceToEvade(perksInfo.DodgerChanceToEvade);
	}


	void RandomWeapon()
	{
		Vector3 positionForWeapon = GetRandomPositionInCameraView();
		PowerupsSpawnManager.instance.SpawnWeapon(positionForWeapon);
	}


	void ActivateTeleportation()
	{
		teleportationIsActive = true;
		teleportationCounter = perksInfo.TeleportationTimeToTryTeleport;
	}

	void TryTeleportate()
	{
		if (Random.Range(0.0f, 1.0f) < perksInfo.TeleportationChance)
		{
			Vector3 position = Vector3.zero;
			position.x = Random.Range(ActivePlayerZone.MinPoint.x, ActivePlayerZone.MaxPoint.x);
			position.y = Random.Range(ActivePlayerZone.MinPoint.y, ActivePlayerZone.MaxPoint.y);

			Instantiate(perksInfo.TeleportationParticles, playerController.transform.position, Quaternion.identity);
			playerController.transform.position = position;
			Instantiate(perksInfo.TeleportationParticles, position, Quaternion.identity);
		}
	}

	void TeleportationUsageCheck()
	{
		if (teleportationIsActive)
		{
			if (teleportationCounter <= 0)
			{
				TryTeleportate();
				teleportationCounter = perksInfo.TeleportationTimeToTryTeleport;
			}
			else
			{
				teleportationCounter -= Time.deltaTime;
			}
		}
	}


	void ActivatePowerupsRain()
	{
		powerupsRainIsActive = true;
		powerupsRainCounter = perksInfo.PowerupsRainTimeToTrySpawn;
	}
		
	void TrySpawnPowerup()
	{
		if (Random.Range(0.0f, 1.0f) < perksInfo.PowerupsRainChance)
		{
			Vector3 positionForPowerup = GetRandomPositionInCameraView();
			PowerupsSpawnManager.instance.SpawnPowerup(positionForPowerup);
		}
	}


	void PowerupsRainCheck()
	{
		if (powerupsRainIsActive)
		{
			if (powerupsRainCounter <= 0)
			{
				TrySpawnPowerup();
				powerupsRainCounter = perksInfo.PowerupsRainTimeToTrySpawn;
			}
			else
			{
				powerupsRainCounter -= Time.deltaTime;
			}
		}
	}


	void OnDestroy()
	{
		EnemySpawner.onAllEnemiesDeath -= StopPerksFunctioning;
		PerksManager.onPerkApplied -= ResumePerksFunctioning;
	}
}
