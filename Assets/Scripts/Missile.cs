using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : Projectile 
{

	public GameObject explosionParticles;
	public LayerMask enemyLayer;
	public float explosionRadius;


	protected override void OnTriggerEnter2D(Collider2D other)
	{
		if (other.GetComponent<EnemyHealthManager>() != null)
		{
			Explode();
			ReturnObject();
		}
	}


	void Explode()
	{
		Instantiate(explosionParticles, transform.position, transform.rotation);
		Collider2D[] enemies = Physics2D.OverlapCircleAll(transform.position, explosionRadius);

		for (int i = 0; i < enemies.Length; i++)
		{
			EnemyHealthManager enemyHealth = enemies[i].GetComponent<EnemyHealthManager>();

			if (enemyHealth != null)
			{
				enemyHealth.GetHurt(stats.Damage);
			}
		}
	}
}
