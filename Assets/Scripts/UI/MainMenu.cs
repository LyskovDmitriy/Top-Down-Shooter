using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour 
{

	public GameObject levelsPanel;


	public void OpenLevelsPanel()
	{
		levelsPanel.SetActive(true);
	}


	public void CloseLevelsPanel()
	{
		levelsPanel.SetActive(false);
	}


	public void LoadLevel(string levelName)
	{
		SceneManager.LoadScene(levelName);
	}


	public void Quit()
	{
		Application.Quit();
	}
}
