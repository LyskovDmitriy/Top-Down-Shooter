using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class PerksInfo : ScriptableObject 
{
	public float FasterThanLightMovespeedMultiplier { get { return fasterThanLightMovespeedMultiplier; } }

	public float SurgeonTimeToRestoreOneHealthPoint { get { return surgeonTimeToRestoreOneHealthPoint; } }

	public float DodgerChanceToEvade { get { return dodgerChanceToEvade; } }


	[SerializeField] private float fasterThanLightMovespeedMultiplier;

	[SerializeField] private float surgeonTimeToRestoreOneHealthPoint;

	[SerializeField] private float dodgerChanceToEvade;
}
