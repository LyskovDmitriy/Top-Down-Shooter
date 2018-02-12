using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class OptionsManager : MonoBehaviour 
{

	public Dropdown resolutionDropdown;
	public Toggle fullscreenToggle;


	private List<Resolution> availableResolutions;


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
		availableResolutions = Screen.resolutions.ToList();
		Resolution currentResolution = new Resolution();
		currentResolution.height = Screen.height;
		currentResolution.width = Screen.width;
		resolutionDropdown.ClearOptions();
		List<string> resolutionStrings = new List<string>(availableResolutions.Count);
		int currentIndex = 0;

		for (int i = 0; i < availableResolutions.Count; i++)
		{
//			if (i > 0) //check for identical resolutions
//			{
//				if (availableResolutions[i].width == availableResolutions[i - 1].width
//				    && availableResolutions[i].height == availableResolutions[i - 1].height) //TODO find a better solution
//				{
//					continue;
//				}
//			}
			
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


	void Start()
	{
		CursorController.instance.SetDefaultCursor();
	}
}
