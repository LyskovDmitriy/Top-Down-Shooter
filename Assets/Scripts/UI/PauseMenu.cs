using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour 
{

	public GameObject pauseScreen;


	private float timeScaleBeforePause;


	public void LoadLevel(string levelName)
	{
		if (Time.timeScale <= 1.0f)
		{
			Time.timeScale = 1.0f;
		}
		SceneManager.LoadScene(levelName);
	}


	public void Pause()
	{
		CursorController.instance.SetDefaultCursor();
		timeScaleBeforePause = Time.timeScale;
		Time.timeScale = 0.0f;
		pauseScreen.SetActive(true);
	}


	public void Resume()
	{
		CursorController.instance.SetShootCursor();
		Time.timeScale = timeScaleBeforePause;
		pauseScreen.SetActive(false);
	}


	public void Quit()
	{
		Application.Quit();
	}


	void Update()
	{
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			if (pauseScreen.activeSelf)
			{
				Resume();
			}
			else
			{
				Pause();
			}
		}
	}
}
