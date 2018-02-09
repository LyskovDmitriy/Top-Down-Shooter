using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class PowerupsInfo : ScriptableObject 
{

	public float SpeedIncreaseDuration { get { return speedIncreaseDuration; } }
	public float SpeedMultiplier { get { return speedMultiplier; } }

	public float SlowDownDuration { get { return slowDownDuration; } }
	public float SlowDownMultiplier { get { return slowDownMultiplier; } }
	public float AdditionalPlayerSlowDownMultiplier { get { return additionalPlayerSlowDownMultiplier; } }

	public float HealthPerKit { get { return healthPerKit; } }

	public GameObject ExplosionParticles { get { return explosionParticles; } }
	public float ExplosionRadius { get { return explosionRadius; } }


	[SerializeField] private float speedIncreaseDuration;
	[SerializeField] private float speedMultiplier;

	[SerializeField] private float slowDownDuration;
	[SerializeField] private float slowDownMultiplier;
	[SerializeField] private float additionalPlayerSlowDownMultiplier;

	[SerializeField] private float healthPerKit;

	[SerializeField] private GameObject explosionParticles;
	[SerializeField] private float explosionRadius;
}
