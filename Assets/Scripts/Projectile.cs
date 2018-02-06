using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour 
{

	public WeaponStats stats;


	private Rigidbody2D rb;
	private TrailRenderer trail;
	private PoolSignature signature;


	protected virtual void OnTriggerEnter2D(Collider2D other)
	{
		EnemyHealthManager enemyHealth = other.GetComponent<EnemyHealthManager>();
		if (enemyHealth != null)
		{
			enemyHealth.GetHurt(stats.Damage);
			ReturnObject();
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
		trail.Clear();
		rb.velocity = transform.right * stats.Speed;
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
