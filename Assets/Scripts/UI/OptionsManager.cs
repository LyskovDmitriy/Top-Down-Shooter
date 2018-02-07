using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class OptionsManager : MonoBehaviour 
{

	public Dropdown resolutionDropdown;
	public Toggle fullscreenToggle;


	private Resolution[] availableResolutions;


	public void ReturnToMainMenu()
	{
		SceneManager.LoadScene("MainMenu");
	}


	public void ChangeResolution(int resolutionIndex)
	{
		Screen.SetResolution(availableResolutions[resolutionIndex].width, availableResolutions[resolutionIndex].height, 
			Screen.fullScreen);
		Debug.Log(Screen.currentResolution.width + " " + Screen.currentResolution.height);
	}


	public void ChangeFullscreenState(bool isFullscreen)
	{
		Screen.fullScreen = isFullscreen;
	}

	
	void Awake () 
	{
		Debug.Log(Screen.currentResolution.width + " " + Screen.currentResolution.height);
		availableResolutions = Screen.resolutions;
		Resolution currentResolution = new Resolution();
		currentResolution.height = Screen.height;
		currentResolution.width = Screen.width;
		Debug.Log(Screen.height + " " + Screen.width);
		resolutionDropdown.ClearOptions();
		List<string> resolutionStrings = new List<string>(availableResolutions.Length);
		int currentIndex = 0;

		for (int i = 0; i < availableResolutions.Length; i++)
		{
			resolutionStrings.Add(availableResolutions[i].width + " X " + availableResolutions[i].height);
			if (availableResolutions[i].width == currentResolution.width && 
				availableResolutions[i].height == currentResolution.height)
			{
				currentIndex = i;
			}
		}

		resolutionDropdown.AddOptions(resolutionStrings);
		resolutionDropdown.value = currentIndex;
		resolutionDropdown.onValueChanged.AddListener(ChangeResolution);

		fullscreenToggle.isOn = Screen.fullScreen;
	}
}
