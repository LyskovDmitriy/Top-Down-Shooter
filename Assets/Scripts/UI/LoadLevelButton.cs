using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadLevelButton : MonoBehaviour 
{

	public GameObject lockedObject;
	public string levelName;


	private static MainMenu mainMenu;


	public void LoadLevel()
	{
		mainMenu.LoadLevel(levelName);
	}

	
	void Start () 
	{
		if (mainMenu == null)
		{
			mainMenu = FindObjectOfType<MainMenu>();
		}
			
		int levelNumberIndex = levelName.IndexOf(" ");
		int previousLevelNumber = int.Parse(levelName.Substring(levelNumberIndex + 1)) - 1;
		if (previousLevelNumber == 0)
		{
			return;
		}
		string previousLevelName = levelName.Substring(0, levelNumberIndex + 1) + previousLevelNumber;
		if (!PlayerPrefs.HasKey(previousLevelName) || PlayerPrefs.GetInt(previousLevelName) <= 0)
		{
			GetComponent<Button>().interactable = false;
			lockedObject.SetActive(true);
		}
	}
}
