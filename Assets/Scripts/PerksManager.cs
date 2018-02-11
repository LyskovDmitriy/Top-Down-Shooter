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
	private bool surgeonIsActive;


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

		playerController = FindObjectOfType<PlayerController>();
		playerHealth = FindObjectOfType<PlayerHealthManager>();

		surgeonIsActive = false;
	}


	void Update()
	{
		if (surgeonIsActive)
		{
			if (playerController != null)
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


	void Dodger()
	{
		playerHealth.SetChanceToEvade(perksInfo.DodgerChanceToEvade);
	}


	void RandomWeapon()
	{
		Vector3 positionForWeapon = Vector3.zero;

		Camera camera = Camera.main;
		float halfCameraHeight = camera.orthographicSize;
		float halfCameraWidth = halfCameraHeight * camera.aspect;
		float randomOffsetX = Random.Range(-halfCameraWidth, halfCameraWidth);
		float randomOffsetY = Random.Range(-halfCameraHeight, halfCameraHeight);
		positionForWeapon.x = camera.transform.position.x + randomOffsetX;
		positionForWeapon.y = camera.transform.position.y + randomOffsetY;

		PowerupsSpawnManager.instance.SpawnWeapon(positionForWeapon);
	}
}
