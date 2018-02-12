using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class PerksInfo : ScriptableObject 
{
	public float FasterThanLightMovespeedMultiplier { get { return fasterThanLightMovespeedMultiplier; } }

	public float SurgeonTimeToRestoreOneHealthPoint { get { return surgeonTimeToRestoreOneHealthPoint; } }

	public float DodgerChanceToEvade { get { return dodgerChanceToEvade; } }

	public GameObject TeleportationParticles { get { return teleportationParticles; } }
	public float TeleportationChance { get { return teleportationChance; } }
	public float TeleportationTimeToTryTeleport { get { return teleportationTimeToTryTeleport; } }

	public float PowerupsRainChance { get { return powerupsRainChance; } }
	public float PowerupsRainTimeToTrySpawn { get { return powerupsRainTimeToTrySpawn; } }


	[SerializeField] private float fasterThanLightMovespeedMultiplier;

	[SerializeField] private float surgeonTimeToRestoreOneHealthPoint;

	[SerializeField] private float dodgerChanceToEvade;

	[SerializeField] private GameObject teleportationParticles;
	[SerializeField] private float teleportationChance;
	[SerializeField] private float teleportationTimeToTryTeleport;

	[SerializeField] private float powerupsRainChance;
	[SerializeField] private float powerupsRainTimeToTrySpawn;
}
