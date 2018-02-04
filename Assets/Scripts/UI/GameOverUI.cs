using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverUI : MonoBehaviour 
{

	public GameObject gameOverScreen;
	public Text screenTitle;


	public void GameOver()
	{
		gameOverScreen.SetActive(true);
		screenTitle.text = "You died!";
	}


	public void GameWon()
	{
		gameOverScreen.SetActive(true);
		screenTitle.text = "Level completed";
	}


	public void RestartLevel()
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().name);
	}


	void Awake()
	{
		PlayerHealthManager.onPlayerDeath += GameOver;
		EnemySpawner.onAllEnemiesDeath += GameWon;
	}


	void OnDestroy()
	{
		PlayerHealthManager.onPlayerDeath -= GameOver;
		EnemySpawner.onAllEnemiesDeath -= GameWon;
	}
}
