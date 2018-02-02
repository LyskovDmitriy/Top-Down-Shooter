using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour 
{

	public WeaponStats stats;


	private Rigidbody2D rb;
	private TrailRenderer trail;
	private PoolSignature signature;


	void Awake()
	{
		rb = GetComponent<Rigidbody2D>();
		trail = GetComponent<TrailRenderer>();
	}

	
	void OnEnable () 
	{
		trail.Clear();
		rb.velocity = transform.right * stats.speed;
	}


	void OnTriggerExit2D(Collider2D collider)
	{
		if (collider.CompareTag("ActiveZoneBorders"))
		{
			if (signature == null)
			{
				signature = GetComponent<PoolSignature>();
			}

			signature.ReturnToPool();
		}
	}
}
