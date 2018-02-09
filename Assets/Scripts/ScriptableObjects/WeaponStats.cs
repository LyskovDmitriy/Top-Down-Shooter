using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class WeaponStats : ScriptableObject 
{

	public float Damage { get { return damage; } }
	public float TimeBetweenShots { get { return timeBetweenShots; } }
	public float Speed { get { return speed; } }
	public float PiercingStrength { get { return piercingStrength; } }


	[SerializeField] private float damage;
	[SerializeField] private float timeBetweenShots;
	[SerializeField] private float speed;
	[SerializeField] private int piercingStrength;
}

