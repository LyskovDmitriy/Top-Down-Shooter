using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverUI : MonoBehaviour 
{

	public GameObject gameOverScreen;
	public GameObject nextLevelButton;
	public Text screenTitle;


	public void GameOver()
	{
		gameOverScreen.SetActive(true);
		nextLevelButton.SetActive(false);
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


	public void LoadNextLevel()
	{
		string thisLevelName = SceneManager.GetActiveScene().name;
		int levelNumberIndex = thisLevelName.IndexOf(" ");
		int nextLevelNumber = int.Parse(thisLevelName.Substring(levelNumberIndex + 1)) + 1;
		string nextLevelName = thisLevelName.Substring(0, levelNumberIndex + 1) + nextLevelNumber;
		SceneManager.LoadScene(nextLevelName); //throws an error when there is no next level
		//Can be executed thorough a ScriptableObject containing all scenes
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
