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
		if (signature == null)
		{
			signature = GetComponent<PoolSignature>();
		}

		//Debug.Log("Return");
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
		}
		rb.velocity = transform.right * stats.Speed;
		collisionNumber = 0;
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
}
