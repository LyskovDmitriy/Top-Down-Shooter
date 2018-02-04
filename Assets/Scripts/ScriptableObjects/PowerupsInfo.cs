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


	[SerializeField] private float speedIncreaseDuration;
	[SerializeField] private float speedMultiplier;

	[SerializeField] private float slowDownDuration;
	[SerializeField] private float slowDownMultiplier;
}
