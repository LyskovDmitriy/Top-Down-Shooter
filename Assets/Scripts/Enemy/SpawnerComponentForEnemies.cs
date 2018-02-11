using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerComponentForEnemies : MonoBehaviour 
{

	public GameObject enemyToSpawn;
	public float maxPositionOffsetX;
	public float maxPositionOffsetY;
	public float timeBetweenSpawns;


	private float betweenSpawnsCounter;


	void Start () 
	{
		betweenSpawnsCounter = timeBetweenSpawns;
	}
	

	void Update () 
	{
		if (betweenSpawnsCounter <= 0.0f)
		{
			SpawnEnemy();
		}
		else
		{
			betweenSpawnsCounter -= Time.deltaTime;
		}
	}


	void SpawnEnemy()
	{
		Instantiate(enemyToSpawn, GetRandomEnemyPosition(), Quaternion.identity);
		betweenSpawnsCounter = timeBetweenSpawns;
	}


	Vector3 GetRandomEnemyPosition() //recently spawns relative to camera
	{
		Vector3 position = Vector3.zero;
		float randomOffsetX = 0.0f;
		float randomOffsetY = 0.0f;

		if (Random.Range(0, 2) == 0)
		{
			randomOffsetX = Random.Range(0.0f, maxPositionOffsetX);
			randomOffsetY = maxPositionOffsetY;
		}
		else
		{
			randomOffsetX = maxPositionOffsetX;
			randomOffsetY = Random.Range(0.0f, maxPositionOffsetY);
		}

		randomOffsetX *= (Random.Range(0, 2) == 0) ? -1 : 1; //gets random sign
		randomOffsetY *= (Random.Range(0, 2) == 0) ? -1 : 1; //gets random sign
		position.x = transform.position.x + randomOffsetX;
		position.y = transform.position.y + randomOffsetY;
		return position;
	}
}
