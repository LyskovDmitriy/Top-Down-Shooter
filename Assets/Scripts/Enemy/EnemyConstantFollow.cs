using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyConstantFollow : MonoBehaviour 
{

	public float movespeed;


	private static Transform player;


	private Vector3 direction;


	void Start () 
	{
		if (player == null)
		{
			PlayerController playerController = FindObjectOfType<PlayerController>();
			if (playerController != null)
			{
				player = playerController.transform;
			}
			else
			{
				enabled = false;
				return;
			}
		}
	}


	void Update () 
	{
		if (player == null)
		{
			enabled = false;
			return;
		}

		float sqrDistanceToPlayer = (player.position - transform.position).sqrMagnitude;

		if (sqrDistanceToPlayer < 0.01f)
		{
			return;
		}

		RotateTowardsPlayer();
		Move();
	}


	void Move()
	{
		transform.Translate(Vector3.right * movespeed * Time.deltaTime);
	}


	void RotateTowardsPlayer()
	{
		direction = player.position - transform.position;
		float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
		transform.eulerAngles = Vector3.forward * angle;
	}
}
