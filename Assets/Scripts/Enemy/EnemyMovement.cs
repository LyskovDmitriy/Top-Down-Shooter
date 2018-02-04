using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour 
{

	public float movespeed;
	public float timeToMove;
	public float timeToWait;
	public float angleDispersion;


	private static Transform player;


	private Vector3 direction;
	private float timeToMoveCounter;
	private float timeToWaitCounter;
	private bool isMoving;


	
	void Start () 
	{
		if (player == null)
		{
			PlayerController playerController = FindObjectOfType<PlayerController>();
			if (playerController != null)
			{
				player = playerController.transform;
			}
		}

		isMoving = true;
		RotateTowardsPlayer();
		timeToMoveCounter = timeToMove;
	}
	
	
	void Update () 
	{
		if (isMoving)
		{
			//TODO follow player if he is in certain radius
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
				RotateTowardsPlayer();
			}
		}
	}


	void MoveTowardsPlayer()
	{
		transform.Translate(Vector3.right * movespeed * Time.deltaTime);
	}


	void RotateTowardsPlayer()
	{
		if (player == null)
		{
			enabled = false;
			return;
		}
		direction = player.position - transform.position;
		float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg + Random.Range(-angleDispersion / 2, angleDispersion / 2);
		//direction = new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0.0f);
		transform.eulerAngles = Vector3.forward * angle;
	}
}
