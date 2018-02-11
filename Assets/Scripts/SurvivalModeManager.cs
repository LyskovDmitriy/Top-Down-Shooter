using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SurvivalModeManager : MonoBehaviour 
{

	public static SurvivalModeManager instance;


	public int RoundNumber { get; private set; }


	public GameObject enemySpawnerPrefab;
	public GameObject[] enemiesHierarchy;
	public Slider spawnProgressBar;
	public int roundTime;
	public int timeToChangeEnemyToSpawn;
	public float maxTimeBetweenSpawns;
	public float minTimeBetweenSpawns;
	public float timeBetweenSpawnsDecreasePerRound;


	private EnemySpawner currentEnemySpawner;
	private int currentDifficulty;
	private float timeBetweenSpawns;
	private float timeToChangeEnemyCounter;
	private float chanceToSpawnEnemyWithCurrentDifficulty = 0.6f;


	void Start () 
	{
		if (instance == null)
		{
			instance = this;
		}
		else if (instance != this)
		{
			Destroy(gameObject);
		}

		RoundNumber = 1;
		currentDifficulty = 0;
		timeBetweenSpawns = maxTimeBetweenSpawns;
		CreateEnemySpawner();

		PerksManager.onPerkApplied += RoundOver;
	}
	

	void Update () 
	{
		if (timeToChangeEnemyCounter >= timeToChangeEnemyToSpawn)
		{
			currentEnemySpawner.waves[0].enemyPrefab = GetEnemyForSpawner();
			timeToChangeEnemyCounter = 0.0f;
		}
		
		timeToChangeEnemyCounter += Time.deltaTime;
	}


	void CreateEnemySpawner()
	{
		currentEnemySpawner = Instantiate(enemySpawnerPrefab, transform.position, transform.rotation).GetComponent<EnemySpawner>();
		currentEnemySpawner.transform.SetParent(transform);
		currentEnemySpawner.spawnProgressBar = spawnProgressBar;

		EnemyWave[] wave = new EnemyWave[1];
		wave[0].enemyPrefab = GetEnemyForSpawner();
		wave[0].spawnNumber = Mathf.RoundToInt(roundTime / timeBetweenSpawns);
		wave[0].timeBetweenSpawns = timeBetweenSpawns;

		currentEnemySpawner.waves = wave;
		timeToChangeEnemyCounter = 0.0f;
	}


	GameObject GetEnemyForSpawner()
	{
		if (Random.Range(0.0f, 1.0f) < chanceToSpawnEnemyWithCurrentDifficulty)
		{
			return enemiesHierarchy[currentDifficulty];
		}
		else
		{
			if (currentDifficulty == 0)
			{
				return enemiesHierarchy[1];
			}
			else if (currentDifficulty + 1 == enemiesHierarchy.Length)
			{
				return enemiesHierarchy[currentDifficulty - 1];
			}
			else
			{
				if (Random.Range(0.0f, 1.0f) < 0.5f)
				{
					return enemiesHierarchy[currentDifficulty - 1];
				}
				else
				{
					return enemiesHierarchy[currentDifficulty + 1];
				}
			}
		}
	}


	void RoundOver()
	{
		if (currentDifficulty + 1 < enemiesHierarchy.Length)
		{
			currentDifficulty++;
		}

		RoundNumber++;
		timeBetweenSpawns = Mathf.Clamp(timeBetweenSpawns - timeBetweenSpawnsDecreasePerRound, maxTimeBetweenSpawns, minTimeBetweenSpawns);
		Destroy(currentEnemySpawner.gameObject);
		CreateEnemySpawner();
	}


	void OnDestroy()
	{
		PerksManager.onPerkApplied -= RoundOver;
	}
}
