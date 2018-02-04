using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemySpawner : MonoBehaviour 
{

	public static event System.Action onAllEnemiesDeath;


	public EnemyWave[] waves;
	public Slider spawnProgressBar;
	public float timeToFirstSpawn;
	public float randomPositionMultiplier;


	private Transform mainCamera;
	private int spawnedEnemies;
	private float halfCameraWidth;
	private float halfCameraHeight;


	void Start () 
	{
		Camera camera = Camera.main;
		halfCameraHeight = camera.orthographicSize;
		halfCameraWidth = halfCameraHeight * camera.aspect;
		mainCamera = camera.transform;

		int numberOfEnemies = 0;
		for (int i = 0; i < waves.Length; i++)
		{
			numberOfEnemies += waves[i].spawnNumber;
		}
		spawnProgressBar.maxValue = numberOfEnemies;
		spawnedEnemies = 0;
		spawnProgressBar.value = spawnedEnemies;

		StartCoroutine(SpawnEnemies());
	}


	IEnumerator SpawnEnemies () 
	{
		yield return new WaitForSeconds(timeToFirstSpawn);
		for (int i = 0; i < waves.Length; i++)
		{
			for (int j = 0; j < waves[i].spawnNumber; j++)
			{
				Vector3 enemyPosition = GetEnemyPosition();
				GameObject enemy = Instantiate(waves[i].enemyPrefab, enemyPosition, Quaternion.identity);
				enemy.transform.SetParent(transform);		
				spawnedEnemies++;
				spawnProgressBar.value = spawnedEnemies;
				//Debug.Log("Spawn");
				yield return new WaitForSeconds(waves[i].timeBetweenSpawns);
				//TODO: Spawn particles
			}
		}
		StartCoroutine(WaitForAllEnemiesDeath());
	}


	IEnumerator WaitForAllEnemiesDeath()
	{
		//Debug.Log("Started waiting");
		while (true)
		{
			if (FindObjectOfType<EnemyHealthManager>() == null)
			{
				break;
			}
			else
			{
				yield return new WaitForSeconds(1.0f);
			}
		}
		if (onAllEnemiesDeath != null)
		{
			onAllEnemiesDeath();
		}
	}


	Vector3 GetEnemyPosition() //recently spawns relative to camera
	{
		Vector3 position = Vector3.zero;
		float randomOffsetX = halfCameraWidth * Random.Range(1.0f / randomPositionMultiplier, randomPositionMultiplier);
		float randomOffsetY = halfCameraHeight * Random.Range(1.0f / randomPositionMultiplier, randomPositionMultiplier);
		randomOffsetX *= (Random.Range(0, 2) == 0) ? -1 : 1; //gets random sign
		randomOffsetY *= (Random.Range(0, 2) == 0) ? -1 : 1; //gets random sign
		position.x = mainCamera.position.x + randomOffsetX;
		position.y = mainCamera.position.y + randomOffsetY;
		return position;
	}
}
