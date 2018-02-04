using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupsApplier : MonoBehaviour 
{

	public static PowerupsApplier instance;


	public PowerupsInfo powerupsInfo;


	private PlayerController playerController;


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

		yield return new WaitForSeconds(powerupsInfo.SpeedIncreaseDuration * powerupsInfo.SlowDownMultiplier);

		Time.fixedDeltaTime /= powerupsInfo.SlowDownMultiplier;
		Time.timeScale = 1.0f;
	}
}
