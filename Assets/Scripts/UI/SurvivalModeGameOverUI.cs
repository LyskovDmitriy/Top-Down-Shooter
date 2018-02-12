using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SurvivalModeGameOverUI : MonoBehaviour 
{
	
	public GameObject gameOverScreen;
	public Text screenTitle;


	public void GameOver()
	{
		CursorController.instance.SetDefaultCursor();
		gameOverScreen.SetActive(true);
		screenTitle.text = "You died!";
	}


	public void RestartLevel()
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().name);
	}


	void Awake()
	{
		PlayerHealthManager.onPlayerDeath += GameOver;
	}


	void OnDestroy()
	{
		PlayerHealthManager.onPlayerDeath -= GameOver;
	}
}
