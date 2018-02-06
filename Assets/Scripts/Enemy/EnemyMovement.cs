using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour 
{

	public float movespeed;
	public float timeToMove;
	public float timeToWait;
	public float angleDispersion;
	public float distanceToFollowPlayer;


	private static Transform player;


	private Vector3 direction;
	private float timeToMoveCounter;
	private float timeToWaitCounter;
	private float distanceToFollowSqr;
	private bool isMoving;


	
	void Start () 
	{
		distanceToFollowSqr = distanceToFollowPlayer * distanceToFollowPlayer;

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

		isMoving = true;
		RotateTowardsPlayer(true);
		timeToMoveCounter = timeToMove;
	}
	
	
	void Update () 
	{
		if (player == null)
		{
			enabled = false;
			return;
		}

		if (isMoving)
		{
			//TODO follow player if he is in certain radius
			float sqrDistanceToPlayer = (player.position - transform.position).sqrMagnitude;

			if (sqrDistanceToPlayer < 0.01f)
			{
				timeToMoveCounter = timeToMove;
				return;
			}

			if (sqrDistanceToPlayer < distanceToFollowSqr)
			{
				RotateTowardsPlayer(false);
				timeToMoveCounter = timeToMove;
			}

			MoveTowardsPlayer();
			timeToMoveCounter -= Time.deltaTime;

			if (timeToMoveCounter < 0.0f)
			{
				isMoving = false;
				timeToWaitCounter = timeToWait;
			}
		}
		else
		{
			timeToWaitCounter -= Time.deltaTime;

			if (timeToWaitCounter < 0.0f)
			{
				isMoving = true;
				timeToMoveCounter = timeToMove;
				RotateTowardsPlayer(true);
			}
		}
	}


	void MoveTowardsPlayer()
	{
		transform.Translate(Vector3.right * movespeed * Time.deltaTime);
	}


	void RotateTowardsPlayer(bool hasDispersion)
	{
		direction = player.position - transform.position;
		float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
		if (hasDispersion)
		{
			angle += Random.Range(-angleDispersion / 2, angleDispersion / 2);
		}
		transform.eulerAngles = Vector3.forward * angle;
	}
}
