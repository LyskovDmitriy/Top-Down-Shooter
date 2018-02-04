using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class PowerupsBehaviorInfo : ScriptableObject 
{
	public float TimeToDisappear { get { return timeToDisappear; } }
	public float BlinkingDuration { get { return blinkingDuration; } }
	public float BlinkTime { get { return blinkTime; } }


	[SerializeField] private float timeToDisappear;
	[SerializeField] private float blinkingDuration;
	[SerializeField] private float blinkTime;
}
