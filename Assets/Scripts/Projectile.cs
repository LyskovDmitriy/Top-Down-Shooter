using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour 
{

	public WeaponStats stats;


	private Rigidbody2D rb;
	private TrailRenderer trail;
	private PoolSignature signature;
	private int collisionNumber;
	private bool isReturned;


	protected virtual void OnTriggerEnter2D(Collider2D other)
	{
		EnemyHealthManager enemyHealth = other.GetComponent<EnemyHealthManager>();
		if (enemyHealth != null)
		{
			collisionNumber++;
			enemyHealth.GetHurt(stats.Damage);
			if (collisionNumber > stats.PiercingStrength)
			{
				ReturnObject();
			}
		}
	}


	protected void ReturnObject()
	{
		if (isReturned)
		{
			return;
		}
		if (signature == null)
		{
			signature = GetComponent<PoolSignature>();
		}

		isReturned = true;
		signature.ReturnToPool();
	}


	void Awake()
	{
		rb = GetComponent<Rigidbody2D>();
		trail = GetComponent<TrailRenderer>();
	}

	
	void OnEnable () 
	{
		if (trail != null)
		{
			trail.Clear();
			trail.enabled = true;
		}
		rb.velocity = transform.right * stats.Speed;
		collisionNumber = 0;
		isReturned = false;
	}
		

	void OnTriggerExit2D(Collider2D other)
	{
		if (other.CompareTag("ActiveZoneBorders"))
		{
			//Debug.Log("Leave zone");
			if (gameObject.activeSelf)
			{
				ReturnObject();
			}
		}
	}


	void OnDisable()
	{
		if (trail != null)
		{
			trail.enabled = false;
		}
	}
}
